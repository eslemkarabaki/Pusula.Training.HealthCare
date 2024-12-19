using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public class ProtocolTypeActionUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(ProtocolTypeActionConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}