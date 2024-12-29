using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Examinations
{
    public interface IExaminationAppService : IApplicationService
    {
        Task<ExaminationDto> CreateAsync(ExaminationCreateDto input);
        Task<ExaminationDto> GetAsync(Guid id);
        Task<PagedResultDto<ExaminationWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(GetExaminationsInput input);
        Task<ExaminationWithNavigationPropertiesDto> GetWithProtocolNoAsync(GetExaminationsInput input);

        Task<ExaminationDto> UpdateAsync(Guid id, ExaminationUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
