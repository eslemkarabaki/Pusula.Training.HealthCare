using System;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;

    public string? FilterText { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? WorkingHours { get; set; } 
    public Guid? TitleId { get; set; } 
    public Guid? DepartmentId { get; set; } 
    public Guid? HospitalId { get; set; } 

    public DoctorExcelDownloadDto()
    {
    }
}
