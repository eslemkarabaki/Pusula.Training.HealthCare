using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class RadiologyExaminationGroupCreateDto
    {
        [Required]
        [StringLength(RadiologyExaminationGroupConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        [StringLength(RadiologyExaminationGroupConsts.DescriptionMaxLength)]
        public string? Description { get; set; }

    }
}
