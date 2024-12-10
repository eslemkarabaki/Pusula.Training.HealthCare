using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Controllers.Districts;

[RemoteService]
[Area("app")]
[ControllerName("District")]
[Route("api/app/district")]
public class DistrictController(IDistrictAppService districtAppService) : HealthCareController
{
    [HttpGet]
    public async Task<List<DistrictDto>> GetListWithDetailsAsync() =>
        await districtAppService.GetListWithDetailsAsync();
}