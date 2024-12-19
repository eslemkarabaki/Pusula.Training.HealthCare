using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(CountryConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(CountryConsts.IsoMaxLength)]
    public string Iso { get; set; } = null!;

    [Required]
    [StringLength(CountryConsts.PhoneCodeMaxLength)]
    public string PhoneCode { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}