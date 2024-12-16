using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Protocols;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Doctor;

public partial class WorkList
{
    private IReadOnlyList<ProtocolDto> DoctorWorkList { get; set; } = [];
    private GetDoctorWorkListInput DoctorWorkListInput { get; set; } = null!;

    private readonly DateTime _today;
    private readonly DateTime _tomorrow;
    private readonly DateTime _thisWeekStart;
    private readonly DateTime _thisMonthStart;

    public WorkList()
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
        DoctorWorkListInput = new GetDoctorWorkListInput(CurrentUser.Id!.Value)
        {
            StartTime = _today,
            EndTime = _tomorrow
        };
        await GetDoctorWorkListAsync();
    }

    private async Task GetDoctorWorkListAsync()
    {
        var result = await ProtocolAppService.GetDoctorWorkListWithDetailsAsync(DoctorWorkListInput);
        DoctorWorkList = result.Items;
    }

    protected virtual async Task SearchAsync()
    {
        await GetDoctorWorkListAsync();
        await InvokeAsync(StateHasChanged);
    }
}