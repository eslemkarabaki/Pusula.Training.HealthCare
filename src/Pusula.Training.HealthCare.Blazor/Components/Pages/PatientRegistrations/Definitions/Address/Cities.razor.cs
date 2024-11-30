using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientRegistrations.Definitions.Address;

public partial class Cities
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    protected PageToolbar Toolbar { get; } = new();
    protected bool ShowAdvancedFilters { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private IReadOnlyList<CityDto> CityList { get; set; } = [];
    private IReadOnlyList<CountryDto> CountryList { get; set; } = [];
    private GetCitiesInput Filter { get; set; }

    private DataGridEntityActionsColumn<CityDto> EntityActionsColumn { get; set; } = new();

    private List<CityDto> SelectedCities { get; set; } = [];
    private bool AllCitiesSelected { get; set; }

    public Cities() =>
        Filter = new GetCitiesInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        CountryList = await CountryAppService.GetListAsync();
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
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Cities"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        // Toolbar.AddButton(L["ExportToExcel"], DownloadAsExcelAsync, IconName.Download);

        Toolbar.AddButton(
            L["NewCity"], OpenCreateCityModalAsync, IconName.Add,
            requiredPolicyName: HealthCarePermissions.Cities.Create
        );

        return ValueTask.CompletedTask;
    }

    private async Task GetCitiesAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await CityAppService.GetListAsync(Filter);
        CityList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetCitiesAsync();
        await InvokeAsync(StateHasChanged);
    }

    // private async Task DownloadAsExcelAsync()
    // {
    //     var token = (await CitiesAppService.GetDownloadTokenAsync()).Token;
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

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CityDto> e)
    {
        CurrentSorting = e
                         .Columns
                         .Where(c => c.SortDirection != SortDirection.Default)
                         .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                         .JoinAsString(",");
        CurrentPage = e.Page;
        await GetCitiesAsync();
        await InvokeAsync(StateHasChanged);
    }

    private Task SelectAllItems()
    {
        AllCitiesSelected = true;
        SelectedCities.AddRange(CityList.Except(SelectedCities));
        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllCitiesSelected = false;
        SelectedCities.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedCityRowsChanged()
    {
        AllCitiesSelected = CityList.Count < PageSize ?
            SelectedCities.Count == CityList.Count :
            SelectedCities.Count == PageSize;

        return Task.CompletedTask;
    }

#endregion

#region Create

    private CityCreateDto NewCity { get; set; } = new();
    private Validations NewCityValidations { get; set; } = new();
    private Modal CreateCityModal { get; set; } = new();

    protected string SelectedCreateTab = "city-create-tab";

    private async Task OpenCreateCityModalAsync()
    {
        NewCity = new CityCreateDto();
        SelectedCreateTab = "city-create-tab";

        await NewCityValidations.ClearAll();
        await CreateCityModal.Show();
    }

    private async Task CloseCreateCityModalAsync()
    {
        NewCity = new CityCreateDto();
        await CreateCityModal.Hide();
    }

    private async Task CreateCityAsync()
    {
        try
        {
            if (await NewCityValidations.ValidateAll() == false)
            {
                return;
            }

            await CityAppService.CreateAsync(NewCity);
            await GetCitiesAsync();
            await CloseCreateCityModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Update

    private CityUpdateDto EditingCity { get; set; } = new();
    private Validations EditingCityValidations { get; set; } = new();
    private Guid EditingCityId { get; set; }
    private Modal EditCityModal { get; set; } = new();

    protected string SelectedEditTab = "city-edit-tab";

    private async Task OpenEditCityModalAsync(CityDto input)
    {
        SelectedEditTab = "city-edit-tab";

        var country = await CityAppService.GetAsync(input.Id);

        EditingCityId = country.Id;
        EditingCity = ObjectMapper.Map<CityDto, CityUpdateDto>(country);

        await EditingCityValidations.ClearAll();
        await EditCityModal.Show();
    }

    private async Task CloseEditCityModalAsync() => await EditCityModal.Hide();

    private async Task UpdateCityAsync()
    {
        try
        {
            if (await EditingCityValidations.ValidateAll() == false)
            {
                return;
            }

            await CityAppService.UpdateAsync(EditingCityId, EditingCity);
            await GetCitiesAsync();
            await EditCityModal.Hide();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Delete

    private async Task DeleteCityAsync(CityDto input)
    {
        await CityAppService.DeleteAsync(input.Id);
        await GetCitiesAsync();
    }

    private async Task DeleteSelectedCitiesAsync()
    {
        var message = AllCitiesSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SelectedCities.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllCitiesSelected)
        {
            await CityAppService.DeleteAllAsync(Filter);
        } else
        {
            await CityAppService.DeleteByIdsAsync(SelectedCities.Select(x => x.Id).ToList());
        }

        SelectedCities.Clear();
        AllCitiesSelected = false;

        await GetCitiesAsync();
    }

#endregion

#region Manual Binding Events

    protected virtual async Task OnNameChangedAsync(string? name)
    {
        Filter.Name = name;
        await SearchAsync();
    }

    protected virtual async Task OnCountryIdChangedAsync(Guid? countryId)
    {
        Filter.CountryId = countryId;
        await SearchAsync();
    }

#endregion

#region Permission

    private bool CanCreateCity { get; set; }
    private bool CanEditCity { get; set; }
    private bool CanDeleteCity { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateCity = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Cities.Create);
        CanEditCity = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Cities.Edit);
        CanDeleteCity = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Cities.Delete);
    }

#endregion
}