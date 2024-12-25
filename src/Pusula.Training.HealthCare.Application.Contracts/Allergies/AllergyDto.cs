using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Allergies;

public class AllergyDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}