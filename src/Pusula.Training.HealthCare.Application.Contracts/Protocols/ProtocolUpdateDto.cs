using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolUpdateDto : IHasConcurrencyStamp
{
    public EnumProtocolStatus Status { get; set; } = EnumProtocolStatus.InProgress;
    public string? Description { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}