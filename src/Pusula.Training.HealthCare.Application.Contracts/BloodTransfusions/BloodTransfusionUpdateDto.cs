using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class BloodTransfusionUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(BloodTransfusionConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}