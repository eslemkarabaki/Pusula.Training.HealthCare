using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Medicines
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<MedicineDto> SfGrid { get; set; } = null!;
    private List<MedicineDto> MedicineList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingMedicineId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetMedicinesAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Medicines"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetMedicinesAsync() =>
        MedicineList = await MedicineAppService.GetListAsync(new GetMedicinesInput());

#region Create

    private SfDialog MedicineCreateDialog { get; set; } = null!;
    private MedicineCreateDto MedicineCreateDto { get; set; } = new();

    private async Task OpenMedicineCreateDialogAsync() => await MedicineCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await MedicineAppService.CreateAsync(MedicineCreateDto);
            await GetMedicinesAsync();
            await HideMedicineCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideMedicineCreateDialogAsync()
    {
        await MedicineCreateDialog.HideAsync();
        SetDefaultsForMedicineCreateDialog();
    }

    private void SetDefaultsForMedicineCreateDialog() => MedicineCreateDto = new MedicineCreateDto();

#endregion

#region Update

    private SfDialog MedicineUpdateDialog { get; set; } = null!;
    private MedicineUpdateDto MedicineUpdateDto { get; set; } = new();

    private async Task OpenMedicineUpdateDialogAsync(MedicineDto input)
    {
        EditingMedicineId = input.Id;
        var dto = await MedicineAppService.GetAsync(EditingMedicineId);
        MedicineUpdateDto.Name = dto.Name;
        await MedicineUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await MedicineAppService.UpdateAsync(EditingMedicineId, MedicineUpdateDto);
            await GetMedicinesAsync();
            await HideMedicineUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideMedicineUpdateDialogAsync()
    {
        await MedicineUpdateDialog.HideAsync();
        SetDefaultsForMedicineCreateDialog();
    }

    private void SetDefaultsForMedicineUpdateDialog() => MedicineUpdateDto = new MedicineUpdateDto();

#endregion

    private async Task DeleteAsync(MedicineDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await MedicineAppService.DeleteAsync(input.Id);
            await GetMedicinesAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == MedicineList.Count;
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

        await MedicineAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetMedicinesAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Medicines.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Medicines.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Medicines.Delete);
    }
}