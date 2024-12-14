using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Grids;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Districts;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientRegistrations.Definitions.Address;

public partial class Districts
{
    private SfGrid<DistrictDto> SfGrid { get; set; } = null!;

    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];

    private List<DistrictDto> DistrictList { get; set; } = [];

    private bool AllDistrictsSelected { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetDistrictsAsync();
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

    private async Task GetDistrictsAsync()
    {
        DistrictList = await DistrictAppService.GetListWithDetailsAsync(new GetDistrictsInput());
        await ClearSelection();
    }

#region DataGrid

    private Task ClearSelection()
    {
        AllDistrictsSelected = false;
        SfGrid.SelectedRecords.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedDistrictRowChangedAsync()
    {
        AllDistrictsSelected = SfGrid.SelectedRecords.Count == DistrictList.Count;
        return Task.CompletedTask;
    }

#endregion

#region Create

    private DistrictCreateDialog CreateDistrictDialog { get; set; } = null!;
    private async Task OpenCreateDistrictDialogAsync() => await CreateDistrictDialog.ShowAsync();

    private async Task CreateDistrictAsync(DistrictCreateDto patient)
    {
        try
        {
            await DistrictAppService.CreateAsync(patient);
            await GetDistrictsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Update

    private DistrictUpdateDialog UpdateDistrictDialog { get; set; } = null!;
    private Guid EditingDistrictId { get; set; }

    private async Task OpenEditDistrictModalAsync(DistrictDto input)
    {
        EditingDistrictId = input.Id;
        await UpdateDistrictDialog.ShowAsync(EditingDistrictId);
    }

    private async Task UpdateDistrictAsync(DistrictUpdateDto dto)
    {
        try
        {
            await DistrictAppService.UpdateAsync(EditingDistrictId, dto);
            await GetDistrictsAsync();
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
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await DistrictAppService.DeleteAsync(input.Id);
            await GetDistrictsAsync();
        }
    }

    private async Task DeleteSelectedDistrictsAsync()
    {
        var message = AllDistrictsSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        await DistrictAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetDistrictsAsync();
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