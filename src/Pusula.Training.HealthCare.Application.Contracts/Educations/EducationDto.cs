using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Educations;

public class EducationDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}