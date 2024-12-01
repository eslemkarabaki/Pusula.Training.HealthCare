using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Countries;
using Syncfusion.Blazor.Grids;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.AppointmentTypes;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class AppointmentTypes
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    private SfGrid<AppointmentTypeDto> SfGrid { get; set; } = null!;
    private List<AppointmentTypeDto> TypeList { get; set; } = [];
    private bool AllTypesSelected { get; set; }
    private AppointmentTypeCreateDialog CreateAppointmentTypeDialog { get; set; } = null!;
    private AppointmentTypeUpdateDialog UpdateAppointmentTypeDialog { get; set; } = null!;
    private Guid EditingTypeId { get; set; }
    private bool CanCreateType { get; set; }
    private bool CanEditType { get; set; }
    private bool CanDeleteType { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetTypesAsync();
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
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["AppointmentTypes"]));
        return ValueTask.CompletedTask;
    }
    private async Task GetTypesAsync()
    {
        TypeList = await AppointmentTypeAppService.GetListAsync();
        await ClearSelection();
    }
    private Task ClearSelection()
    {
        AllTypesSelected = false;
        SfGrid.SelectedRecords.Clear();
        return Task.CompletedTask;
    }
    private Task SelectedTypeRowChangedAsync()
    {
        AllTypesSelected = SfGrid.SelectedRecords.Count == TypeList.Count;
        return Task.CompletedTask;
    }

    private async Task OpenAppointmentTypeCreateDialogAsync() => await CreateAppointmentTypeDialog.ShowAsync();

    private async Task CreateTypeAsync(AppointmentTypeCreateDto type)
    {
        try
        {
            await AppointmentTypeAppService.CreateAsync(type);
            await GetTypesAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task OpenEditTypeModalAsync(AppointmentTypeDto input)
    {
        EditingTypeId = input.Id;
        await UpdateAppointmentTypeDialog.ShowAsync(EditingTypeId);
    }

    private async Task UpdateTypeAsync(AppointmentTypeUpdateDto dto)
    {
        try
        {
            await AppointmentTypeAppService.UpdateAsync(EditingTypeId, dto);
            await GetTypesAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task DeleteTypeAsync(AppointmentTypeDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await AppointmentTypeAppService.DeleteAsync(input.Id);
            await GetTypesAsync();
        }
    }
    private async Task DeleteSelectedTypesAsync()
    {
        var message = AllTypesSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        await AppointmentTypeAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetTypesAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateType = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.AppointmentTypes.Create);
        CanEditType = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.AppointmentTypes.Edit);
        CanDeleteType = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.AppointmentTypes.Delete);
    }
}
