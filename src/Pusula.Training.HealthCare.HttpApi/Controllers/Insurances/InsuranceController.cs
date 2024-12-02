using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.Insurances
{

    [RemoteService]
    [Area("app")]
    [ControllerName("Insurance")]
    [Route("api/app/insurances")]
    public class InsuranceController : HealthCareController, IInsurancesAppService
    {
        protected IInsurancesAppService _insurancesAppService;

        public InsuranceController(IInsurancesAppService insurancesAppService)
        {
            _insurancesAppService = insurancesAppService;
        }


        [HttpGet]
        [Route("list-insurances")]
        public virtual Task<List<InsuranceDto>> GetListInsurancesAsync() => _insurancesAppService.GetListInsurancesAsync();

        [HttpGet("all")]
        public virtual Task<PagedResultDto<InsuranceDto>> GetListAsync(GetInsurancesInput input) => _insurancesAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<InsuranceDto> GetAsync(Guid id) => _insurancesAppService.GetAsync(id);

        [HttpPost]
        public virtual Task<InsuranceDto> CreateAsync(InsuranceCreateDto input) => _insurancesAppService.CreateAsync(input);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<InsuranceDto> UpdateAsync(Guid id, InsuranceUpdateDto input) => _insurancesAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => _insurancesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(InsuranceExcelDownloadDto input) => _insurancesAppService.GetListAsExcelFileAsync(input);

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => _insurancesAppService.GetDownloadTokenAsync();

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> insuranceIds) => _insurancesAppService.DeleteByIdsAsync(insuranceIds);

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetInsurancesInput input) => _insurancesAppService.DeleteAllAsync(input);

    }
}
