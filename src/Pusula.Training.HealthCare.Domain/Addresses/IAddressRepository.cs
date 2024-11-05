using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Addresses;

public interface IAddressRepository : IRepository<Address, Guid>
{
}