using System;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorExcelDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? WorkingHours { get; set; }
    public Guid? TitleId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? HospitalId { get; set; }
}