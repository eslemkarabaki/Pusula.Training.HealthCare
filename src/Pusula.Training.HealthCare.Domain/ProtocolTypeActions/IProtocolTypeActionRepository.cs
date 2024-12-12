using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public interface IProtocolTypeActionRepository : IRepository<ProtocolTypeAction, Guid>
{
}