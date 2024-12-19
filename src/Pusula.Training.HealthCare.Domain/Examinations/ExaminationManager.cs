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
            Check.NotNullOrWhiteSpace(summaryDocument, nameof(summaryDocument));

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
            DateTime startDate)
        {
            Check.NotNullOrWhiteSpace(summaryDocument, nameof(summaryDocument));

            var examination = await _examinationRepository.GetAsync(id);

            examination.SummaryDocument = summaryDocument;
            examination.StartDate = startDate;

            return await _examinationRepository.UpdateAsync(examination);
        }

        // Delete an Examination
        public async Task DeleteAsync(Guid id)
        {
            await _examinationRepository.DeleteAsync(id);
        }
        
    }
}
