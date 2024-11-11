using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Countries
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    protected PageToolbar Toolbar { get; } = new();
    protected bool ShowAdvancedFilters { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private IReadOnlyList<CountryDto> CountryList { get; set; } = [];
    private GetCountriesInput Filter { get; set; }

    private DataGridEntityActionsColumn<CountryDto> EntityActionsColumn { get; set; } = new();

    private List<CountryDto> SelectedCountries { get; set; } = [];
    private bool AllCountriesSelected { get; set; }

    public Countries()
    {
        Filter = new GetCountriesInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
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
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Countries"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        // Toolbar.AddButton(L["ExportToExcel"], DownloadAsExcelAsync, IconName.Download);

        Toolbar.AddButton(L["NewCountry"], OpenCreateCountryModalAsync, IconName.Add,
            requiredPolicyName: HealthCarePermissions.Countries.Create);

        return ValueTask.CompletedTask;
    }


    private async Task GetCountriesAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await CountryAppService.GetListAsync(Filter);
        CountryList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetCountriesAsync();
        await InvokeAsync(StateHasChanged);
    }

    // private async Task DownloadAsExcelAsync()
    // {
    //     var token = (await CountriesAppService.GetDownloadTokenAsync()).Token;
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

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CountryDto> e)
    {
        CurrentSorting = e.Columns
                          .Where(c => c.SortDirection != SortDirection.Default)
                          .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                          .JoinAsString(",");
        CurrentPage = e.Page;
        await GetCountriesAsync();
        await InvokeAsync(StateHasChanged);
    }


    private Task SelectAllItems()
    {
        AllCountriesSelected = true;

        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllCountriesSelected = false;
        SelectedCountries.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedCountryRowsChanged()
    {
        AllCountriesSelected = CountryList.Count < PageSize
            ? SelectedCountries.Count == CountryList.Count
            : SelectedCountries.Count == PageSize;

        return Task.CompletedTask;
    }

#endregion


#region Create

    private CountryCreateDto NewCountry { get; set; } = new();
    private Validations NewCountryValidations { get; set; } = new();
    private Modal CreateCountryModal { get; set; } = new();

    protected string SelectedCreateTab = "country-create-tab";

    private async Task OpenCreateCountryModalAsync()
    {
        NewCountry = new CountryCreateDto();
        SelectedCreateTab = "country-create-tab";

        await NewCountryValidations.ClearAll();
        await CreateCountryModal.Show();
    }

    private async Task CloseCreateCountryModalAsync()
    {
        NewCountry = new CountryCreateDto();
        await CreateCountryModal.Hide();
    }

    private async Task CreateCountryAsync()
    {
        try
        {
            if (await NewCountryValidations.ValidateAll() == false)
            {
                return;
            }

            await CountryAppService.CreateAsync(NewCountry);
            await GetCountriesAsync();
            await CloseCreateCountryModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Update

    private CountryUpdateDto EditingCountry { get; set; } = new();
    private Validations EditingCountryValidations { get; set; } = new();
    private Guid EditingCountryId { get; set; }
    private Modal EditCountryModal { get; set; } = new();

    protected string SelectedEditTab = "country-edit-tab";

    private async Task OpenEditCountryModalAsync(CountryDto input)
    {
        SelectedEditTab = "country-edit-tab";

        var country = await CountryAppService.GetAsync(input.Id);

        EditingCountryId = country.Id;
        EditingCountry = ObjectMapper.Map<CountryDto, CountryUpdateDto>(country);

        await EditingCountryValidations.ClearAll();
        await EditCountryModal.Show();
    }

    private async Task CloseEditCountryModalAsync()
    {
        await EditCountryModal.Hide();
    }

    private async Task UpdateCountryAsync()
    {
        try
        {
            if (await EditingCountryValidations.ValidateAll() == false)
            {
                return;
            }

            await CountryAppService.UpdateAsync(EditingCountryId, EditingCountry);
            await GetCountriesAsync();
            await EditCountryModal.Hide();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Delete

    private async Task DeleteCountryAsync(CountryDto input)
    {
        await CountryAppService.DeleteAsync(input.Id);
        await GetCountriesAsync();
    }

    private async Task DeleteSelectedCountriesAsync()
    {
        var message = AllCountriesSelected
            ? L["DeleteAllRecords"].Value
            : L["DeleteSelectedRecords", SelectedCountries.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllCountriesSelected)
        {
            await CountryAppService.DeleteAllAsync(Filter);
        }
        else
        {
            await CountryAppService.DeleteByIdsAsync(SelectedCountries.Select(x => x.Id).ToList());
        }

        SelectedCountries.Clear();
        AllCountriesSelected = false;

        await GetCountriesAsync();
    }

#endregion

#region Manual Binding Events

    protected virtual async Task OnNameChangedAsync(string? name)
    {
        Filter.Name = name;
        await SearchAsync();
    }

    protected virtual async Task OnAbbreviationChangedAsync(string? abbreviation)
    {
        Filter.Abbreviation = abbreviation;
        await SearchAsync();
    }

#endregion

#region Permission

    private bool CanCreateCountry { get; set; }
    private bool CanEditCountry { get; set; }
    private bool CanDeleteCountry { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateCountry = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Countries.Create);
        CanEditCountry = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Countries.Edit);
        CanDeleteCountry = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Countries.Delete);
    }

#endregion
}