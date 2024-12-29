using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Vaccines;

public class VaccineDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}