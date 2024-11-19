using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.WorkLists;

public class WorkListCreateDto
{
    [Required]
    [StringLength(100)]
    public string Code { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Required]
    public Guid DepartmentId { get; set; }
}
