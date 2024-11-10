using System;

namespace Pusula.Training.HealthCare.DoctorWithDepartments
{
    public class DoctorWithDepartment
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string WorkingHours { get; set; } = null!;
        public Guid TitleId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid HospitalId { get; set; }
        public string DepartmentName { get; set; } = null!; // Department adı burada yer alacak

        // Diğer gerekli özellikler
    }
}
