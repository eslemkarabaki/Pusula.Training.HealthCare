using Pusula.Training.HealthCare.Examinations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Diagnoses;
using Volo.Abp.Application.Dtos;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Volo.Abp.ObjectMapping;
using Pusula.Training.HealthCare.Protocols;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Medical;

public partial class Examinations
{
    [Parameter]
    public int ProtocolNo { get; set; }
    [CascadingParameter]
    private ProtocolDto Protocol { get; set; }
    public ExaminationUpdateDto UpdateExaminationDto { get; set; } = new();
    private bool showSuccessMessage { get; set; } = false;
    private bool showErrorMessage { get; set; } = false;
    private string Pain { get; set; }
    private PagedResultDto<ExaminationPhysicalDto> ExaminationPhysical { get; set; } = new();

    private bool Visibility = false;
    private bool ConfirmationDialogVisibility = false;

    public ExaminationWithNavigationPropertiesDto? ExaminationDto { get; set; }
    protected override async Task OnInitializedAsync()
    {
        ExaminationDto =  await ExaminationAppService.GetWithProtocolNoAsync(new GetExaminationsInput { ProtocolNo = ProtocolNo});

        UpdateExaminationDto = ObjectMapper.Map<ExaminationDto, ExaminationUpdateDto>(ExaminationDto.Examination);
        DiagnosisType = await DiagnosisAppService.GetListAsync(new GetDiagnosisInput());
    }
    private async Task<ExaminationWithNavigationPropertiesDto> GetExamination()
    {
        return  (await ExaminationAppService
            .GetListWithNavigationPropertiesAsync(new GetExaminationsInput {ProtocolNo  = ProtocolNo }))
            .Items.ToList()
            .FirstOrDefault();

    }
    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        return base.OnAfterRenderAsync(firstRender);
    }
    private async Task OnSaveAllChanges()
    {

        var response = await ExaminationAppService.GetWithProtocolNoAsync(new GetExaminationsInput
        {
            ProtocolNo = ProtocolNo
        });

        if (response == null)
        {
            UpdateExaminationDto = new ExaminationUpdateDto
            {
                PhysicalUpdateDto = ObjectMapper.Map<ExaminationPhysicalDto, ExaminationPhysicalUpdateDto>(response.ExaminationPhysical),
                AnamnezUpdateDto = ObjectMapper.Map<ExaminationAnamnezDto, ExaminationAnamnezUpdateDto>(response.ExaminationAnamnez),
                DiagnosisUpdateDto = ObjectMapper.Map<ExaminationDiagnosisDto, ExaminationDiagnosisUpdateDto>(response.ExaminationDiagnoses)
            };
        }
        else
        {
         await ExaminationAppService.UpdateAsync(ExaminationDto.Examination.Id,UpdateExaminationDto);
        }
        await Task.Delay(1000);
        bool isSaveSuccessful = await SaveChangesAsync();
        ConfirmationDialogVisibility = true;
        StateHasChanged();
    }
    private PagedResultDto<DiagnosisDto> DiagnosisType { get; set; } = new();
    private async void OnConfirmYes()
    {
        bool isSaveSuccessful = await SaveChangesAsync();

        if (isSaveSuccessful)
        {
            showSuccessMessage = true;
            showErrorMessage = false;
            Visibility = true;
            InvokeAsync(StateHasChanged);
        }
        else
        {
            showErrorMessage = true;
            showSuccessMessage = false;
            Visibility = true;
            InvokeAsync(StateHasChanged);
        }

        ConfirmationDialogVisibility = false;
    }
    private async Task<bool> SaveChangesAsync()
    {
        await Task.Delay(1000);
        bool success = true;
        return success;
    }

    private void OnConfirmNo()
    {
        showErrorMessage = true;
        showSuccessMessage = false;
        Visibility = true;

        ConfirmationDialogVisibility = false;
        InvokeAsync(StateHasChanged);
    }

    private void DialogButtonClick()
    {
        Visibility = false;
    }

    private void DialogOpen()
    {
        Console.WriteLine("Dialog Opened");
    }

    private void DialogClose()
    {
        Console.WriteLine("Dialog Closed");
    }

    private void OverlayClick()
    {
        Visibility = false;
        StateHasChanged();
        Console.WriteLine("Dialog closed due to overlay click");
    }

}