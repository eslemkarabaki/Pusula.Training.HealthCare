using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Districts;

public interface IDistrictRepository : IRepository<District, Guid>
{
}