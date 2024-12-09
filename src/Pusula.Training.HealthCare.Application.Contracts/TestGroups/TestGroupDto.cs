using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupDto : EntityDto<Guid>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}
