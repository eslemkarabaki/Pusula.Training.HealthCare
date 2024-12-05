using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.ProtocolTypes;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class ProtocolTypes
    {
        [Inject]
        public IProtocolTypeAppService ProtocolTypeAppService { get; set; }
        private List<ProtocolTypeDto> ProtocolTypeList { get; set; } = new List<ProtocolTypeDto>();
        private bool IsDialogVisible { get; set; } = false;
        private ProtocolTypeDto ProtocolTypeModel { get; set; } = new ProtocolTypeDto();
        private Guid EditingProtocolTypeId { get; set; }
        private ProtocolTypeUpdateDto EditingProtocolType { get; set; } = new();
        private bool OpenUpdateDialog { get; set; }
        private string ErrorMessage { get; set; } = string.Empty;

        private int maxResultCount = 10;
        private int skipCount = 0;
        private string sorting = "Name";

        protected override async Task OnInitializedAsync()
        {
            await RefreshProtocolTypes();
        }
        private async Task RefreshProtocolTypes()
        {
            var response = await ProtocolTypeAppService.GetListAsync(new GetProtocolTypeInput());
            ProtocolTypeList = response.Items.ToList();
        }
        private void OpenCreateDialog()
        {
            ProtocolTypeModel = new ProtocolTypeDto();
            ErrorMessage = string.Empty;
            IsDialogVisible = true;
        }
        private void CloseDialog()
        {
            ErrorMessage = string.Empty;
            IsDialogVisible = false;
            StateHasChanged();
        }
        private async Task HandleCreate()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(ProtocolTypeModel.Name))
            {
                ErrorMessage = "Protocol Type name cannot be empty.";
                StateHasChanged();
                return;
            }
            await ProtocolTypeAppService.CreateAsync(new ProtocolTypeCreateDto { Name = ProtocolTypeModel.Name });
            IsDialogVisible = false;
            await RefreshProtocolTypes();
        }
        private async Task OpenEditProtocolTypeModalAsync(ProtocolTypeDto input)
        {
            EditingProtocolTypeId = input.Id;
            var protocolType = await ProtocolTypeAppService.GetAsync(input.Id);
            EditingProtocolType = ObjectMapper.Map<ProtocolTypeDto, ProtocolTypeUpdateDto>(protocolType);
            OpenUpdateDialog = true;
        }
        private async Task HandleUpdate()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(EditingProtocolType.Name))
            {
                ErrorMessage = "Protocol Type name cannot be empty.";
                StateHasChanged();
                return;
            }
            await ProtocolTypeAppService.UpdateAsync(EditingProtocolTypeId, EditingProtocolType);
            OpenUpdateDialog = false;
            await RefreshProtocolTypes();
        }
        private void CloseUpdateDialog()
        {
            ErrorMessage = string.Empty;
            OpenUpdateDialog = false;
            StateHasChanged();
        }
        private async Task DeleteProtocolTypeAsync(ProtocolTypeDto input)
        {
            bool isConfirm = await DialogService.ConfirmAsync("Are you sure you want to permanently delete this item?", "Delete Confirmation");
            if (isConfirm)
            {
                await ProtocolTypeAppService.DeleteAsync(input.Id);
                await RefreshProtocolTypes();
            }
        }
    }
}
