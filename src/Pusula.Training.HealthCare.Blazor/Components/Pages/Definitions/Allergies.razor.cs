using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Allergies;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Allergies
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<AllergyDto> SfGrid { get; set; } = null!;
    private List<AllergyDto> AllergyList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingAllergyId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetAllergiesAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Allergies"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetAllergiesAsync() =>
        AllergyList = await AllergyAppService.GetListAsync(new GetAllergiesInput());

#region Create

    private SfDialog AllergyCreateDialog { get; set; } = null!;
    private AllergyCreateDto AllergyCreateDto { get; set; } = new();

    private async Task OpenAllergyCreateDialogAsync() => await AllergyCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await AllergyAppService.CreateAsync(AllergyCreateDto);
            await GetAllergiesAsync();
            await HideAllergyCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideAllergyCreateDialogAsync()
    {
        await AllergyCreateDialog.HideAsync();
        SetDefaultsForAllergyCreateDialog();
    }

    private void SetDefaultsForAllergyCreateDialog() => AllergyCreateDto = new AllergyCreateDto();

#endregion

#region Update

    private SfDialog AllergyUpdateDialog { get; set; } = null!;
    private AllergyUpdateDto AllergyUpdateDto { get; set; } = new();

    private async Task OpenAllergyUpdateDialogAsync(AllergyDto input)
    {
        EditingAllergyId = input.Id;
        var dto = await AllergyAppService.GetAsync(EditingAllergyId);
        AllergyUpdateDto.Name = dto.Name;
        await AllergyUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await AllergyAppService.UpdateAsync(EditingAllergyId, AllergyUpdateDto);
            await GetAllergiesAsync();
            await HideAllergyUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideAllergyUpdateDialogAsync()
    {
        await AllergyUpdateDialog.HideAsync();
        SetDefaultsForAllergyCreateDialog();
    }

    private void SetDefaultsForAllergyUpdateDialog() => AllergyUpdateDto = new AllergyUpdateDto();

#endregion

    private async Task DeleteAsync(AllergyDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await AllergyAppService.DeleteAsync(input.Id);
            await GetAllergiesAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == AllergyList.Count;
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

        await AllergyAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetAllergiesAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Allergies.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Allergies.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Allergies.Delete);
    }
}