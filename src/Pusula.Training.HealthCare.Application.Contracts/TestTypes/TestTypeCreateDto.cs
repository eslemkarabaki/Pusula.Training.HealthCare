using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.TestTypes;

public class TestTypeCreateDto
{
    [Required]
    [StringLength(100)] 
    public string Name { get; set; } = null!;
}
