using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class ExaminationPhysicalAppService : ApplicationService, IExaminationPhysicalAppService
    {
        private readonly IRepository<ExaminationPhysical, Guid> _repository;

        public ExaminationPhysicalAppService(IRepository<ExaminationPhysical, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ExaminationPhysicalDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return ObjectMapper.Map<ExaminationPhysical, ExaminationPhysicalDto>(entity);
        }

        public async Task<List<ExaminationPhysicalDto>> GetListAsync(GetExaminationPhysicalInput input)
        {
            var query = await _repository.GetQueryableAsync();

            // Filtreleme ve sıralama (opsiyonel)
            query = query
                .WhereIf(input.ExaminationId.HasValue, e => e.ExaminationId == input.ExaminationId);

            var items = query
                .OrderByDescending(e => e.CreationTime)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            // Listeyi DTO'ya dönüştür
            return ObjectMapper.Map<List<ExaminationPhysical>, List<ExaminationPhysicalDto>>(items);
        }

        public async Task<ExaminationPhysicalDto> CreateAsync(ExaminationPhysicalCreateDto input)
        {
            var entity = new ExaminationPhysical(
                GuidGenerator.Create(),
                input.ExaminationId,
                input.Weight,
                input.Height,
                input.BodyMassIndex,
                input.VitalAge,
                input.Fever,
                input.Pulse,
                input.SystolicBloodPressure,
                input.DiastolicBloodPressure,
                input.SPO2,
                input.PhysicalNote
            );

            await _repository.InsertAsync(entity);

            return ObjectMapper.Map<ExaminationPhysical, ExaminationPhysicalDto>(entity);
        }


        public async Task<ExaminationPhysicalDto> UpdateAsync(Guid id, ExaminationPhysicalUpdateDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);

            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<ExaminationPhysical, ExaminationPhysicalDto>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
