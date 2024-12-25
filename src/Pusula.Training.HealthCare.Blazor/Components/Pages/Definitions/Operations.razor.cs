using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Operations;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Operations
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<OperationDto> SfGrid { get; set; } = null!;
    private List<OperationDto> OperationList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingOperationId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetOperationsAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Operations"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetOperationsAsync() =>
        OperationList = await OperationAppService.GetListAsync(new GetOperationsInput());

#region Create

    private SfDialog OperationCreateDialog { get; set; } = null!;
    private OperationCreateDto OperationCreateDto { get; set; } = new();

    private async Task OpenOperationCreateDialogAsync() => await OperationCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await OperationAppService.CreateAsync(OperationCreateDto);
            await GetOperationsAsync();
            await HideOperationCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideOperationCreateDialogAsync()
    {
        await OperationCreateDialog.HideAsync();
        SetDefaultsForOperationCreateDialog();
    }

    private void SetDefaultsForOperationCreateDialog() => OperationCreateDto = new OperationCreateDto();

#endregion

#region Update

    private SfDialog OperationUpdateDialog { get; set; } = null!;
    private OperationUpdateDto OperationUpdateDto { get; set; } = new();

    private async Task OpenOperationUpdateDialogAsync(OperationDto input)
    {
        EditingOperationId = input.Id;
        var dto = await OperationAppService.GetAsync(EditingOperationId);
        OperationUpdateDto.Name = dto.Name;
        await OperationUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await OperationAppService.UpdateAsync(EditingOperationId, OperationUpdateDto);
            await GetOperationsAsync();
            await HideOperationUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideOperationUpdateDialogAsync()
    {
        await OperationUpdateDialog.HideAsync();
        SetDefaultsForOperationCreateDialog();
    }

    private void SetDefaultsForOperationUpdateDialog() => OperationUpdateDto = new OperationUpdateDto();

#endregion

    private async Task DeleteAsync(OperationDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await OperationAppService.DeleteAsync(input.Id);
            await GetOperationsAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == OperationList.Count;
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

        await OperationAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetOperationsAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Operations.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Operations.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Operations.Delete);
    }
}