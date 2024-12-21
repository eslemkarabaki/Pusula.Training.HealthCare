using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Jobs;

public interface IJobRepository : IRepository<Job, Guid>
{
}