using System; 

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class RadiologyRequestExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public DateTime RequestDate { get; set; }
    public Guid ProtocolId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DoctorId { get; set; }
    public RadiologyRequestExcelDownloadDto() { }
}
