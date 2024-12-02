using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public interface IProtocolTypeRepository : IRepository<ProtocolType, Guid>
{
}