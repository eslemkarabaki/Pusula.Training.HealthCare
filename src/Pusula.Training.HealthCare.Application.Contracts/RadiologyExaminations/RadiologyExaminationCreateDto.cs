using System; 
using System.ComponentModel.DataAnnotations; 

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExaminationCreateDto
    {
        [Required]
        [StringLength(RadiologyExaminationConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(RadiologyExaminationConsts.MaxCodeLength)]
        public string ExaminationCode { get; set; } = null!;

        public Guid GroupId { get; set; }
    }
}
