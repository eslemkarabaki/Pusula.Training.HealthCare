using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Jobs;

public interface IJobAppService : IApplicationService
{
    Task<JobDto> GetAsync(Guid id);
    Task<List<JobDto>> GetListAsync(GetJobsInput input);
    Task<JobDto> CreateAsync(JobCreateDto input);

    Task<JobDto> UpdateAsync(Guid id, JobUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetJobsInput input);
}