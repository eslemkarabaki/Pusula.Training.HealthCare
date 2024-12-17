using Pusula.Training.HealthCare.Diagnoses;
using System.Threading.Tasks;
using System;
using Pusula.Training.HealthCare;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Pusula.Training.HealthCare.GlobalExceptions;

public class DiagnosisAppService : HealthCareAppService, IDiagnosisAppService
{
    private readonly IDiagnosisRepository _diagnosisRepository;
    private readonly DiagnosisManager _diagnosisManager;

    public DiagnosisAppService(
        IDiagnosisRepository diagnosisRepository,
        DiagnosisManager diagnosisManager)
    {
        _diagnosisRepository = diagnosisRepository;
        _diagnosisManager = diagnosisManager;
    }
    public async Task<DiagnosisDto> CreateAsync(DiagnosisCreateDto input)
    {
      var diagnosis = await _diagnosisManager.CreateAsync(input.Code, input.Name);
        return ObjectMapper.Map<Diagnosis, DiagnosisDto>(diagnosis);
    }
    public async Task DeleteAsync(Guid id)
    {
        var diagnosis = await _diagnosisRepository.GetAsync(id);
        GlobalException.ThrowIf(diagnosis is null, "Diagnosis is null!", "DiagnosisCode");
        await _diagnosisRepository.DeleteAsync(diagnosis);
    }
    public async Task<DiagnosisDto> GetAsync(Guid id)
    {
        var diagnosis = await _diagnosisRepository.GetAsync(id);
        return ObjectMapper.Map<Diagnosis, DiagnosisDto>(diagnosis);
    }
    public async Task<PagedResultDto<DiagnosisDto>> GetListAsync(GetDiagnosisInput input)
    {
        var diagnoses = await _diagnosisRepository.GetListAsync();
        var diagnosisDtos = ObjectMapper.Map<List<Diagnosis>, List<DiagnosisDto>>(diagnoses);
        return new PagedResultDto<DiagnosisDto>(
            diagnosisDtos.Count,
            diagnosisDtos);
    }
    public async Task<DiagnosisDto> UpdateAsync(Guid id, DiagnosisUpdateDto input)
    {
     var diagnosis = await _diagnosisManager.UptadeAsync(id, input.Code, input.Name);
     return ObjectMapper.Map<Diagnosis, DiagnosisDto>(diagnosis);
    }
}
