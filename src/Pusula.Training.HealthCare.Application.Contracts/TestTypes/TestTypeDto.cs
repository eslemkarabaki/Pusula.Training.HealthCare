using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestTypes;

public class TestTypeDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;
}
