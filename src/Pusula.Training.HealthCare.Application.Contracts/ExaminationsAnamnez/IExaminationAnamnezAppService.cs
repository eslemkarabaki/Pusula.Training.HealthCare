using Pusula.Training.HealthCare.ExaminationDiagnoses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Examinations
{
    public interface IExaminationAnamnezAppService
    {
        Task<List<ExaminationAnamnezDto>> GetListAsync(GetExaminationsInput input);
        Task<ExaminationAnamnezDto> CreateAsync(ExaminationAnamnezCreateDto input);
        Task<ExaminationAnamnezDto> GetAsync(Guid id);
        Task<ExaminationAnamnezDto> UpdateAsync(Guid id, ExaminationAnamnezUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
