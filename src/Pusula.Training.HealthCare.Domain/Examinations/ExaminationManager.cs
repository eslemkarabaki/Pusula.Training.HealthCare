using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationManager : DomainService
    {
        private readonly IExaminationRepository _examinationRepository;

        public ExaminationManager(IExaminationRepository examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }

        // Create a new Examination
        public async Task<Examination> CreateAsync(
            Guid protocolId,
            Guid doctorId,
            Guid patientId,
            string summaryDocument,
            DateTime startDate)
        {
            var examination = new Examination(
                GuidGenerator.Create(),
                protocolId,
                doctorId,
                patientId,
                summaryDocument,
                startDate
                
            );

            return await _examinationRepository.InsertAsync(examination);
        }

        // Get a single Examination
        public async Task<Examination> GetAsync(Guid id)
        {
            return await _examinationRepository.GetAsync(id);
        }

        // Update an existing Examination
        public async Task<Examination> UpdateAsync(
            Guid id,
            string summaryDocument,
            ExaminationDiagnosis examinationDiagnoses,
            ExaminationAnamnez examinationAnamnez,
            ExaminationPhysical examinationPhysical)
            
        {
            Check.NotNullOrWhiteSpace(summaryDocument, nameof(summaryDocument));

            var examination = await _examinationRepository.GetAsync(id);
            id = examination.Id;
            examination.SummaryDocument = summaryDocument;
            examination.ExaminationAnamnez = examinationAnamnez;
            examination.ExaminationDiagnoses = examinationDiagnoses;
            examination.ExaminationPhysical = examinationPhysical;
            return await _examinationRepository.UpdateAsync(examination);
        }

        // Delete an Examination
        public async Task DeleteAsync(Guid id)
        {
            await _examinationRepository.DeleteAsync(id);
        }
    }
}
