using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;


namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class RadiologyExaminationGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(RadiologyExaminationGroupConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [StringLength(RadiologyExaminationGroupConsts.DescriptionMaxLength)]
        public string? Description { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
