using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Jobs;

[RemoteService(false)]
[Authorize(HealthCarePermissions.Jobs.Default)]
public class JobAppService(IJobRepository jobRepository, JobManager jobManager)
    : HealthCareAppService, IJobAppService
{
    public async Task<JobDto> GetAsync(Guid id) => ObjectMapper.Map<Job, JobDto>(await jobRepository.GetAsync(id));

    public async Task<List<JobDto>> GetListAsync(GetJobsInput input) =>
        ObjectMapper.Map<List<Job>, List<JobDto>>(
            await jobRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.Jobs.Create)]
    public async Task<JobDto> CreateAsync(JobCreateDto input)
    {
        var job = await jobManager.CreateAsync(input.Name);
        return ObjectMapper.Map<Job, JobDto>(job);
    }

    [Authorize(HealthCarePermissions.Jobs.Edit)]
    public async Task<JobDto> UpdateAsync(Guid id, JobUpdateDto input)
    {
        var job = await jobManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<Job, JobDto>(job);
    }

    [Authorize(HealthCarePermissions.Jobs.Delete)]
    public async Task DeleteAsync(Guid id) => await jobRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Jobs.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await jobRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Jobs.Delete)]
    public async Task DeleteAllAsync(GetJobsInput input) => await jobRepository.DeleteAllAsync(input.Name);
}