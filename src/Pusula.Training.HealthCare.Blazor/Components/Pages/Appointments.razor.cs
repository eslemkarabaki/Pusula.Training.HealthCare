using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.AppointmentTypes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Schedule;
using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.Schedule.Internal;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Countries;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;

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
    private IReadOnlyList<PatientDto> Patients { get; set; } = [];
    private Guid SelectedDepartmentId { get; set; }
    private Guid SelectedDoctorId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Departments = await DepartmentsAppService.GetListDepartmentsAsync();
        AppointmentTypes = await AppointmentTypeAppService.GetListAppointmentTypesAsync();
        var patients = await PatientAppService.GetListAsync(new());
        Patients = patients.Items;
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
