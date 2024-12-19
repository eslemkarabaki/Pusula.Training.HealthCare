using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Pusula.Training.HealthCare.Shared;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientAppService : IApplicationService
{
    Task<PatientDto> GetAsync(GetPatientInput input);
    Task<PatientWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(GetPatientInput input);

    Task<PagedResultDto<PatientDto>> GetListAsync(GetPatientsInput input);

    Task<PagedResultDto<PatientWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetPatientsInput input
    );

    Task<List<AddressDto>> GetPatientAddressesWithDetailsAsync(Guid patientId);

    Task DeleteAsync(Guid id);

    Task<PatientDto> CreateAsync(PatientCreateDto input);

    Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input);

    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> patientIds);

    Task DeleteAllAsync(GetPatientsInput input);

    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
}