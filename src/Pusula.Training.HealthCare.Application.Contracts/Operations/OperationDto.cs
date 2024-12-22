using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Operations;

public class OperationDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}