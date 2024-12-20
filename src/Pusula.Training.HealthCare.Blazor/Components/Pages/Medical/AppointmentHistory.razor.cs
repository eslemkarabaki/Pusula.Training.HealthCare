using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.PivotView;
using Syncfusion.EJ2.FileManager.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Medical;

public partial class AppointmentHistory 
{
    [Parameter]
    public int ProtocolNo { get; set; }

    private IReadOnlyList<AppointmentWithNavigationPropertiesDto> AppointmentList = [];    

    [CascadingParameter]
    private ProtocolDto Protocol { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await GetAppointmentsAsync();
    }

    private async Task GetAppointmentsAsync()
    {
        AppointmentList = await AppointmentAppService.GetPatientWaitingAppointmentsAsync(Protocol.PatientId);
                
    }

}