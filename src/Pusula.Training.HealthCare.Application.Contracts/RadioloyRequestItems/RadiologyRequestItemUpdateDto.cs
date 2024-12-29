using System;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemUpdateDto : IHasConcurrencyStamp
{
    public Guid RequestId { get; set; }
    public Guid ExaminationId { get; set; }
    public string Result { get; set; }
    public DateTime ResultDate { get; set; }
    public RadiologyRequestItemState State { get; set; } 
    public string ConcurrencyStamp { get; set; } = null!;
}