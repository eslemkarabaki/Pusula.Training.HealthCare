using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Hospitals
{
    public interface IHospitalsAppService : IApplicationService
    {
        Task<PagedResultDto<HospitalDto>> GetListAsync(GetHospitalsInput input);

        Task<HospitalDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<HospitalDto> CreateAsync(HospitalCreateDto input);

        Task<HospitalDto> UpdateAsync(Guid id, HospitalUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(HospitalExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> hospitalIds);

        Task DeleteAllAsync(GetHospitalsInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
