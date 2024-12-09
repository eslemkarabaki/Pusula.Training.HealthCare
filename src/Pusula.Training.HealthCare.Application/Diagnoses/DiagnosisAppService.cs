using AutoMapper.Internal.Mappers;
using Pusula.Training.HealthCare.Diagnoses;
using System.Threading.Tasks;
using System;
using Volo.Abp.Guids;
using Pusula.Training.HealthCare;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;

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
        if (diagnosis == null)
        {
            throw new EntityNotFoundException(typeof(Diagnosis), id);
        }
        await _diagnosisRepository.DeleteAsync(diagnosis);
    }


    public async Task<DiagnosisDto> GetAsync(Guid id)
    {
        var diagnosis = await _diagnosisRepository.GetAsync(id);
        return ObjectMapper.Map<Diagnosis, DiagnosisDto>(diagnosis);
    }


    public async Task<PagedResultDto<DiagnosisDto>> GetListAsync(GetDiagnosisInput input)
    {
        var queryable = await _diagnosisRepository.GetQueryableAsync();
        queryable = queryable.WhereIf(!string.IsNullOrWhiteSpace(input.Name),
                                      d => d.Name.Contains(input.Name));
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var items = await AsyncExecuter.ToListAsync(
            queryable
                .OrderBy(input.Sorting ?? nameof(Diagnosis.Name))
                .PageBy(input.SkipCount, input.MaxResultCount)
        );
        return new PagedResultDto<DiagnosisDto>(
            totalCount,
            ObjectMapper.Map<List<Diagnosis>, List<DiagnosisDto>>(items)
        );
    }
    public async Task<DiagnosisDto> UpdateAsync(Guid id, DiagnosisUpdateDto input)
    {
     var diagnosis = await _diagnosisManager.UptadeAsync(id, input.Code, input.Name);
     return ObjectMapper.Map<Diagnosis, DiagnosisDto>(diagnosis);
    }

}
