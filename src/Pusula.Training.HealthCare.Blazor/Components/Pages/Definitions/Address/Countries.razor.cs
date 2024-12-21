using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Countries;
using Pusula.Training.HealthCare.Countries;
using Syncfusion.Blazor.Grids;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions.Address;

public partial class Countries
{
    private SfGrid<CountryDto> SfGrid { get; set; } = null!;

    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];

    private List<CountryDto> CountryList { get; set; } = [];

    private bool AllCountriesSelected { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetCountriesAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Patients"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetCountriesAsync()
    {
        CountryList = await CountryAppService.GetListAsync(new GetCountriesInput());
        await ClearSelection();
    }

#region DataGrid

    private Task ClearSelection()
    {
        AllCountriesSelected = false;
        SfGrid.SelectedRecords.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedCountryRowChangedAsync()
    {
        AllCountriesSelected = SfGrid.SelectedRecords.Count == CountryList.Count;
        return Task.CompletedTask;
    }

#endregion

#region Create

    private CountryCreateDialog CreateCountryDialog { get; set; } = null!;
    private async Task OpenCreateCountryDialogAsync() => await CreateCountryDialog.ShowAsync();

    private async Task CreateCountryAsync(CountryCreateDto patient)
    {
        try
        {
            await CountryAppService.CreateAsync(patient);
            await GetCountriesAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Update

    private CountryUpdateDialog UpdateCountryDialog { get; set; } = null!;
    private Guid EditingCountryId { get; set; }

    private async Task OpenEditCountryModalAsync(CountryDto input)
    {
        EditingCountryId = input.Id;
        await UpdateCountryDialog.ShowAsync(EditingCountryId);
    }

    private async Task UpdateCountryAsync(CountryUpdateDto dto)
    {
        try
        {
            await CountryAppService.UpdateAsync(EditingCountryId, dto);
            await GetCountriesAsync();
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
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await CountryAppService.DeleteAsync(input.Id);
            await GetCountriesAsync();
        }
    }

    private async Task DeleteSelectedCountriesAsync()
    {
        var message = AllCountriesSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        await CountryAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetCountriesAsync();
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