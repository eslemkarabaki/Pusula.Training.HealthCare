using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid PatientId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? DoctorId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? DepartmentId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? ProtocolTypeId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? ProtocolTypeActionId { get; set; }

    [Required]
    [DeniedValues(EnumProtocolStatus.None)]
    public EnumProtocolStatus Status { get; set; } = EnumProtocolStatus.InProgress;

    [StringLength(ProtocolConsts.DescriptionMaxLength)]
    public string? Description { get; set; }
}