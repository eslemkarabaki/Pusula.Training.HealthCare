using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Blazor.Models;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypes;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using FilteringEventArgs = Syncfusion.Blazor.DropDowns.FilteringEventArgs;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientRegistrations.Reports;

public partial class PatientProtocols
{
    private EditContext FilterContext { get; set; }
    private GetProtocolsInput Filter { get; set; }
    private int PageSize => 100;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private readonly DateTime _today;
    private readonly DateTime _tomorrow;
    private readonly DateTime _thisWeekStart;
    private readonly DateTime _thisMonthStart;
    private IEnumerable<ProtocolTypeDto> ProtocolTypeList { get; set; } = [];
    private IReadOnlyList<ProtocolDto> ProtocolList { get; set; } = [];

    private SfGrid<ProtocolDto> SfGrid { get; set; } = null!;
    private IEnumerable<SfPieChartModel>? DepartmentPieChartData { get; set; }
    private IEnumerable<SfPieChartModel>? StatusPieChartData { get; set; }
    private string? SelectedPieChartDepartment { get; set; }

    public PatientProtocols()
    {
        _today = DateTime.Today;
        _tomorrow = _today.AddDays(1).AddMinutes(-1);
        var todayIsSaturday = _today.DayOfWeek is DayOfWeek.Saturday; // cumartesi
        var todayIsSunday = _today.DayOfWeek is DayOfWeek.Sunday;     // pazar
        _thisWeekStart =
            todayIsSaturday ? _today.AddDays(2) :
            todayIsSunday ? _today.AddDays(1) : _today.AddDays(-((int)_today.DayOfWeek - 1));
        _thisMonthStart = new DateTime(_today.Year, _today.Month, 1);

        Filter = new GetProtocolsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting,
            StartTime = _today,
            EndTime = _tomorrow
        };
        FilterContext = new EditContext(Filter);
    }

    protected override async Task OnInitializedAsync()
    {
        ProtocolTypeList = await ProtocolTypeAppService.GetListAsync();
        await GetProtocolsAsync();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetProtocolsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task GetProtocolsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await ProtocolAppService.GetListWithDetailsAsync(Filter);
        ProtocolList = result.Items;
        TotalCount = (int)result.TotalCount;
        DepartmentPieChartData = SfPieChartModel.FromGroup(
            ProtocolList.Count, ProtocolList.GroupBy(e => e.DepartmentId), e => e.Department.Name
        );
        FillStatusPieChartData();
    }

    private void FillStatusChartDataWithSelectedDepartment(AccumulationPointEventArgs args)
    {
        SelectedPieChartDepartment = (string)args.Point.X;
        StatusPieChartData = SfPieChartModel.FromGroup(
            ProtocolList.Count(e => e.Department.Name == SelectedPieChartDepartment),
            ProtocolList.Where(e => e.Department.Name == SelectedPieChartDepartment).GroupBy(e => e.Status),
            e => e.Status.ToString()
        );
        StateHasChanged();
    }

    private void FillStatusPieChartData()
    {
        SelectedPieChartDepartment = null;
        StatusPieChartData = SfPieChartModel.FromGroup(
            ProtocolList.Count, ProtocolList.GroupBy(e => e.Status), e => e.Status.ToString()
        );
    }

#region Dashboard

    private readonly double[] _spacing = [10, 10];

#endregion

#region Doctor

    private IEnumerable<DoctorDto> DoctorList { get; set; } = [];
    private SfAutoComplete<Guid?, DoctorDto> DoctorFilterAutoComplete { get; set; } = null!;
    private GetDoctorsInput GetDoctorsInput { get; set; } = new() { MaxResultCount = 10 };

    private async Task FilterDoctorAsync(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        GetDoctorsInput.FullName = args.Text;
        var doctors = await DoctorAppService.GetListAsync(GetDoctorsInput);
        DoctorList = doctors.Items;
        await DoctorFilterAutoComplete.FilterAsync(DoctorList);
    }

#endregion

#region Department

    private IEnumerable<DepartmentDto> DepartmentList { get; set; } = [];
    private SfAutoComplete<Guid?, DepartmentDto> DepartmentFilterAutoComplete { get; set; } = null!;
    private GetDepartmentsInput GetDepartmentsInput { get; set; } = new() { MaxResultCount = 10 };

    private async Task FilterDepartmentAsync(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        GetDepartmentsInput.Name = args.Text;
        var departments = await DepartmentsAppService.GetListAsync(GetDepartmentsInput);
        DepartmentList = departments.Items;
        await DepartmentFilterAutoComplete.FilterAsync(DepartmentList);
    }

#endregion

#region Permission

    private bool CanCreateProtocol { get; set; }
    private bool CanEditProtocol { get; set; }
    private bool CanDeleteProtocol { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateProtocol = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Protocols.Create);
        CanEditProtocol = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Protocols.Edit);
        CanDeleteProtocol = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Protocols.Delete);
    }

#endregion
}