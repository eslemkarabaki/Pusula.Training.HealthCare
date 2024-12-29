using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Jobs;

public class JobDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}