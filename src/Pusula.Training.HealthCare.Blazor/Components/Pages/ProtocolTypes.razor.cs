using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.ProtocolTypeActions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class ProtocolTypes
{
    protected override async Task OnInitializedAsync() => await GetProtocolTypes();

#region ProtocolType

    private IReadOnlyList<ProtocolTypeDto> ProtocolTypeList { get; set; } = [];

    private async Task GetProtocolTypes()
    {
        var response = await ProtocolTypeAppService.GetListAsync(new GetProtocolTypeInput());
        ProtocolTypeList = response.Items;
    }

    private async Task DeleteProtocolTypeAsync(ProtocolTypeDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync(
            "Are you sure you want to permanently delete this item?", "Delete Confirmation"
        );
        if (isConfirm)
        {
            await ProtocolTypeAppService.DeleteAsync(input.Id);
            await GetProtocolTypes();
        }
    }

#region CreateProtocolType

    private SfDialog ProtocolTypeCreateDialog { get; set; } = null!;
    private ProtocolTypeCreateDto ProtocolTypeCreateDto { get; set; } = new();
    private async Task ShowProtocolTypeCreateDialogAsync() => await ProtocolTypeCreateDialog.ShowAsync();

    private async Task HideProtocolTypeCreateDialogAsync()
    {
        SetDefaultsForCreateProtocolTypeDialog();
        await ProtocolTypeCreateDialog.HideAsync();
    }

    private void SetDefaultsForCreateProtocolTypeDialog() => ProtocolTypeCreateDto = new ProtocolTypeCreateDto();

    private async Task ProtocolTypeCreateFormOnValidSubmitAsync()
    {
        await ProtocolTypeAppService.CreateAsync(ProtocolTypeCreateDto);
        await GetProtocolTypes();
        await HideProtocolTypeCreateDialogAsync();
    }

#endregion

#region UpdateProtocolType

    private SfDialog ProtocolTypeUpdateDialog { get; set; } = null!;
    private Guid UpdatingProtocolTypeId { get; set; }
    private ProtocolTypeUpdateDto ProtocolTypeUpdateDto { get; set; } = new();

    private async Task ShowProtocolTypeUpdateDialogAsync(Guid protocolTypeId)
    {
        UpdatingProtocolTypeId = protocolTypeId;
        var item = await ProtocolTypeAppService.GetAsync(protocolTypeId);
        ProtocolTypeUpdateDto.Name = item.Name;
        await ProtocolTypeUpdateDialog.ShowAsync();
    }

    private async Task HideProtocolTypeUpdateDialogAsync()
    {
        SetDefaultsForUpdateProtocolTypeDialog();
        await ProtocolTypeUpdateDialog.HideAsync();
    }

    private void SetDefaultsForUpdateProtocolTypeDialog() => ProtocolTypeUpdateDto = new ProtocolTypeUpdateDto();

    private async Task ProtocolTypeUpdateFormOnValidSubmitAsync()
    {
        await ProtocolTypeAppService.UpdateAsync(UpdatingProtocolTypeId, ProtocolTypeUpdateDto);
        await GetProtocolTypes();
        await HideProtocolTypeUpdateDialogAsync();
    }

#endregion

#endregion

#region ProcotolTypeAction

    private SfGrid<ProtocolTypeActionDto> ProtocolTypeActionListGrid { get; set; } = null!;
    private IEnumerable<ProtocolTypeActionDto> ProtocolTypeActionList { get; set; } = [];
    private Guid? SelectedProtocolTypeId { get; set; }

    private async Task GetProtocolTypeActions(Guid protocolTypeId)
    {
        ProtocolTypeActionList = await ProtocolTypeActionAppService.GetAllAsync(protocolTypeId);
        await ProtocolTypeActionListGrid.Refresh();
    }

    private async Task SelectedProtocolTypeChangedAsync(ListBoxChangeEventArgs<Guid[], ProtocolTypeDto> args)
    {
        SelectedProtocolTypeId = args.Value.FirstOrDefault();
        if (SelectedProtocolTypeId.HasValue)
        {
            await GetProtocolTypeActions(SelectedProtocolTypeId.Value);
        }
    }

    private async Task DeleteProtocolTypeActionAsync(Guid protocolTypeActionId) => await Task.CompletedTask;

#region CreateProcotolTypeAction

    private SfDialog ProtocolTypeActionCreateDialog { get; set; } = null!;
    private ProtocolTypeActionCreateDto ProtocolTypeActionCreateDto { get; set; } = new();

    private async Task ShowProtocolTypeActionCreateDialogAsync(Guid protocolTypeId)
    {
        ProtocolTypeActionCreateDto.ProtocolTypeId = protocolTypeId;
        await ProtocolTypeActionCreateDialog.ShowAsync();
    }

    private async Task HideProtocolTypeActionCreateDialogAsync()
    {
        SetDefaultsForProtocolTypeActionCreateDialog();
        await ProtocolTypeActionCreateDialog.HideAsync();
    }

    private void SetDefaultsForProtocolTypeActionCreateDialog() =>
        ProtocolTypeActionCreateDto = new ProtocolTypeActionCreateDto();

    private async Task ProtocolTypeActionCreateFormOnValidSubmitAsync()
    {
        var result = await ProtocolTypeActionAppService.CreateAsync(ProtocolTypeActionCreateDto);
        await GetProtocolTypeActions(result.ProtocolTypeId);
        await HideProtocolTypeActionCreateDialogAsync();
    }

#endregion

#region UpdateProcotolTypeAction

    private SfDialog ProtocolTypeActionUpdateDialog { get; set; } = null!;
    private Guid UpdatingProtocolTypeActionId { get; set; }
    private ProtocolTypeActionUpdateDto ProtocolTypeActionUpdateDto { get; set; } = new();

    private async Task ShowProtocolTypeActionUpdateDialogAsync(Guid protocolActionTypeId)
    {
        UpdatingProtocolTypeActionId = protocolActionTypeId;
        var item = await ProtocolTypeActionAppService.GetAsync(protocolActionTypeId);
        ProtocolTypeActionUpdateDto.Name = item.Name;
        await ProtocolTypeActionUpdateDialog.ShowAsync();
    }

    private async Task HideProtocolTypeActionUpdateDialogAsync()
    {
        SetDefaultsForProtocolTypeActionUpdateDialog();
        await ProtocolTypeActionUpdateDialog.HideAsync();
    }

    private void SetDefaultsForProtocolTypeActionUpdateDialog() =>
        ProtocolTypeActionUpdateDto = new ProtocolTypeActionUpdateDto();

    private async Task ProtocolTypeActionUpdateFormOnValidSubmitAsync()
    {
        var result = await ProtocolTypeActionAppService.UpdateAsync(
            UpdatingProtocolTypeActionId, ProtocolTypeActionUpdateDto
        );
        await GetProtocolTypeActions(result.ProtocolTypeId);
        await HideProtocolTypeActionUpdateDialogAsync();
    }

#endregion

#endregion
}