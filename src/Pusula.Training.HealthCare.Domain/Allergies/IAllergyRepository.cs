using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Allergies;

public interface IAllergyRepository : IRepository<Allergy, Guid>
{
}