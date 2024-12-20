namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Radiologies;

using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Radiologies;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class Request
{
    private SfGrid<RadiologyRequestWithNavigationPropertiesDto> SfGrid { get; set; } = null!;
    private Dictionary<Guid, List<RadiologyRequestItemWithNavigationPropertiesDto>> DetailItems { get; set; } = new();
    private IReadOnlyList<RadiologyRequestWithNavigationPropertiesDto> RequestList { get; set; } = new List<RadiologyRequestWithNavigationPropertiesDto>();

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync(); 
        await LoadRequestsAsync();
    }

    private async Task LoadRequestsAsync()
    {
        await GetRequestsAsync();
        StateHasChanged();  
    }

    private async Task GetRequestsAsync()
    {
        var requestResult = await RadiologyRequestAppService.GetListNavigationPropertiesAsync(new GetRadiologyRequestsInput());
        RequestList = requestResult.Items;
        foreach (var request in RequestList)
        {
            var requestItemsResult = await RadiologyRequestItemAppService.GetListWithNavigationPropertiesByRequestItemAsync(new GetRadiologyRequestItemsInput(), request.RadiologyRequest.Id);
            DetailItems[request.RadiologyRequest.Id] = requestItemsResult.Items.ToList();
        }

        StateHasChanged();
    }


    #region Permission
    private bool CanCreateItem { get; set; }
    private bool CanEditItem { get; set; }
    private bool CanDeleteItem { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateItem = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.RadiologyRequestItems.Create);
        CanEditItem = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.RadiologyRequestItems.Edit);
        CanDeleteItem = await AuthorizationService.IsGrantedAsync(HealthCarePermissions.RadiologyRequestItems.Delete);
    }
    #endregion 

    #region UpdateRequestItem

    private RadiologyRequestItemWithNavigationPropertiesDto? EditingDto { get; set; }
    private RadiologyRequestResultDialog ResultDialog { get; set; } = null!;

    private async Task OpenResultDialog(RadiologyRequestItemWithNavigationPropertiesDto dto)
    {
        EditingDto = dto;
        await ResultDialog.ShowAsync(dto.RadiologyRequestItem.Result ?? string.Empty);
    }

    private async Task OnResultDialogSave(string newResult)
    {
        if (EditingDto == null) return;

        EditingDto.RadiologyRequestItem.Result = newResult;
        await OnResultChange(EditingDto.RadiologyRequestItem, newResult);  
    }

    private async Task OnResultChange(RadiologyRequestItemDto dto, string newValue)
    {
        if (string.IsNullOrWhiteSpace(newValue) || newValue.Length < 3)
        {
            return;
        }

        dto.Result = newValue;
        dto.ResultDate = DateTime.Now;
        dto.State = RadiologyRequestItemState.Completed;

        var updateDto = new RadiologyRequestItemUpdateDto
        {
            RequestId = dto.RequestId ?? throw new InvalidOperationException("State cannot be null"),
            ExaminationId = dto.ExaminationId ?? throw new InvalidOperationException("ExaminationId cannot be null"),
            Result = dto.Result,
            ResultDate = dto.ResultDate ?? throw new InvalidOperationException("ResultDate cannot be null"),
            State = dto.State ?? throw new InvalidOperationException("State cannot be null"),
            ConcurrencyStamp = dto.ConcurrencyStamp
        };

        try
        {
            await RadiologyRequestItemAppService.UpdateAsync(dto.Id, updateDto);
        }
        catch (Exception ex)
        {
        }
    }

    private static string StripHtml(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
    }

    #endregion

    #region State Change      

    private Dictionary<Guid, bool> dropDownVisibilityState = new();
     
    private void ToggleDropDown(Guid itemId)
    { 
        if (dropDownVisibilityState.ContainsKey(itemId))
        {
            dropDownVisibilityState[itemId] = !dropDownVisibilityState[itemId];
        }
        else
        {
            dropDownVisibilityState[itemId] = true;
        }
         
        foreach (var key in dropDownVisibilityState.Keys.ToList())
        {
            if (key != itemId)
            {
                dropDownVisibilityState[key] = false;
            }
        }
    }
     
    private bool IsDropDownVisibleForItem(Guid itemId)
    {
        return dropDownVisibilityState.ContainsKey(itemId) && dropDownVisibilityState[itemId];
    }

    private async Task StatusChanged(RadiologyRequestItemWithNavigationPropertiesDto dto, RadiologyRequestItemState? newState)
    {
        if (newState == null)
        {
            newState = RadiologyRequestItemState.Pending;
        }

        Console.WriteLine($"State is changing to {newState}");

        if (dto.RadiologyRequestItem.State == newState)
        {
            return;
        }

        dto.RadiologyRequestItem.State = newState;

        var updateDto = new RadiologyRequestItemUpdateDto
        {
            RequestId = dto.RadiologyRequestItem.RequestId ?? throw new InvalidOperationException("RequestId cannot be null"),
            ExaminationId = dto.RadiologyRequestItem.ExaminationId ?? throw new InvalidOperationException("ExaminationId cannot be null"),
            Result = dto.RadiologyRequestItem.Result,
            ResultDate = DateTime.Now,
            State = newState.Value,
            ConcurrencyStamp = dto.RadiologyRequestItem.ConcurrencyStamp
        };

        try
        {
            await RadiologyRequestItemAppService.UpdateAsync(dto.RadiologyRequestItem.Id, updateDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating state: {ex.Message}");
        }
         
        dropDownVisibilityState[dto.RadiologyRequestItem.Id] = false;
    }
    #endregion

}
