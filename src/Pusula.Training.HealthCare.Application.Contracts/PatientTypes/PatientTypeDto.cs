using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeDto : EntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; } = null!;
    public string ConcurrencyStamp { get; set; } = null!;
}