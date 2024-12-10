using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorUpdateDto : IHasConcurrencyStamp
{
    [Required] [StringLength(128)] public string FirstName { get; set; } = null!;

    [Required] [StringLength(128)] public string LastName { get; set; } = null!;

    [Required] [StringLength(256)] public string WorkingHours { get; set; } = null!;

    [Required] public Guid TitleId { get; set; }

    [Required] public Guid DepartmentId { get; set; }

    [Required] public Guid HospitalId { get; set; }

    [Required] public IdentityUserUpdateDto User { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}