using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Pusula.Training.HealthCare.GlobalExceptions;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationAnamnezManager : DomainService
    {
        private readonly IRepository<ExaminationAnamnez, Guid> _examinationAnamnezRepository;

        public ExaminationAnamnezManager(IRepository<ExaminationAnamnez, Guid> examinationAnamnezRepository)
        {
            _examinationAnamnezRepository = examinationAnamnezRepository;
        }

        public async Task<ExaminationAnamnez> CreateAsync(
    
            Guid examinationId,
            string complaint,
            string history,
            DateTime startDate)
        {
            Check.NotNull(complaint, nameof(complaint));
            Check.NotNull(history, nameof(history));

            var examinationAnamnez = new ExaminationAnamnez(GuidGenerator.Create(), examinationId,complaint, history, startDate);

            return await _examinationAnamnezRepository.InsertAsync(examinationAnamnez);
        }

        public async Task<ExaminationAnamnez> UpdateAsync(
            Guid id,
            string complaint,
            string history,
            DateTime startDate)
        {
            var examinationAnamnez = await _examinationAnamnezRepository.GetAsync(id);
            GlobalException.ThrowIf(examinationAnamnez is null, "Examination Anamnez is null", "ExaminationAnamnezCode");

            examinationAnamnez.Complaint = complaint;
            examinationAnamnez.History = history;
            examinationAnamnez.StartDate = startDate;

            return await _examinationAnamnezRepository.UpdateAsync(examinationAnamnez);
        }
    }
}
