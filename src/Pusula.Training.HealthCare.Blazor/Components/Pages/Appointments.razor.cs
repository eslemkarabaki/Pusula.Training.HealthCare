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

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Appointments
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    private DateTime CurrentDate { get; set; } = DateTime.Today;
    private GetPatientsInputValidator PatientsInputValidator { get; set; } = new();
    private SfSchedule<AppointmentDto> refSchedule { get; set; }
    private List<AppointmentDto> AppointmentLists { get; set; } = [];
    private AppointmentCreateDto AppointmentCreateDto { get; set; } = new();
    private List<DoctorDto> Doctors { get; set; } = [];
    private IReadOnlyList<DepartmentDto> Departments { get; set; } = [];
    private List<AppointmentTypeDto> AppointmentTypes { get; set; } = [];
    private Guid SelectedPatientId { get; set; }
    private Guid SelectedDepartmentId { get; set; }
    private Guid SelectedDoctorId { get; set; }
    private string PatientFilter { get; set; } = string.Empty;
    private string ErrorMessage { get; set; } = string.Empty;
    private IReadOnlyList<PatientDto> Patients { get; set; } = [];
    private SfAutoComplete<Guid, PatientDto> refAutoComplatePatient { get; set; }    
    private SfAutoComplete<Guid, DepartmentDto> refAutoComplateDepartment { get; set; }    
    private bool valueSelected => SelectedDepartmentId != Guid.Empty && SelectedDoctorId != Guid.Empty;
    private EditContext AppointmentContext { get; set; }
    private View CurrentView { get; set; } = View.Week;    
    private int IntervalValue { get; set; } 
    private bool GridLine { get; set; } = true;
    private PatientCreateDialog CreatePatientDialog { get; set; } = null!;
    private IEnumerable<CountryDto> CountryList { get; set; } = [];
    private IEnumerable<PatientTypeDto> PatientTypeList { get; set; } = [];
    private async Task OpenCreatePatientDialogAsync() => await CreatePatientDialog.ShowAsync();
    private IReadOnlyList<PatientDto> PatientDetails { get; set; } = [];
    private PatientDto? LastPatient { get; set; } 
    private DateTime MinDate = DateTime.Now;
   
    private async Task CreatePatientAsync(PatientDto patient)
    {
        LastPatient = patient;
        await Task.CompletedTask;
    }
    
    public static class AppointmentHelper
    {
        public static IDictionary<string, object> ApplyCategoryColor(string color, IDictionary<string, object> attributes)
        {          
                if (attributes == null)
                {
                    attributes = new Dictionary<string, object>();
                }

                if (!attributes.ContainsKey("style"))
                {
                    attributes["style"] = $"background-color: {color};";
                }
                else
                {
                    attributes["style"] += $" background-color: {color};";
                }

                return attributes;
            }
    }
    public void OnEventRendered(EventRenderedArgs<AppointmentDto> args)
    {
        string categoryColor = args.Data.Status switch
        {
            EnumStatus.Scheduled => "#FFD700", 
            EnumStatus.Completed => "#32CD32", 
            EnumStatus.Cancelled => "#FF6347", 
            EnumStatus.NoShow => "#A9A9A9",    
            EnumStatus.Rescheduled => "#1E90FF", 
            _ => "#FFFFFF" 
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
        AppointmentContext = new EditContext(AppointmentCreateDto);
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
        AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(args.ItemData.Id);
        await InvokeAsync(StateHasChanged);
    }
    public async Task OnActionBegin(ActionEventArgs<AppointmentDto> args)
    {       

        if (args.ActionType == Syncfusion.Blazor.Schedule.ActionType.EventCreate) {
            if(AppointmentContext.Validate())
            {
                AppointmentCreateDto.DepartmentId = SelectedDepartmentId;
                AppointmentCreateDto.DoctorId = SelectedDoctorId;
                await AppointmentsAppService.CreateAsync(AppointmentCreateDto);
                AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);
                AppointmentCreateDto = new AppointmentCreateDto();
                AppointmentContext = new EditContext(AppointmentCreateDto);
                await InvokeAsync(StateHasChanged);
            }                                  
            
        }

        else if(args.ActionType == Syncfusion.Blazor.Schedule.ActionType.EventChange)
        {
            var x = args.ChangedRecords.First();
            var input = ObjectMapper.Map<AppointmentDto, AppointmentUpdateDto>(x);
            input.DepartmentId = SelectedDepartmentId;
            input.DoctorId = SelectedDoctorId;
            input.PatientId = SelectedPatientId;           

            await AppointmentsAppService.UpdateAsync(x.Id, input);
            AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);
            await InvokeAsync(StateHasChanged);
        } else if (args.ActionType == ActionType.EventRemove)
        {
            var x = args.DeletedRecords.First();
            await AppointmentsAppService.DeleteAsync(x.Id);
            AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);
            await InvokeAsync(StateHasChanged);
        }
    } 

}
