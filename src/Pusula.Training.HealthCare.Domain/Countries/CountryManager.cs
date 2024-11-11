using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Countries;

public class CountryManager(ICountryRepository countryRepository) : DomainService
{
    public virtual async Task<Country> CreateAsync(
        string name,
        string abbreviation
    )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(abbreviation, nameof(abbreviation), CountryConsts.AbbreviationMaxLength);

        var country = new Country(GuidGenerator.Create(), name, abbreviation);
        return await countryRepository.InsertAsync(country);
    }

    public virtual async Task<Country> UpdateAsync(
        Guid id,
        string name,
        string abbreviation,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(abbreviation, nameof(abbreviation), CountryConsts.AbbreviationMaxLength);

        var country = await countryRepository.GetAsync(id);

        country.Name = name;
        country.Abbreviation = abbreviation;

        country.SetConcurrencyStampIfNotNull(concurrencyStamp);

        return await countryRepository.UpdateAsync(country);
    }
}