using System.ComponentModel.DataAnnotations; 
namespace Pusula.Training.HealthCare.Hospitals
{
    public class HospitalUpdateDto
    {
        [Required]
        [StringLength(HospitalConsts.NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(HospitalConsts.AddressMaxLength)]
        public string Address { get; set; } = string.Empty;
        public string[]? DepartmentNames { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;

    }
}
