using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Titles;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorWithNavigationProperties
{
    public Doctor Doctor { get; set; } = null!;
    public Title Title { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public Hospital Hospital { get; set; } = null!;
}