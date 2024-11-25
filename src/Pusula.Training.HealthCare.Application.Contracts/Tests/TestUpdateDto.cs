using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Tests;

public class TestUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(TestConsts.CodeMaxLength)]
    public string Code { get; set; } = null!;

    [Required]
    [StringLength(TestConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required] public Guid TestGroupId { get; set; }
    [Required] public Guid TestTypeId { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}