using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Diagnoses;
using Syncfusion.Blazor.Popups;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class Diagnoses
    {
        [Inject]
        public IDiagnosisAppService DiagnosesAppService { get; set; }
        private SfDialog CreateDialog { get; set; } = null!;
        private List<DiagnosisDto> DiagnosisList { get; set; } = new List<DiagnosisDto>();
        private DiagnosisDto DiagnosisModel { get; set; } = new DiagnosisDto();
        private SfDialog UpdateDialog { get; set; } = null;
        private DiagnosisDto EditingDiagnosis { get; set; } = new DiagnosisDto();
        private Guid EditingDiagnosisId { get; set; }
        private string ErrorMessage { get; set; } = string.Empty;
        private int maxResultCount = 10;
        private int skipCount = 0;
        private string sorting = "Name";

        // Override OnInitializedAsync from ComponentBase
        protected override async Task OnInitializedAsync()
        {
            await RefreshDiagnoses();
            StateHasChanged();
        }

        private async Task RefreshDiagnoses()
        {
            var response = await DiagnosisAppService.GetListAsync(new GetDiagnosisInput());
            DiagnosisList = response.Items.ToList();
        }

        private async Task OpenCreateDialog()
        {
            DiagnosisModel = new DiagnosisDto();
            ErrorMessage = string.Empty;
            await CreateDialog.ShowAsync();
        }

        private async Task CloseDialog()
        {
            ErrorMessage = string.Empty;
            await CreateDialog.HideAsync();
            StateHasChanged();
        }

        private async Task HandleCreate()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(DiagnosisModel.Code) || string.IsNullOrWhiteSpace(DiagnosisModel.Name))
            {
                ErrorMessage = "Code and Name cannot be empty.";
                StateHasChanged();
                return;
            }

            await DiagnosisAppService.CreateAsync(new DiagnosisCreateDto
            {
                Code = DiagnosisModel.Code,
                Name = DiagnosisModel.Name
            });
            await CloseDialog();
            await RefreshDiagnoses();
        }

        private async Task OpenEditDiagnosisModalAsync(DiagnosisDto diagnosis)
        {
            EditingDiagnosisId = diagnosis.Id;
            EditingDiagnosis = await DiagnosisAppService.GetAsync(diagnosis.Id);
            await UpdateDialog.ShowAsync();
        }

        private async Task HandleUpdate()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(EditingDiagnosis.Code) || string.IsNullOrWhiteSpace(EditingDiagnosis.Name))
            {
                ErrorMessage = "Code and Name cannot be empty.";
                StateHasChanged();
                return;
            }

            await DiagnosisAppService.UpdateAsync(EditingDiagnosisId, new DiagnosisUpdateDto
            {
                Code = EditingDiagnosis.Code,
                Name = EditingDiagnosis.Name
            });

            await UpdateDialog.HideAsync();
            await RefreshDiagnoses();
        }

        private async Task CloseUpdateDialog()
        {
            ErrorMessage = string.Empty;
            await UpdateDialog.HideAsync();
            StateHasChanged();
        }

        private async Task DeleteDiagnosisAsync(DiagnosisDto diagnosis)
        {
            bool isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete this Diagnosis?", "Delete Confirmation");
            if (isConfirm)
            {
                await DiagnosisAppService.DeleteAsync(diagnosis.Id);
                await RefreshDiagnoses();
            }
        }
    }
}
