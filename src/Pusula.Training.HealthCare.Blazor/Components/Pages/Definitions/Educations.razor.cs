using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Educations;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Educations
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<EducationDto> SfGrid { get; set; } = null!;
    private List<EducationDto> EducationList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingEducationId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetEducationsAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Educations"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetEducationsAsync() =>
        EducationList = await EducationAppService.GetListAsync(new GetEducationsInput());

#region Create

    private SfDialog EducationCreateDialog { get; set; } = null!;
    private EducationCreateDto EducationCreateDto { get; set; } = new();

    private async Task OpenEducationCreateDialogAsync() => await EducationCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await EducationAppService.CreateAsync(EducationCreateDto);
            await GetEducationsAsync();
            await HideEducationCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideEducationCreateDialogAsync()
    {
        await EducationCreateDialog.HideAsync();
        SetDefaultsForEducationCreateDialog();
    }

    private void SetDefaultsForEducationCreateDialog() => EducationCreateDto = new EducationCreateDto();

#endregion

#region Update

    private SfDialog EducationUpdateDialog { get; set; } = null!;
    private EducationUpdateDto EducationUpdateDto { get; set; } = new();

    private async Task OpenEducationUpdateDialogAsync(EducationDto input)
    {
        EditingEducationId = input.Id;
        var dto = await EducationAppService.GetAsync(EditingEducationId);
        EducationUpdateDto.Name = dto.Name;
        await EducationUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await EducationAppService.UpdateAsync(EditingEducationId, EducationUpdateDto);
            await GetEducationsAsync();
            await HideEducationUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideEducationUpdateDialogAsync()
    {
        await EducationUpdateDialog.HideAsync();
        SetDefaultsForEducationCreateDialog();
    }

    private void SetDefaultsForEducationUpdateDialog() => EducationUpdateDto = new EducationUpdateDto();

#endregion

    private async Task DeleteAsync(EducationDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await EducationAppService.DeleteAsync(input.Id);
            await GetEducationsAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == EducationList.Count;
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

        await EducationAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetEducationsAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Educations.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Educations.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Educations.Delete);
    }
}