using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Jobs;

public class JobManager(IJobRepository jobRepository) : DomainService
{
    public virtual async Task<Job> CreateAsync(
        string name
    )
    {
        var entity = new Job(GuidGenerator.Create(), name);
        return await jobRepository.InsertAsync(entity);
    }

    public virtual async Task<Job> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await jobRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await jobRepository.UpdateAsync(entity);
    }
}