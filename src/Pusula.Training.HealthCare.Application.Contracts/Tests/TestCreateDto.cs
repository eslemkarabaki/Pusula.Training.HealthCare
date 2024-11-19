using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Tests
{
    public class TestCreateDto
    {
        [Required]
        [StringLength(TestConsts.CodeMaxLength)]
        public string Code { get; set; } = null!;

        [Required]
        [StringLength(TestConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public Guid TestGroupId { get; set; }
    }
}
