using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Insurances
{
    public interface IInsurancesAppService: IApplicationService
    {
        Task<PagedResultDto<InsuranceDto>> GetListAsync(GetInsurancesInput input);
        Task<List<InsuranceDto>> GetListInsurancesAsync();
        Task<InsuranceDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<InsuranceDto> CreateAsync(InsuranceCreateDto input);
        Task<InsuranceDto> UpdateAsync(Guid id, InsuranceUpdateDto input);
        Task<IRemoteStreamContent> GetListAsExcelFileAsync(InsuranceExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> insuranceIds);
        Task DeleteAllAsync(GetInsurancesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
