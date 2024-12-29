using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Jobs;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Jobs
{
    private readonly List<Volo.Abp.BlazoriseUI.BreadcrumbItem> _breadcrumbItems = [];
    private SfGrid<JobDto> SfGrid { get; set; } = null!;
    private List<JobDto> JobList { get; set; } = [];
    private bool AllRowSelected { get; set; }

    private Guid EditingJobId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetJobsAsync();
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
        _breadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Jobs"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetJobsAsync() => JobList = await JobAppService.GetListAsync(new GetJobsInput());

#region Create

    private SfDialog JobCreateDialog { get; set; } = null!;
    private JobCreateDto JobCreateDto { get; set; } = new();

    private async Task OpenJobCreateDialogAsync() => await JobCreateDialog.ShowAsync();

    private async Task CreateAsync()
    {
        try
        {
            await JobAppService.CreateAsync(JobCreateDto);
            await GetJobsAsync();
            await HideJobCreateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideJobCreateDialogAsync()
    {
        await JobCreateDialog.HideAsync();
        SetDefaultsForJobCreateDialog();
    }

    private void SetDefaultsForJobCreateDialog() => JobCreateDto = new JobCreateDto();

#endregion

#region Update

    private SfDialog JobUpdateDialog { get; set; } = null!;
    private JobUpdateDto JobUpdateDto { get; set; } = new();

    private async Task OpenJobUpdateDialogAsync(JobDto input)
    {
        EditingJobId = input.Id;
        var dto = await JobAppService.GetAsync(EditingJobId);
        JobUpdateDto.Name = dto.Name;
        await JobUpdateDialog.ShowAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            await JobAppService.UpdateAsync(EditingJobId, JobUpdateDto);
            await GetJobsAsync();
            await HideJobUpdateDialogAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task HideJobUpdateDialogAsync()
    {
        await JobUpdateDialog.HideAsync();
        SetDefaultsForJobCreateDialog();
    }

    private void SetDefaultsForJobUpdateDialog() => JobUpdateDto = new JobUpdateDto();

#endregion

    private async Task DeleteAsync(JobDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await JobAppService.DeleteAsync(input.Id);
            await GetJobsAsync();
        }
    }

    private Task SelectedRowChangedAsync()
    {
        AllRowSelected = SfGrid.SelectedRecords.Count == JobList.Count;
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

        await JobAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Id).ToList());
        await GetJobsAsync();
    }

    private bool CanCreate { get; set; }
    private bool CanEdit { get; set; }
    private bool CanDelete { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreate = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Jobs.Create);
        CanEdit = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Jobs.Edit);
        CanDelete = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Jobs.Delete);
    }
}