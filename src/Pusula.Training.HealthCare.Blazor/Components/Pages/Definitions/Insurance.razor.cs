using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Insurances;
using Volo.Abp.Validation;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Definitions;

public partial class Insurance
{
    [Inject]
    public IInsurancesAppService InsurancesAppService { get; set; }

    private List<InsuranceDto> InsuranceList { get; set; } = new();
    private InsuranceDto InsuranceModel { get; set; } = new();
    private bool IsDialogVisible { get; set; } = false;
    private bool OpenUpdateDialog { get; set; } = false;
    private Guid EditingInsuranceId { get; set; }
    private InsuranceUpdateDto EditingInsurance { get; set; } = new();
    private string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync() => await RefreshInsurances();

    private async Task RefreshInsurances()
    {
        var response = await InsurancesAppService.GetListAsync(new GetInsurancesInput());
        InsuranceList = response.Items.ToList();
    }

    private void OpenCreateDialog()
    {
        InsuranceModel = new InsuranceDto();
        IsDialogVisible = true;
        OpenUpdateDialog = false;
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
        if (string.IsNullOrWhiteSpace(InsuranceModel.Name))
        {
            ErrorMessage = "Insurance name cannot be empty.";
            Console.WriteLine(ErrorMessage);
            StateHasChanged();
            return;
        }

        await InsurancesAppService.CreateAsync(new InsuranceCreateDto { Name = InsuranceModel.Name });
        IsDialogVisible = false;
        await RefreshInsurances();
    }

    private async Task OpenEditInsurancesModalAsync(InsuranceDto input)
    {
        EditingInsurance =
            ObjectMapper.Map<InsuranceDto, InsuranceUpdateDto>(await InsurancesAppService.GetAsync(input.Id));
        EditingInsuranceId = input.Id;
        OpenUpdateDialog = true;
        IsDialogVisible = false;
    }

    private async Task HandleUpdate()
    {
        ErrorMessage = string.Empty;
        if (string.IsNullOrWhiteSpace(EditingInsurance.Name))
        {
            ErrorMessage = "Insurance name cannot be empty.";
            Console.WriteLine(ErrorMessage);
            StateHasChanged();
            return;
        }

        await InsurancesAppService.UpdateAsync(EditingInsuranceId, EditingInsurance);
        OpenUpdateDialog = false;
        EditingInsurance = new InsuranceUpdateDto();
        await RefreshInsurances();
    }

    private void CloseUpdateDialog()
    {
        ErrorMessage = string.Empty;
        OpenUpdateDialog = false;
        StateHasChanged();
    }

    private async Task DeleteInsuranceAsync(InsuranceDto input)
    {
        try
        {
            var isConfirm = await DialogService.ConfirmAsync(
                "Are you sure you want to permanently delete this item?", "Delete Confirmation"
            );
            if (isConfirm)
            {
                await InsurancesAppService.DeleteAsync(input.Id);
                await RefreshInsurances();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting insurance: {ex.Message}");
        }
    }
}