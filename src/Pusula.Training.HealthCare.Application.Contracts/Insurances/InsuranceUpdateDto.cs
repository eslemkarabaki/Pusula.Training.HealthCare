using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Insurances
{
    public class InsuranceUpdateDto: IHasConcurrencyStamp
    {
        [Required]
        [StringLength(InsuranceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
