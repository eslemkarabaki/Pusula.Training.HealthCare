using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Allergies;

public class AllergyUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(AllergyConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}