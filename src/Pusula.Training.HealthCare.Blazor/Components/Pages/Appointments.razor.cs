using Pusula.Training.HealthCare.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.AppointmentTypes;
using Syncfusion.Blazor.Schedule;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Departments;
using Syncfusion.Blazor.DropDowns;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Patients;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientTypes;
using System.Collections.ObjectModel;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Appointments
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    private DateTime CurrentDate { get; set; } = DateTime.Today;
    private GetPatientsInputValidator PatientsInputValidator { get; set; } = new();
    private List<AppointmentDto> AppointmentLists { get; set; } = [];

    private List<DoctorDto> Doctors { get; set; } = [];
    private IReadOnlyList<DepartmentDto> Departments { get; set; } = [];
    private List<AppointmentTypeDto> AppointmentTypes { get; set; } = [];
    private Guid SelectedDepartmentId { get; set; }
    private Guid SelectedDoctorId { get; set; }
    private IReadOnlyList<PatientDto> Patients { get; set; } = [];
    private SfAutoComplete<Guid, PatientDto> refAutoComplatePatient { get; set; }
    private SfAutoComplete<Guid, DepartmentDto> refAutoComplateDepartment { get; set; }
    private bool valueSelected => SelectedDepartmentId != Guid.Empty && SelectedDoctorId != Guid.Empty;
    private View CurrentView { get; set; } = View.Week;
    private int IntervalValue { get; set; }
    private bool GridLine { get; set; } = true;
    private DateTime MinDate = DateTime.Now;

    public Appointments()
    {
        SetDefaultsForCreateDto();
        SetDefaultsForUpdateDto();
    }

    public static class AppointmentHelper
    {
        public static IDictionary<string, object> ApplyCategoryColor(
            string color,
            IDictionary<string, object> attributes
        )
        {
            if (attributes == null)
            {
                attributes = new Dictionary<string, object>();
            }

            if (!attributes.ContainsKey("style"))
            {
                attributes["style"] = $"background-color: {color};";
            } else
            {
                attributes["style"] += $" background-color: {color};";
            }

            return attributes;
        }
    }

    public void OnEventRendered(EventRenderedArgs<AppointmentDto> args)
    {
        var categoryColor = args.Data.Status switch
        {
            EnumStatus.Scheduled   => "#FFD700",
            EnumStatus.Completed   => "#32CD32",
            EnumStatus.Cancelled   => "#FF6347",
            EnumStatus.NoShow      => "#A9A9A9",
            EnumStatus.Rescheduled => "#1E90FF",
            _                      => "#FFFFFF"
        };

        var updatedAttributes = AppointmentHelper.ApplyCategoryColor(categoryColor, args.Attributes);
        args.Attributes = updatedAttributes.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    protected async Task GetPatientFilter(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        var filter = new GetPatientsInput { FilterText = args.Text };
        var result = await PatientsInputValidator.ValidateAsync(filter);
        if (!result.IsValid)
        {
            return;
        }

        var patients = await PatientAppService.GetListAsync(filter);
        Patients = patients.Items;
        await refAutoComplatePatient.FilterAsync(Patients);
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        Departments = await DepartmentsAppService.GetListDepartmentsAsync();
        AppointmentTypes = await AppointmentTypeAppService.GetListAppointmentTypesAsync();
    }

    protected async Task DepartmentSelect(ChangeEventArgs<Guid, DepartmentDto> args)
    {
        IntervalValue = args.ItemData.Duration;
        SelectedDepartmentId = args.ItemData.Id;
        Doctors = await DoctorsAppService.GetListDoctorsAsync(args.ItemData.Id);
        await InvokeAsync(StateHasChanged);
    }

    protected async Task DoctorSelect(ChangeEventArgs<Guid, DoctorDto> args)
    {
        SelectedDoctorId = args.ItemData.Id;
        await GetAppointmentsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task GetAppointmentsAsync() =>
        AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);

    private void CancelScheduleEvent(CellClickEventArgs args) => args.Cancel = true;
    private void CancelScheduleEvent(EventClickArgs<AppointmentDto> args) => args.Cancel = true;

#region Patient Creation

    private PatientCreateDialog CreatePatientDialog { get; set; } = null!;

    private async Task OpenCreatePatientDialogAsync() => await CreatePatientDialog.ShowAsync();
    private PatientDto? CreatedPatient { get; set; }

    private async Task PatientCreatedAsync(PatientDto createdPatient)
    {
        CreatedPatient = createdPatient;
        AppointmentCreateDto.PatientId = createdPatient.Id;
        await Task.CompletedTask;
    }

#endregion

#region Create

    private AppointmentCreateDto AppointmentCreateDto { get; set; }
    private EditContext AppointmentCreateContext { get; set; }
    private SfDialog CreateAppointmentDialog { get; set; }

    private async Task OpenCreateAppointmentDialogAsync(CellClickEventArgs args)
    {
        SetDefaultsForCreateDto(args);
        await CreateAppointmentDialog.ShowAsync();
    }

    private async Task CloseCreateAppointmentDialogAsync()
    {
        await CreateAppointmentDialog.HideAsync();
        SetDefaultsForCreateDto();
    }

    private async Task CreateAppointmentAsync()
    {
        try
        {
            AppointmentCreateDto.DepartmentId = SelectedDepartmentId;
            AppointmentCreateDto.DoctorId = SelectedDoctorId;
            if (AppointmentCreateContext.Validate())
            {
                await AppointmentsAppService.CreateAsync(AppointmentCreateDto);
                await GetAppointmentsAsync();
                await CloseCreateAppointmentDialogAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void SetDefaultsForCreateDto(CellClickEventArgs? args = null)
    {
        AppointmentCreateDto = new AppointmentCreateDto()
        {
            StartTime = args?.StartTime ?? DateTime.Today,
            EndTime = args?.EndTime ?? DateTime.Today
        };
        AppointmentCreateContext = new EditContext(AppointmentCreateDto);
    }

#endregion

#region Update

    private AppointmentUpdateDto AppointmentUpdateDto { get; set; }
    private Guid EditingAppointmentId { get; set; }
    private EditContext AppointmentUpdateContext { get; set; }
    private SfDialog UpdateAppointmentDialog { get; set; }

    private async Task OpenUpdateAppointmentDialogAsync(EventClickArgs<AppointmentDto> args)
    {
        EditingAppointmentId = args.Event.Id;
        ObjectMapper.Map(args.Event, AppointmentUpdateDto);
        await UpdateAppointmentDialog.ShowAsync();
    }

    private async Task CloseUpdateAppointmentDialogAsync()
    {
        await UpdateAppointmentDialog.HideAsync();
        SetDefaultsForUpdateDto();
    }

    private async Task UpdateAppointmentAsync()
    {
        try
        {
            if (AppointmentUpdateContext.Validate())
            {
                await AppointmentsAppService.UpdateAsync(EditingAppointmentId, AppointmentUpdateDto);
                await GetAppointmentsAsync();
                await CloseUpdateAppointmentDialogAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void SetDefaultsForUpdateDto()
    {
        AppointmentUpdateDto = new AppointmentUpdateDto();
        AppointmentUpdateContext = new EditContext(AppointmentUpdateDto);
    }

#endregion

    public async Task OnActionBegin(ActionEventArgs<AppointmentDto> args)
    {
        if (args.ActionType == ActionType.EventRemove)
        {
            var x = args.DeletedRecords.First();
            await AppointmentsAppService.DeleteAsync(x.Id);
            AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);
            await InvokeAsync(StateHasChanged);
        }
    }
}