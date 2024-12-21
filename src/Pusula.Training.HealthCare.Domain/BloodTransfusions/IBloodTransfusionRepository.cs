using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public interface IBloodTransfusionRepository : IRepository<BloodTransfusion, Guid>
{
}