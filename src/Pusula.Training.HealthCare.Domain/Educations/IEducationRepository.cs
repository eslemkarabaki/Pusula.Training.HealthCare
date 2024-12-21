using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Educations;

public interface IEducationRepository : IRepository<Education, Guid>
{
}