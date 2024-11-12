using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Cities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Districts
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    protected PageToolbar Toolbar { get; } = new();
    protected bool ShowAdvancedFilters { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private IReadOnlyList<DistrictDto> DistrictList { get; set; } = [];
    private IReadOnlyList<CityDto> CityList { get; set; } = [];
    private GetDistrictsInput Filter { get; set; }

    private DataGridEntityActionsColumn<DistrictDto> EntityActionsColumn { get; set; } = new();

    private List<DistrictDto> SelectedDistricts { get; set; } = [];
    private bool AllDistrictsSelected { get; set; }

    public Districts()
    {
        Filter = new GetDistrictsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        CityList = await CityAppService.GetListAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await SetToolbarItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Districts"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        // Toolbar.AddButton(L["ExportToExcel"], DownloadAsExcelAsync, IconName.Download);

        Toolbar.AddButton(L["NewDistrict"], OpenCreateDistrictModalAsync, IconName.Add,
            requiredPolicyName: HealthCarePermissions.Districts.Create);

        return ValueTask.CompletedTask;
    }


    private async Task GetDistrictsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await DistrictAppService.GetListAsync(Filter);
        DistrictList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetDistrictsAsync();
        await InvokeAsync(StateHasChanged);
    }

    // private async Task DownloadAsExcelAsync()
    // {
    //     var token = (await DistrictsAppService.GetDownloadTokenAsync()).Token;
    //     var remoteService =
    //         await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("HealthCare") ??
    //         await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
    //     var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
    //
    //     NavigationManager.NavigateTo(
    //         $"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/app/countrys/as-excel-file?DownloadToken={token}{Filter.ToQueryParameterString(culture)}",
    //         true);
    // }

#region DataGrid

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<DistrictDto> e)
    {
        CurrentSorting = e.Columns
                          .Where(c => c.SortDirection != SortDirection.Default)
                          .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                          .JoinAsString(",");
        CurrentPage = e.Page;
        await GetDistrictsAsync();
        await InvokeAsync(StateHasChanged);
    }


    private Task SelectAllItems()
    {
        AllDistrictsSelected = true;

        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllDistrictsSelected = false;
        SelectedDistricts.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedDistrictRowsChanged()
    {
        AllDistrictsSelected = DistrictList.Count < PageSize
            ? SelectedDistricts.Count == DistrictList.Count
            : SelectedDistricts.Count == PageSize;

        return Task.CompletedTask;
    }

#endregion


#region Create

    private DistrictCreateDto NewDistrict { get; set; } = new();
    private Validations NewDistrictValidations { get; set; } = new();
    private Modal CreateDistrictModal { get; set; } = new();

    protected string SelectedCreateTab = "district-create-tab";

    private async Task OpenCreateDistrictModalAsync()
    {
        NewDistrict = new DistrictCreateDto();
        SelectedCreateTab = "district-create-tab";

        await NewDistrictValidations.ClearAll();
        await CreateDistrictModal.Show();
    }

    private async Task CloseCreateDistrictModalAsync()
    {
        NewDistrict = new DistrictCreateDto();
        await CreateDistrictModal.Hide();
    }

    private async Task CreateDistrictAsync()
    {
        try
        {
            if (await NewDistrictValidations.ValidateAll() == false)
            {
                return;
            }

            await DistrictAppService.CreateAsync(NewDistrict);
            await GetDistrictsAsync();
            await CloseCreateDistrictModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Update

    private DistrictUpdateDto EditingDistrict { get; set; } = new();
    private Validations EditingDistrictValidations { get; set; } = new();
    private Guid EditingDistrictId { get; set; }
    private Modal EditDistrictModal { get; set; } = new();

    protected string SelectedEditTab = "district-edit-tab";

    private async Task OpenEditDistrictModalAsync(DistrictDto input)
    {
        SelectedEditTab = "district-edit-tab";

        var country = await DistrictAppService.GetAsync(input.Id);

        EditingDistrictId = country.Id;
        EditingDistrict = ObjectMapper.Map<DistrictDto, DistrictUpdateDto>(country);

        await EditingDistrictValidations.ClearAll();
        await EditDistrictModal.Show();
    }

    private async Task CloseEditDistrictModalAsync()
    {
        await EditDistrictModal.Hide();
    }

    private async Task UpdateDistrictAsync()
    {
        try
        {
            if (await EditingDistrictValidations.ValidateAll() == false)
            {
                return;
            }

            await DistrictAppService.UpdateAsync(EditingDistrictId, EditingDistrict);
            await GetDistrictsAsync();
            await EditDistrictModal.Hide();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Delete

    private async Task DeleteDistrictAsync(DistrictDto input)
    {
        await DistrictAppService.DeleteAsync(input.Id);
        await GetDistrictsAsync();
    }

    private async Task DeleteSelectedDistrictsAsync()
    {
        var message = AllDistrictsSelected
            ? L["DeleteAllRecords"].Value
            : L["DeleteSelectedRecords", SelectedDistricts.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllDistrictsSelected)
        {
            await DistrictAppService.DeleteAllAsync(Filter);
        }
        else
        {
            await DistrictAppService.DeleteByIdsAsync(SelectedDistricts.Select(x => x.Id).ToList());
        }

        SelectedDistricts.Clear();
        AllDistrictsSelected = false;

        await GetDistrictsAsync();
    }

#endregion

#region Manual Binding Events

    protected virtual async Task OnNameChangedAsync(string? name)
    {
        Filter.Name = name;
        await SearchAsync();
    }

    protected virtual async Task OnCityIdChangedAsync(Guid? cityId)
    {
        Filter.CityId = cityId;
        await SearchAsync();
    }

#endregion

#region Permission

    private bool CanCreateDistrict { get; set; }
    private bool CanEditDistrict { get; set; }
    private bool CanDeleteDistrict { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateDistrict = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Districts.Create);
        CanEditDistrict = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Districts.Edit);
        CanDeleteDistrict = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Districts.Delete);
    }

#endregion
}