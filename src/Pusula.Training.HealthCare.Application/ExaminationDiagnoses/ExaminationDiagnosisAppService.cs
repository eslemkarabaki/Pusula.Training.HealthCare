using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisAppService : ApplicationService, IExaminationDiagnosisAppService
    {
        private readonly IExaminationDiagnosisRepository _repository;

        public ExaminationDiagnosisAppService(IExaminationDiagnosisRepository repository)
        {
            _repository = repository;
        }


        public async Task<ExaminationDiagnosisDto> CreateAsync(ExaminationDiagnosisCreateDto input)
        {
            var entity = new ExaminationDiagnosis(
                GuidGenerator.Create(),
                input.ExaminationId,
                input.DiagnosisId,
                input.Explanation,
                input.Type
            );

            await _repository.InsertAsync(entity);
            return ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(entity);
        }

        public async Task<ExaminationDiagnosisDto> UpdateAsync(Guid id, ExaminationDiagnosisUpdateDto input)
        {
            var entity = await _repository.GetAsync(id);

            entity = new ExaminationDiagnosis(
                id,
                input.ExaminationId,
                input.DiagnosisId,
                input.Explanation,
                input.Type
            );

            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(entity);
        }

        public async Task<ExaminationDiagnosisDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(entity);
        }

        public async Task<List<ExaminationDiagnosisDto>> GetListAsync(GetExaminationDiagnosisInput input)
        {
            var list = await _repository.GetListAsync(
                input.ExaminationId,
                input.DiagnosisId,
                input.Explanation,
                input.Type
            );

            return ObjectMapper.Map<List<ExaminationDiagnosis>, List<ExaminationDiagnosisDto>>(list);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

