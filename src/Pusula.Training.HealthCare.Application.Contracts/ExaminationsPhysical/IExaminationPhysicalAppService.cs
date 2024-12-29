
using System;using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public interface IExaminationPhysicalAppService : IApplicationService
    {
        Task<ExaminationPhysicalDto> CreateAsync(ExaminationPhysicalCreateDto input);
        Task<ExaminationPhysicalDto> UpdateAsync(Guid id, ExaminationPhysicalUpdateDto input);
        Task<ExaminationPhysicalDto> GetAsync(Guid id);
        Task<List<ExaminationPhysicalDto>> GetListAsync(GetExaminationPhysicalInput input);
        Task DeleteAsync(Guid id);
    }
}
