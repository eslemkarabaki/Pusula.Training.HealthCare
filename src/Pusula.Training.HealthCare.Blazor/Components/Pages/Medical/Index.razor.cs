using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Blazor.Models;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using Syncfusion.Blazor.Buttons;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Medical;

public partial class Index
{
    private DoctorDto Doctor { get; set; }
    private IReadOnlyList<ProtocolDto> DoctorWorkList { get; set; } = [];
    private IReadOnlyList<AppointmentWithNavigationPropertiesDto> DoctorAppointmentList { get; set; } = [];
    private GetDoctorWorkListInput DoctorWorkListInput { get; set; } = null!;
    private GetDoctorAppointmentListInput DoctorAppointmentListInput { get; set; } = null!;

    private readonly DateTime _today;
    private readonly DateTime _tomorrow;
    private readonly DateTime _thisWeekStart;
    private readonly DateTime _thisMonthStart;

    private bool PageInitialized { get; set; }

    private EnumDoctorWorkListOption SelectedEnumDoctorWorkListOption { get; set; } = EnumDoctorWorkListOption.WorkList;

    public Index()
    {
        _today = DateTime.Today;
        _tomorrow = _today.AddDays(1).AddMinutes(-1);
        var todayIsSaturday = _today.DayOfWeek is DayOfWeek.Saturday; // cumartesi
        var todayIsSunday = _today.DayOfWeek is DayOfWeek.Sunday;     // pazar
        _thisWeekStart =
            todayIsSaturday ? _today.AddDays(2) :
            todayIsSunday ? _today.AddDays(1) : _today.AddDays(-((int)_today.DayOfWeek - 1));
        _thisMonthStart = new DateTime(_today.Year, _today.Month, 1);
    }

    protected override async Task OnInitializedAsync()
    {
        Doctor = await DoctorAppService.GetAsync(new GetDoctorInput(UserId: CurrentUser.Id));
        DoctorWorkListInput = new GetDoctorWorkListInput(Doctor.Id)
        {
            StartTime = _today,
            EndTime = _tomorrow,
            Sorting = $"{nameof(ProtocolDto.StartTime)} desc"
        };
        DoctorAppointmentListInput = new GetDoctorAppointmentListInput(Doctor.Id)
        {
            StartTime = _today,
            EndTime = _tomorrow,
            Sorting = $"Appointment.{nameof(AppointmentDto.StartTime)} desc"
        };
        await SearchAsync();
        PageInitialized = true;
    }

    private async Task SelectedEnumDoctorWorkListOptionChangedAsync(ChangeArgs<EnumDoctorWorkListOption> args)
    {
        if (args.Value == SelectedEnumDoctorWorkListOption) return;

        SelectedEnumDoctorWorkListOption = args.Value;
        await SearchAsync();
    }

    private async Task WorkListStatusHasChangedAsync(EnumProtocolStatus status)
    {
        DoctorWorkListInput.ToggleStatus(status);
        await GetDoctorWorkListAsync();
    }

    private async Task GetDoctorWorkListAsync()
    {
        var result = await ProtocolAppService.GetDoctorWorkListWithDetailsAsync(DoctorWorkListInput);
        DoctorWorkList = result.Items;
    }

    private async Task AppointmentStatusHasChangedAsync(EnumAppointmentStatus status)
    {
        DoctorAppointmentListInput.ToggleStatus(status);
        await GetDoctorAppointmentListAsync();
    }

    private async Task GetDoctorAppointmentListAsync()
    {
        var result =
            await AppointmentAppService.GetDoctorAppointmentListWithNavigationPropertiesAsync(
                DoctorAppointmentListInput
            );
        DoctorAppointmentList = result.Items;
    }

    private async Task SearchAsync()
    {
        switch (SelectedEnumDoctorWorkListOption)
        {
            case EnumDoctorWorkListOption.WorkList:
                await GetDoctorWorkListAsync();
                break;
            case EnumDoctorWorkListOption.Appointment:
                await GetDoctorAppointmentListAsync();
                break;
        }

        await InvokeAsync(StateHasChanged);
    }

    private void NavigateToMedicalCard(int protocolNo) =>
        NavigationManager.NavigateTo($"medical/medical-card/{protocolNo}/dashboard");
}