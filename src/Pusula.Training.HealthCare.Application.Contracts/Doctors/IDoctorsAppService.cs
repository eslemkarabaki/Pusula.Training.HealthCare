using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Doctors
{
    public interface IDoctorsAppService : IApplicationService
    {
        Task<DoctorDto> CreateAsync(DoctorCreateDto input);
        Task DeleteAllAsync(GetDoctorsInput input);
        Task DeleteAsync(Guid id);
        Task DeleteByIdsAsync(List<Guid> doctorIds);
        Task<DoctorDto> GetAsync(Guid id);
        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input);
        Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input);
        Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input);

        public interface IDoctorsAppService
        {
            Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input);
            Task<DoctorDto> GetAsync(Guid id);
            Task<DoctorDto> CreateAsync(DoctorCreateDto input);
            Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input);
            Task DeleteAsync(Guid id);
            Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input);
            Task<DownloadTokenResultDto> GetDownloadTokenAsync();
            Task DeleteByIdsAsync(List<Guid> doctorIds);
            Task DeleteAllAsync(GetDoctorsInput input);
        }

    }
}
