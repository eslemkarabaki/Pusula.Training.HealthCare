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
using Polly;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Syncfusion.Blazor.Data;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Appointments
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    private DateTime CurrentDate { get; set; } = DateTime.Today;
    private SfSchedule<AppointmentDto> refSchedule { get; set; }
    private List<AppointmentDto> AppointmentLists { get; set; } = [];
    private List<DoctorDto> Doctors { get; set; } = [];
    private List<DepartmentDto> Departments { get; set; } = [];
    private List<AppointmentTypeDto> AppointmentTypes { get; set; } = [];
    private Guid SelectedPatientId { get; set; } 
    private Guid SelectedDepartmentId { get; set; }
    private Guid SelectedDoctorId { get; set; }
    private string PatientFilter { get; set; } = string.Empty;
    private string ErrorMessage { get; set; } = string.Empty;
    private IReadOnlyList<PatientDto> Patients { get; set; } = [];
    private SfAutoComplete<Guid, PatientDto> refAutoComplate { get; set; }
    private bool valueSelected => SelectedPatientId != Guid.Empty && SelectedDepartmentId != Guid.Empty && SelectedDoctorId != Guid.Empty;

    
    protected async void GetPatientFilter(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        if (args.Text.IsNullOrEmpty())
        {
            return;            
        }

        var patients = await PatientAppService.GetListAsync(new GetPatientsInput { FilterText=args.Text});
        Patients = patients.Items;

        refAutoComplate.DataSource = Patients;
        var query = new Query().Where(new WhereFilter() { Field = "FullName", Operator = "contains", value = args.Text, IgnoreCase = true });
        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
        await refAutoComplate.FilterAsync(Patients, query);
        await InvokeAsync(StateHasChanged);

    }

    private void NavigateToPage()
    {
        NavigationManager.NavigateTo("/patients"); 
    }

    protected override async Task OnInitializedAsync()
    {        
        Departments = await DepartmentsAppService.GetListDepartmentsAsync();
        AppointmentTypes = await AppointmentTypeAppService.GetListAppointmentTypesAsync();
        
        
    }

    protected async void DepartmentSelect(ChangeEventArgs<Guid, DepartmentDto>args)
    {
        SelectedDepartmentId = args.ItemData.Id;
        Doctors = await DoctorsAppService.GetListDoctorsAsync(args.ItemData.Id);
        await InvokeAsync(StateHasChanged);
    }

    protected async void DoctorSelect(ChangeEventArgs<Guid, DoctorDto> args)
    {
        SelectedDoctorId = args.ItemData.Id;
        AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(args.ItemData.Id);
        await InvokeAsync(StateHasChanged);
    }    

    public async void OnActionBegin(ActionEventArgs<AppointmentDto> args)
    {
        if (args.ActionType == Syncfusion.Blazor.Schedule.ActionType.EventCreate) {

            var input=ObjectMapper.Map<AppointmentDto, AppointmentCreateDto>(args.AddedRecords.First());
            input.DepartmentId = SelectedDepartmentId;
            input.DoctorId = SelectedDoctorId;
            input.PatientId = SelectedPatientId;
            await AppointmentsAppService.CreateAsync(input);
            AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);
            await InvokeAsync(StateHasChanged);
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
        }
        else if(args.ActionType == Syncfusion.Blazor.Schedule.ActionType.EventRemove)
        {
            var x = args.DeletedRecords.First();            
            await AppointmentsAppService.DeleteAsync(x.Id);
            AppointmentLists = await AppointmentsAppService.GetListAppointmentsAsync(SelectedDoctorId);
            await InvokeAsync(StateHasChanged);
        }
    }

   


}
