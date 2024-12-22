using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Vaccines;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Vaccines
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<VaccineDto> SfGrid { get; set; } = null!;
    private List<VaccineDto> VaccineList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingVaccineId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetVaccinesAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Vaccines"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetVaccinesAsync() => VaccineList = await VaccineAppService.GetListAsync(new GetVaccinesInput());

#region Create

    private SfDialog VaccineCreateDialog { get; set; } = null!;
    private VaccineCreateDto VaccineCreateDto { get; set; } = new();

    private async Task OpenVaccineCreateDialogAsync() => await VaccineCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await VaccineAppService.CreateAsync(VaccineCreateDto);
            await GetVaccinesAsync();
            await HideVaccineCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideVaccineCreateDialogAsync()
    {
        await VaccineCreateDialog.HideAsync();
        SetDefaultsForVaccineCreateDialog();
    }

    private void SetDefaultsForVaccineCreateDialog() => VaccineCreateDto = new VaccineCreateDto();

#endregion

#region Update

    private SfDialog VaccineUpdateDialog { get; set; } = null!;
    private VaccineUpdateDto VaccineUpdateDto { get; set; } = new();

    private async Task OpenVaccineUpdateDialogAsync(VaccineDto input)
    {
        EditingVaccineId = input.Id;
        var dto = await VaccineAppService.GetAsync(EditingVaccineId);
        VaccineUpdateDto.Name = dto.Name;
        await VaccineUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await VaccineAppService.UpdateAsync(EditingVaccineId, VaccineUpdateDto);
            await GetVaccinesAsync();
            await HideVaccineUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideVaccineUpdateDialogAsync()
    {
        await VaccineUpdateDialog.HideAsync();
        SetDefaultsForVaccineCreateDialog();
    }

    private void SetDefaultsForVaccineUpdateDialog() => VaccineUpdateDto = new VaccineUpdateDto();

#endregion

    private async Task DeleteAsync(VaccineDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await VaccineAppService.DeleteAsync(input.Id);
            await GetVaccinesAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == VaccineList.Count;
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

        await VaccineAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetVaccinesAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Vaccines.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Vaccines.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Vaccines.Delete);
    }
}