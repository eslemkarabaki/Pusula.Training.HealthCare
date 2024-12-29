using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(DoctorConsts.FirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(DoctorConsts.LastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [Range(DoctorConsts.AppointmentTimeMin, DoctorConsts.AppointmentTimeMax)]
    public int AppointmentTime { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? TitleId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? DepartmentId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}