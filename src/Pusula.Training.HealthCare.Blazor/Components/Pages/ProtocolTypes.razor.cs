using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.ProtocolTypes;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Popups;


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
        private bool OpenUptadeDialog {  get; set; }
        private bool OpenDeleteDialog { get; set; }




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
            ProtocolTypeModel = new ProtocolTypeDto(); // Yeni giriş için modeli temizle
            IsDialogVisible = true;
        }
        private void OnVisibleChange(bool visible)
        {
            IsDialogVisible = visible;
        }
        private void CloseDialog()
        {
            IsDialogVisible = false;
        }
        private void OnNameChange(string value)
        {
            ProtocolTypeModel.Name = value;
        }

        private async Task HandleCreate()
        {
            if (!string.IsNullOrWhiteSpace(ProtocolTypeModel.Name))
            {
                await ProtocolTypeAppService.CreateAsync(new ProtocolTypeCreateDto { Name = ProtocolTypeModel.Name });
                IsDialogVisible = false;
                await RefreshProtocolTypes();
            }
        }
        private ProtocolTypeUpdateDto EditingProtocolType { get; set; } = new();
        private async Task OpenEditProtocolTypeModalAsync(ProtocolTypeDto input)
        {

            EditingProtocolType = ObjectMapper.Map<ProtocolTypeDto, ProtocolTypeUpdateDto>(await ProtocolTypeAppService.GetAsync(input.Id));
            EditingProtocolTypeId = input.Id;
            OpenUptadeDialog = true;
        }
        private async Task HandleUptade()
        {
                await ProtocolTypeAppService.UpdateAsync(EditingProtocolTypeId, EditingProtocolType);
                OpenUptadeDialog = false;
                EditingProtocolType = new();
                await RefreshProtocolTypes();
            
        }
        private void CloseUptadeDialog()
        {
            OpenUptadeDialog = false;
        }
        private async Task DeleteProtocolTypeAsync(ProtocolTypeDto input)
        {

            bool isConfirm = await DialogService.ConfirmAsync("Are you sure you want to permanently delete these items?", "Delete Multiple Items");

            if (isConfirm)
            { 

                await ProtocolTypeAppService.DeleteAsync(input.Id);
            IsDialogVisible = false;
            await RefreshProtocolTypes();

            }
            
        }
    }
}
