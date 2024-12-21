using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Operations;

public interface IOperationRepository : IRepository<Operation, Guid>
{
}