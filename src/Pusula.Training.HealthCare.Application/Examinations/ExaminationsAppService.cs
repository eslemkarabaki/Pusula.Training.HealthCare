using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationAppService : ApplicationService, IExaminationAppService
    {
        private readonly ExaminationManager _examinationManager;
        private readonly IExaminationRepository _examinationRepository;
        public ExaminationAppService(ExaminationManager  examinationManager, IExaminationRepository examinationRepository)
        {
            _examinationManager = examinationManager;
            _examinationRepository = examinationRepository;
        }


        // Create a new Examination
        public async Task<ExaminationDto> CreateAsync(ExaminationCreateDto input)
        {
            var examination = await _examinationManager.CreateAsync(
                input.ProtocolId,
                input.DoctorId,
                input.PatientId,
                input.SummaryDocument,
                input.StartDate
            );

            return ObjectMapper.Map<Examination, ExaminationDto>(examination);
        }

        // Get a single Examination
        public async Task<ExaminationDto> GetAsync(Guid id)
        {
            var examination = await _examinationManager.GetAsync(id);
            return ObjectMapper.Map<Examination, ExaminationDto>(examination);
        }

        // Get a paginated list of Examinations
        public async Task<PagedResultDto<ExaminationWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(GetExaminationsInput input)
        {
            var examinations = await _examinationRepository.GetListWithNavigationPropertiesAsync(
                input.ProtocolId,
                input.DoctorId,
                input.PatientId,
                input.StartDate,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount
            );
            var totalCount = await _examinationRepository.GetCountAsync(
                input.ProtocolId,
                input.DoctorId,
                input.PatientId,
                input.StartDate
            );
            return new PagedResultDto<ExaminationWithNavigationPropertiesDto>(
                totalCount,
                ObjectMapper.Map<List<ExaminationWithNavigationProperties>, List<ExaminationWithNavigationPropertiesDto>>(examinations)
            );
        }


        // Update an existing Examination
        public async Task<ExaminationDto> UpdateAsync(Guid id, ExaminationUpdateDto input)
        {
            var examination = await _examinationManager.UpdateAsync(
                  id,
                  input.SummaryDocument,
                  ObjectMapper.Map<ExaminationDiagnosisUpdateDto, ExaminationDiagnosis>(input.DiagnosisUpdateDto),
                  ObjectMapper.Map<ExaminationAnamnezUpdateDto, ExaminationAnamnez>(input.AnamnezUpdateDto),
                  ObjectMapper.Map<ExaminationPhysicalUpdateDto, ExaminationPhysical>(input.PhysicalUpdateDto)
            );

            return ObjectMapper.Map<Examination, ExaminationDto>(examination);
        }

        // Delete an Examination
        public async Task DeleteAsync(Guid id)
        {
            await _examinationManager.DeleteAsync(id);
        }
            
        public async Task<ExaminationWithNavigationPropertiesDto> GetWithProtocolNoAsync(GetExaminationsInput input)
        {
           var response = await _examinationRepository.GetWithNavigationPropertiesAsync(input.ProtocolNo);

            return ObjectMapper.Map<ExaminationWithNavigationProperties, ExaminationWithNavigationPropertiesDto>(response);
        }
    }
}
