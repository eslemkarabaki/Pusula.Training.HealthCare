using Pusula.Training.HealthCare.DataAnnotations;
using System; 
using System.ComponentModel.DataAnnotations; 

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class RadiologyRequestCreateDto
{
    [Required]
    public DateTime RequestDate { get; set; } = DateTime.Now;
    [Required]
    [NotEmptyGuid]
    public Guid ProtocolId { get; set; }
    [Required]
    [NotEmptyGuid]
    public Guid DepartmentId { get; set; }
    [Required]
    [NotEmptyGuid]
    public Guid DoctorId { get; set; }
}