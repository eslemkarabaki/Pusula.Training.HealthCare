using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Vaccines;

public class VaccineUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(VaccineConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}