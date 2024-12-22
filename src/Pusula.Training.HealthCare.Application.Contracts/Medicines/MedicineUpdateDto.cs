using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Medicines;

public class MedicineUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(MedicineConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}