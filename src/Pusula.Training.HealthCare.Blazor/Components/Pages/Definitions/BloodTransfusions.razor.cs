using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.BloodTransfusions;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class BloodTransfusions
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<BloodTransfusionDto> SfGrid { get; set; } = null!;
    private List<BloodTransfusionDto> BloodTransfusionList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingBloodTransfusionId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetBloodTransfusionsAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["BloodTransfusions"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetBloodTransfusionsAsync() =>
        BloodTransfusionList = await BloodTransfusionAppService.GetListAsync(new GetBloodTransfusionsInput());

#region Create

    private SfDialog BloodTransfusionCreateDialog { get; set; } = null!;
    private BloodTransfusionCreateDto BloodTransfusionCreateDto { get; set; } = new();

    private async Task OpenBloodTransfusionCreateDialogAsync() => await BloodTransfusionCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await BloodTransfusionAppService.CreateAsync(BloodTransfusionCreateDto);
            await GetBloodTransfusionsAsync();
            await HideBloodTransfusionCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideBloodTransfusionCreateDialogAsync()
    {
        await BloodTransfusionCreateDialog.HideAsync();
        SetDefaultsForBloodTransfusionCreateDialog();
    }

    private void SetDefaultsForBloodTransfusionCreateDialog() =>
        BloodTransfusionCreateDto = new BloodTransfusionCreateDto();

#endregion

#region Update

    private SfDialog BloodTransfusionUpdateDialog { get; set; } = null!;
    private BloodTransfusionUpdateDto BloodTransfusionUpdateDto { get; set; } = new();

    private async Task OpenBloodTransfusionUpdateDialogAsync(BloodTransfusionDto input)
    {
        EditingBloodTransfusionId = input.Id;
        var dto = await BloodTransfusionAppService.GetAsync(EditingBloodTransfusionId);
        BloodTransfusionUpdateDto.Name = dto.Name;
        await BloodTransfusionUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await BloodTransfusionAppService.UpdateAsync(EditingBloodTransfusionId, BloodTransfusionUpdateDto);
            await GetBloodTransfusionsAsync();
            await HideBloodTransfusionUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideBloodTransfusionUpdateDialogAsync()
    {
        await BloodTransfusionUpdateDialog.HideAsync();
        SetDefaultsForBloodTransfusionCreateDialog();
    }

    private void SetDefaultsForBloodTransfusionUpdateDialog() =>
        BloodTransfusionUpdateDto = new BloodTransfusionUpdateDto();

#endregion

    private async Task DeleteAsync(BloodTransfusionDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await BloodTransfusionAppService.DeleteAsync(input.Id);
            await GetBloodTransfusionsAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == BloodTransfusionList.Count;
        return Task.CompletedTask;
    }

    private async Task DeleteSelectedRowsAsync()
    {
        var message = AllRowSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        await BloodTransfusionAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetBloodTransfusionsAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.BloodTransfusions.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.BloodTransfusions.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.BloodTransfusions.Delete);
    }
}