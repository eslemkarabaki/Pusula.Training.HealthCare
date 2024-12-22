using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Operations;

public class OperationUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(OperationConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}