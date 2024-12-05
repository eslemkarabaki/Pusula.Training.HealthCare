using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Appointments;

public class AppointmentUpdateDto : IHasConcurrencyStamp
{
    [Required] public DateTime StartTime { get; set; }

    [Required] public DateTime EndTime { get; set; }

    [Required] public EnumStatus Status { get; set; }

    [StringLength(AppointmentConsts.NoteMaxLength, MinimumLength = 3)]
    public string? Notes { get; set; }

    [Required] [NotEmptyGuid] public Guid AppointmentTypeId { get; set; }

    [Required] [NotEmptyGuid] public Guid DepartmentId { get; set; }

    [Required] [NotEmptyGuid] public Guid DoctorId { get; set; }

    [Required] [NotEmptyGuid] public Guid PatientId { get; set; }

    public string? ConcurrencyStamp { get; set; }
}