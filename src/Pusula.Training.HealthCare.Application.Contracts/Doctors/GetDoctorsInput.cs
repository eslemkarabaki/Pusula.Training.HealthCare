using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.Doctors
{
    public class GetDoctorsInput : PagedAndSortedResultRequestDto
    {
        public string? WorkingHours;

        public string? FilterText { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? TitleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? HospitalId { get; set; }

        public GetDoctorsInput()
        {
        }
    }
}
