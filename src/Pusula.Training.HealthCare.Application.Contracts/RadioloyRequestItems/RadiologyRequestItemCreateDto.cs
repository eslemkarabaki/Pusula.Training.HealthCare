using Pusula.Training.HealthCare.DataAnnotations;
using System; 
using System.ComponentModel.DataAnnotations; 

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid RequestId { get; set; }
    [Required]
    [NotEmptyGuid]
    public Guid ExaminationId { get; set; }
    [Required]
    [StringLength(RadiologyRequestItemConsts.ResultMaxLength, MinimumLength = 3)]
    public string Result { get; set; } = string.Empty;
    [Required]
    public DateTime ResultDate { get; set; } = DateTime.Now;
    [Required]
    public RadiologyRequestItemState State { get; set; } = RadiologyRequestItemState.Pending;
}
