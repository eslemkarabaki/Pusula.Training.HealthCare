using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Hospitals
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new();
    protected PageToolbar Toolbar { get; } = new();
    protected bool ShowAdvancedFilters { get; set; }
    private IReadOnlyList<HospitalDto> HospitalList { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }
    private bool CanCreateHospital { get; set; }
    private bool CanEditHospital { get; set; }
    private bool CanDeleteHospital { get; set; }
    private HospitalCreateDto NewHospital { get; set; }
    private Validations NewHospitalValidations { get; set; } = new();
    private HospitalUpdateDto EditingHospital { get; set; }
    private Validations EditingHospitalValidations { get; set; } = new();
    private Guid EditingHospitalId { get; set; }
    private Modal CreateHospitalModal { get; set; } = new();
    private Modal EditHospitalModal { get; set; } = new();
    private GetHospitalsInput Filter { get; set; }
    private DataGridEntityActionsColumn<HospitalDto> EntityActionsColumn { get; set; } = new();

    protected string SelectedCreateTab = "hospital-create-tab";
    protected string SelectedEditTab = "hospital-edit-tab";

    private List<DepartmentDto> DepartmentList { get; set; } = new();
    private IReadOnlyList<string> DepartmentNameList { get; set; } = [];

    private readonly IDepartmentsAppService DepartmentsAppService;

    public Hospitals(IDepartmentsAppService departmentsAppService)
    {
        DepartmentsAppService = departmentsAppService;
        NewHospital = new HospitalCreateDto();
        EditingHospital = new HospitalUpdateDto();
        Filter = new GetHospitalsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
        HospitalList = new List<HospitalDto>();
    }

    private List<HospitalDto> SelectedHospitals { get; set; } = new();
    private bool AllHospitalsSelected { get; set; }

    protected override async Task OnInitializedAsync() => await SetPermissionsAsync();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await SetToolbarItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateHospital = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Hospitals.Create);
        CanEditHospital = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Hospitals.Edit);
        CanDeleteHospital = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Hospitals.Delete);
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Hospitals"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["ExportToExcel"], DownloadAsExcelAsync, IconName.Download);

        Toolbar.AddButton(
            L["NewHospital"], OpenCreateHospitalModalAsync, IconName.Add,
            requiredPolicyName: HealthCarePermissions.Hospitals.Create
        );

        return ValueTask.CompletedTask;
    }

    private async Task GetHospitalsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await HospitalsAppService.GetListAsync(Filter);
        HospitalList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetHospitalsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task DownloadAsExcelAsync()
    {
        var token = (await HospitalsAppService.GetDownloadTokenAsync()).Token;
        var remoteService =
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("HealthCare") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
        var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
        if (!culture.IsNullOrEmpty())
        {
            culture = "&culture=" + culture;
        }

        await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
        NavigationManager.NavigateTo(
            $"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/app/hospitals/as-excel-file?DownloadToken={token}&FilterText={HttpUtility.UrlEncode(Filter.FilterText)}{culture}&Name={HttpUtility.UrlEncode(Filter.Name)}",
            true
        );
    }

    private async Task OnDataGridAsync(DataGridReadDataEventArgs<HospitalDto> e)
    {
        CurrentSorting = e.Columns
                          .Where(c => c.SortDirection != SortDirection.Default)
                          .Select(c => c.Field + " " + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                          .JoinAsString(",");
        CurrentPage = e.Page;

        await GetHospitalsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OpenCreateHospitalModalAsync()
    {
        NewHospital = new HospitalCreateDto();
        SelectedCreateTab = "hospital-create-tab";
        await NewHospitalValidations.ClearAll();
        await LoadDepartmentsAsync();

        DepartmentNameList = new List<string>();
        await CreateHospitalModal.Show();
    }

    private async Task CloseCreateHospitalModalAsync()
    {
        NewHospital = new HospitalCreateDto();
        await CreateHospitalModal.Hide();
    }

    private async Task OpenEditHospitalModalAsync(HospitalDto input)
    {
        SelectedEditTab = "hospital-edit-tab";

        var hospital = await HospitalsAppService.GetAsync(input.Id);

        EditingHospitalId = input.Id;
        EditingHospital = ObjectMapper.Map<HospitalDto, HospitalUpdateDto>(input);

        EditingHospitalValidations.ClearAll();

        await LoadDepartmentsAsync();

        DepartmentNameList = hospital.DepartmentNames.ToList();
        await EditHospitalModal.Show();
    }

    private async Task DeleteHospitalAsync(HospitalDto input)
    {
        await HospitalsAppService.DeleteAsync(input.Id);
        await GetHospitalsAsync();
    }

    private async Task CreateHospitalAsync()
    {
        try
        {
            if (await NewHospitalValidations.ValidateAll())
            {
                NewHospital.DepartmentNames = DepartmentNameList.ToArray();
                await HospitalsAppService.CreateAsync(NewHospital);
                await GetHospitalsAsync();
                await CloseCreateHospitalModalAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task CloseEditHospitalModalAsync() => await EditHospitalModal.Hide();

    private async Task UpdateHospitalAsync()
    {
        try
        {
            if (await EditingHospitalValidations.ValidateAll() == false) return;

            EditingHospital.DepartmentNames = DepartmentNameList.ToArray();
            await HospitalsAppService.UpdateAsync(EditingHospitalId, EditingHospital);
            await GetHospitalsAsync();
            await CloseEditHospitalModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void OnSelectedCreateTabChanged(string name) => SelectedCreateTab = name;

    private void OnSelectedEditTabChanged(string name) => SelectedEditTab = name;

    protected virtual async Task OnNameChangedAsync(string? name)
    {
        Filter.Name = name;
        await SearchAsync();
    }

    private Task SelectAllItems()
    {
        AllHospitalsSelected = true;

        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllHospitalsSelected = false;
        SelectedHospitals.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedHospitalRowsChanged()
    {
        if (SelectedHospitals.Count == HospitalList.Count)
        {
            AllHospitalsSelected = true;
        }

        return Task.CompletedTask;
    }

    private async Task DeleteSelectedHospitalsAsync()
    {
        var message = AllHospitalsSelected ?
            L["DeleteAllRecords"] :
            L["DeleteSelectedRecords", SelectedHospitals.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllHospitalsSelected)
        {
            await HospitalsAppService.DeleteAllAsync(Filter);
        } else
        {
            foreach (var selectedHospital in SelectedHospitals)
            {
                var hospital = await HospitalsAppService.GetAsync(selectedHospital.Id);

                await HospitalsAppService.DeleteAsync(hospital.Id);
            }

            SelectedHospitals.Clear();
            AllHospitalsSelected = false;

            await GetHospitalsAsync();
        }
    }

    private async Task LoadDepartmentsAsync() =>
        DepartmentList = (await DepartmentsAppService.GetListAsync(new GetDepartmentsInput())).Items.ToList();
}