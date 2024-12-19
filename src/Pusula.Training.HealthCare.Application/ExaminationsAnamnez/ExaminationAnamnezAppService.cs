using AutoMapper.Internal.Mappers;
using Pusula.Training.HealthCare.Examinations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ExaminationsAnamnez;

public class ExaminationAnamnezAppService : ApplicationService
{
    private readonly ExaminationAnamnezManager _manager;
    private readonly IExaminationAnamnezRepository _repository;

    public ExaminationAnamnezAppService(
        ExaminationAnamnezManager manager,
        IExaminationAnamnezRepository repository)
    {
        _manager = manager;
        _repository = repository;
    }

    public async Task<List<ExaminationAnamnezDto>> GetListAsync(GetExaminationAnamnezInput input)
    {
        var list = await _repository.GetListAsync(x =>
            (!input.ExaminationId.HasValue || x.ExaminationId == input.ExaminationId) &&
            (!input.StartDate.HasValue || x.StartDate.Date == input.StartDate.Value.Date));

        return ObjectMapper.Map<List<ExaminationAnamnez>, List<ExaminationAnamnezDto>>(list);
    }

    public async Task<ExaminationAnamnezDto> CreateAsync(ExaminationAnamnezCreateDto input)
    {

        var entity = new ExaminationAnamnez(
            GuidGenerator.Create(),
            input.ExaminationId,
            input.Complaint,
            input.History,
            input.StartDate);
        await _repository.InsertAsync(entity);

        return ObjectMapper.Map<ExaminationAnamnez, ExaminationAnamnezDto>(entity);
    }

    public async Task<ExaminationAnamnezDto> UpdateAsync(Guid id, ExaminationAnamnezUpdateDto input)
    {
        var entity = await _manager.UpdateAsync(
            id,
            input.Complaint,
            input.History,
            input.StartDate);

        return ObjectMapper.Map<ExaminationAnamnez, ExaminationAnamnezDto>(entity);
    }
    public async Task<ExaminationAnamnezDto> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id);
        return ObjectMapper.Map<ExaminationAnamnez, ExaminationAnamnezDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
