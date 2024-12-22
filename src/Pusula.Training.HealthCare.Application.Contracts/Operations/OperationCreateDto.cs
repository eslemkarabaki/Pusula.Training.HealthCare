using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Operations;

public class OperationCreateDto
{
    [Required]
    [StringLength(OperationConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}