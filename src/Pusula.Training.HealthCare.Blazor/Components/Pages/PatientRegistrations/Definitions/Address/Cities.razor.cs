using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Countries;
using Pusula.Training.HealthCare.Countries;
using Syncfusion.Blazor.Grids;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Cities;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientRegistrations.Definitions.Address;

public partial class Cities
{
    private SfGrid<CityDto> SfGrid { get; set; } = null!;

    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];

    private List<CityDto> CityList { get; set; } = [];

    private bool AllCitiesSelected { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetCitiesAsync();
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

    private async Task GetCitiesAsync()
    {
        CityList = await CityAppService.GetListWithDetailsAsync();
        await ClearSelection();
    }

    #region DataGrid

    private Task ClearSelection()
    {
        AllCitiesSelected = false;
        SfGrid.SelectedRecords.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedCityRowChangedAsync()
    {
        AllCitiesSelected = SfGrid.SelectedRecords.Count == CityList.Count;
        return Task.CompletedTask;
    }

    #endregion

    #region Create

    private CityCreateDialog CreateCityDialog { get; set; } = null!;
    private async Task OpenCreateCityDialogAsync() => await CreateCityDialog.ShowAsync();

    private async Task CreateCityAsync(CityCreateDto patient)
    {
        try
        {
            await CityAppService.CreateAsync(patient);
            await GetCitiesAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    #endregion

    #region Update

    private CityUpdateDialog UpdateCityDialog { get; set; } = null!;
    private Guid EditingCityId { get; set; }

    private async Task OpenEditCityModalAsync(CityDto input)
    {
        EditingCityId = input.Id;
        await UpdateCityDialog.ShowAsync(EditingCityId);
    }

    private async Task UpdateCityAsync(CityUpdateDto dto)
    {
        try
        {
            await CityAppService.UpdateAsync(EditingCityId, dto);
            await GetCitiesAsync();
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
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await CityAppService.DeleteAsync(input.Id);
            await GetCitiesAsync();
        }
    }

    private async Task DeleteSelectedCitiesAsync()
    {
        var message = AllCitiesSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        await CityAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetCitiesAsync();
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

