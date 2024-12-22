using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Medicines;

public class MedicineDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}