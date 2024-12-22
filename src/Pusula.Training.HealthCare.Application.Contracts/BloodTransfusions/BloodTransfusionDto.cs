using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class BloodTransfusionDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}