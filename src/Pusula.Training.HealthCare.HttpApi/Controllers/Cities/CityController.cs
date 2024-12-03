using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Controllers.Cities;

[RemoteService]
[Area("app")]
[ControllerName("City")]
[Route("api/app/city")]
public class CityController(ICityAppService cityAppService) : HealthCareController, ICityAppService
{
    [HttpGet("get/{id:guid}")]
    public async Task<CityDto> GetAsync(Guid id) => await cityAppService.GetAsync(id);

    [HttpGet("get/country/{cityId:guid}")]
    public async Task<CountryDto> GetCountryAsync(Guid cityId) => await cityAppService.GetCountryAsync(cityId);

    [HttpGet("get/all/with-details")]
    public async Task<List<CityDto>> GetListWithDetailsAsync() => await cityAppService.GetListWithDetailsAsync();

    [HttpGet("get/all/with-details/{countryId:guid}")]
    public async Task<List<CityDto>> GetListWithDetailsAsync(Guid countryId) =>
        await cityAppService.GetListWithDetailsAsync(countryId);

    [HttpGet("get/all/with-details/filtered")]
    public async Task<PagedResultDto<CityDto>> GetListWithDetailsAsync([FromBody] GetCitiesInput input) =>
        await cityAppService.GetListWithDetailsAsync(input);

    [HttpPost]
    public async Task<CityDto> CreateAsync(CityCreateDto input) => await cityAppService.CreateAsync(input);

    [HttpPut("{id:guid}")]
    public async Task<CityDto> UpdateAsync(Guid id, [FromBody] CityUpdateDto input) =>
        await cityAppService.UpdateAsync(id, input);

    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync(Guid id) => await cityAppService.DeleteAsync(id);

    [HttpDelete]
    public async Task DeleteByIdsAsync([FromBody] List<Guid> patientIds) =>
        await cityAppService.DeleteByIdsAsync(patientIds);

    [HttpDelete("all")]
    public async Task DeleteAllAsync(GetCitiesInput input) => await cityAppService.DeleteAllAsync(input);
}