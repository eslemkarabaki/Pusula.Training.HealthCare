using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.TestTypes;

public class TestTypeUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(100)] // Adjust max length as needed
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}
