using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Doctors;

public interface IDoctorAppService : IApplicationService
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
}