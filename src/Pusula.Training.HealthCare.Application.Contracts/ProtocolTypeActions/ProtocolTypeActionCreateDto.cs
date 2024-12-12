using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public class ProtocolTypeActionCreateDto
{
    [Required] [NotEmptyGuid] public Guid ProtocolTypeId { get; set; }

    [Required]
    [StringLength(ProtocolTypeActionConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}