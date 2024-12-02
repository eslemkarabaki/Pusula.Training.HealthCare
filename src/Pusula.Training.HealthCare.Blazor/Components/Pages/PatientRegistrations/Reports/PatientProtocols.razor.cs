using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypes;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using FilteringEventArgs = Syncfusion.Blazor.DropDowns.FilteringEventArgs;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientRegistrations.Reports;

public partial class PatientProtocols
{
    private EditContext FilterContext { get; set; }
    private GetProtocolsInput Filter { get; set; }
    private int PageSize => 50;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private DateTime Today => DateTime.Today;
    private DateTime Tomorrow => Today.AddDays(1).AddMinutes(-1);
    private bool TodayIsSaturday => Today.DayOfWeek is DayOfWeek.Saturday; // cumartesi
    private bool TodayIsSunday => Today.DayOfWeek is DayOfWeek.Sunday;     // pazar

    private DateTime ThisWeekStart =>
        TodayIsSaturday ? Today.AddDays(2) :
        TodayIsSunday ? Today.AddDays(1) : Today.AddDays(-((int)Today.DayOfWeek - 1));

    private DateTime ThisMonthStart => new(Today.Year, Today.Month, 1);

    private IEnumerable<ProtocolTypeDto> ProtocolTypeList { get; set; } = [];
    private IReadOnlyList<ProtocolDto> ProtocolList { get; set; } = [];

    private SfGrid<ProtocolDto> SfGrid { get; set; } = null!;

    public PatientProtocols()
    {
        Filter = new GetProtocolsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting,
            StartTime = Today,
            EndTime = Tomorrow
        };
        FilterContext = new EditContext(Filter);
    }

    protected override async Task OnInitializedAsync() =>
        ProtocolTypeList = await ProtocolTypeAppService.GetListAsync();

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
    }

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