using System; 
using System.ComponentModel.DataAnnotations; 
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExaminationUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(RadiologyExaminationConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(RadiologyExaminationConsts.MaxCodeLength)]
        public string ExaminationCode { get; set; } = null!;

        public Guid GroupId { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;

    }
}
