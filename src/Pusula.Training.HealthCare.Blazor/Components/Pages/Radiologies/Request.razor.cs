namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Radiologies;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Radiologies;
using Pusula.Training.HealthCare.Blazor.Models;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Volo.Abp;

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
          
            foreach (var item in DetailItems[request.RadiologyRequest.Id])
            {
                await GetDocumentByItemId(item.RadiologyRequestItem.Id);
            }
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

    private async Task OnResultDialogSave(RequestItemWithDocumentModel newResult)
    {
        if (EditingDto == null) return;
        EditingDto.RadiologyRequestItem.Result = newResult.Result;
        await OnResultChange(EditingDto.RadiologyRequestItem, EditingDto.RadiologyRequestItem.Result, newResult.File);
    }

    private async Task OnResultChange(RadiologyRequestItemDto dto, string newValue, IFormFile? selectedFile)
    {
        if (string.IsNullOrWhiteSpace(newValue) || newValue.Length < 3)
        {
            return;
        }

        try
        {

            dto.Result = newValue;
            dto.ResultDate = DateTime.Now;
            dto.State = RadiologyRequestItemState.Completed;

            var updateDto = new RadiologyRequestItemUpdateDto
            {
                RequestId = dto.RequestId ?? throw new InvalidOperationException("State cannot be null"),
                ExaminationId = dto.ExaminationId ?? throw new InvalidOperationException("ExaminationId cannot be null"),
                Result = dto.Result,
                ResultDate = dto.ResultDate ?? throw new InvalidOperationException("ResultDate cannot be null"),
                State = dto.State ?? throw new InvalidOperationException("State cannot be null")
            };
             
            if (selectedFile != null)
            {
                var input = new RadiologyExaminationDocumentCreateDto
                {
                    File = selectedFile,
                    ItemId = dto.Id
                };
                await RadiologyExaminationDocumentAppService.CreateAsync(input);
                await GetDocumentByItemId(dto.Id);
            }

            await RadiologyRequestItemAppService.UpdateAsync(dto.Id, updateDto);
            await SendNotification();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw new UserFriendlyException("An error occurred while saving the result or file. The operation was rolled back.");
        }
    }
    #endregion


    #region ScriptHtml
    private static string StripHtml(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
    }
    #endregion

    #region Hub
    private async Task SendNotification()
    {
        try
        {
            await JS.InvokeVoidAsync("SendNotificationToDoctor", EditingDto.Patient.FullName + "adlý hastanýn sonucu çýktý.", EditingDto.Doctor.UserId);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
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

    #region GetDocumentByItemId
     
    private RadiologyDocumentDialog DocumentDialog { get; set; } = null!;
    private Dictionary<Guid, List<RadiologyExaminationDocumentDto>> ItemDocuments { get; set; } = new();

    private async Task ShowDocumentsAsync(Guid itemId)
    {
        if (ItemDocuments.TryGetValue(itemId, out var documents))
        {
            await DocumentDialog.ShowAsync(documents);
        }
    }


    private async Task GetDocumentByItemId(Guid itemId)
    {
        var result = await RadiologyExaminationDocumentAppService.GetListAsync(new GetRadiologyExaminationDocumentsInput { ItemId = itemId });
        ItemDocuments[itemId] = result.Items.ToList();
    }
    #endregion
}
