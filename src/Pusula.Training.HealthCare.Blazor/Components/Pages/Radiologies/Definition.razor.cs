using Microsoft.AspNetCore.Authorization; 
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Radiologies; 
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Radiologies;
public partial class Definition
{
    private SfGrid<RadiologyExaminationGroupDto> SfGrid { get; set; } = null!;
    private SfGrid<RadiologyExaminationDto> SfGridEx { get; set; } = null!;

    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new();

    private IReadOnlyList<RadiologyExaminationGroupDto> GroupList { get; set; } = new List<RadiologyExaminationGroupDto>();
    private List<RadiologyExaminationDto> ExList { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetGroupsAsync(); 
    }

    private async Task GetGroupsAsync()
    {
        var groupResult = await RadiologyExaminationGroupAppService.GetListAsync(new GetRadiologyExaminationGroupsInput());
        GroupList = groupResult.Items;

        foreach (var group in GroupList)
        {
            var examinationInput = new GetRadiologyExaminationsInput { GroupId = group.Id };
            var examinationResult = await RadiologyExaminationAppService.GetListByGruopIdAsync(examinationInput, group.Id);
            DetailExaminations[group.Id] = examinationResult.Items.ToList();
        }
    }


    #region Permission

    private bool CanCreateGroup { get; set; }
    private bool CanEditGroup { get; set; }
    private bool CanDeleteGroup { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateGroup = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.RadiologyExaminationGroups.Create);
        CanEditGroup = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.RadiologyExaminationGroups.Edit);
        CanDeleteGroup = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.RadiologyExaminationGroups.Delete);
    }

    #endregion

    #region Create

    private RadiologyGroupCreateDialog CreateGroupDialog { get; set; } = null!;
    private async Task OpenCreateGroupDialogAsync() => await CreateGroupDialog.ShowAsync();

    private async Task CreateGroupAsync(RadiologyExaminationGroupCreateDto group)
    {
        try
        {
            await RadiologyExaminationGroupAppService.CreateAsync(group);
            await GetGroupsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    #endregion

    #region Update

    private RadiologyGroupUpdateDialog UpdateGroupDialog { get; set; } = null!;
    private Guid EditingGroupId { get; set; }

    private async Task OpenEditGroupModalAsync(RadiologyExaminationGroupDto input)
    {
        EditingGroupId = input.Id;
        await UpdateGroupDialog.ShowAsync(EditingGroupId);
    }

    private async Task UpdateGroupAsync(RadiologyExaminationGroupUpdateDto dto)
    {
        try
        {
            await RadiologyExaminationGroupAppService.UpdateAsync(EditingGroupId, dto);
            await GetGroupsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    #endregion

    #region Delete

    private async Task DeleteGroupAsync(RadiologyExaminationGroupDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await RadiologyExaminationGroupAppService.DeleteAsync(input.Id);
            await GetGroupsAsync();
        }
    } 
    #endregion

    #region RadiologyExamination
    private Dictionary<Guid, List<RadiologyExaminationDto>> DetailExaminations { get; set; } = new();
    private RadiologyExaminationUpdateDialog UpdateDialog { get; set; } = null!;
    private RadiologyExaminationCreateDialog CreateExaminationDialog { get; set; } = null!;
    private Guid EditingExaminationId { get; set; }
    private Guid EditingExaminationGroupId { get; set; }

    private async Task OpenEditDialogAsync(RadiologyExaminationDto examination)
    {
        EditingExaminationId = examination.Id;

        await UpdateDialog.ShowAsync(EditingExaminationId);
    }

    private async Task OpenCreateExaminationDialogAsync(RadiologyExaminationGroupDto context)
    { 
        EditingExaminationGroupId = context.Id;
        await CreateExaminationDialog.ShowAsync(EditingExaminationGroupId);   
    }

    private async Task CreateExaminationAsync(RadiologyExaminationCreateDto group)
    {
        try
        {
            group.GroupId = EditingExaminationGroupId;
            await RadiologyExaminationAppService.CreateAsync(group);
            await GetGroupsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task UpdateExaminationAsync(RadiologyExaminationUpdateDto updatedExamination)
    {

        await RadiologyExaminationAppService.UpdateAsync(EditingExaminationId, updatedExamination);
         
        await GetGroupsAsync();
    }

    private async Task DeleteExaminationAsync(RadiologyExaminationDto examination)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await RadiologyExaminationAppService.DeleteAsync(examination.Id);

            await GetGroupsAsync();
        } 
    }



    #endregion
}
