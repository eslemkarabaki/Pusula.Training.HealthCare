using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.AppDefaults;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.PatientTypes;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare;

public class HealthCareDataSeederContributor(
    ICityRepository cityRepository,
    ICountryRepository countryRepository,
    IDistrictRepository districtRepository,
    IAddressRepository addressRepository,
    IPatientRepository patientRepository,
    IPatientTypeRepository patientTypeRepository,
    IAppDefaultRepository appDefaultRepository,
    IGuidGenerator guidGenerator
)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        if (!await patientRepository.AnyAsync())
        {
            var countries = await SeedCountriesAsync();
            var cities = await SeedCitiesAsync(countries);
            var districts = await SeedDistrictsAsync(cities);
            var patientTypes = await SeedPatientTypesAsync();
            var patients = await SeedPatientsAsync(countries, patientTypes);
            await SeedAddressesAsync(patients, districts);
        }

        if (!await appDefaultRepository.AnyAsync())
        {
            await appDefaultRepository.InsertAsync(new AppDefault(guidGenerator.Create()));
        }
    }

    // Country
    private async Task<IEnumerable<Country>> SeedCountriesAsync()
    {
        IEnumerable<Country> countries =
        [
            new(guidGenerator.Create(), "Türkiye", "TR", "90", true),
            new(guidGenerator.Create(), "İngiltere", "UK", "1"),
            new(guidGenerator.Create(), "Almanya", "GE", "1"),
            new(guidGenerator.Create(), "Fransa", "FR", "1"),
            new(guidGenerator.Create(), "Suriye", "SY", "1")
        ];

        await countryRepository.InsertManyAsync(countries, true);
        return countries;
    }

    // City
    private async Task<IEnumerable<Guid>> SeedCitiesAsync(IEnumerable<Country> countries)
    {
        IEnumerable<City> cities =
        [
            new(guidGenerator.Create(), countries.ElementAt(0).Id, "İstanbul"),
            new(guidGenerator.Create(), countries.ElementAt(0).Id, "Ankara"),
            new(guidGenerator.Create(), countries.ElementAt(1).Id, "Londra"),
            new(guidGenerator.Create(), countries.ElementAt(2).Id, "Berlin"),
            new(guidGenerator.Create(), countries.ElementAt(3).Id, "Paris"),
            new(guidGenerator.Create(), countries.ElementAt(4).Id, "Şam")
        ];

        return await SeedEntitiesAsync(cities, e => cityRepository.InsertManyAsync(e, true));
    }

    // District
    private async Task<IEnumerable<Guid>> SeedDistrictsAsync(IEnumerable<Guid> cities)
    {
        IEnumerable<District> districts =
        [
            new(guidGenerator.Create(), cities.ElementAt(0), "Ümraniye"),
            new(guidGenerator.Create(), cities.ElementAt(0), "Maltepe"),
            new(guidGenerator.Create(), cities.ElementAt(1), "Çankaya"),
            new(guidGenerator.Create(), cities.ElementAt(1), "Etimesgut"),
            new(guidGenerator.Create(), cities.ElementAt(2), "Westminster"),
            new(guidGenerator.Create(), cities.ElementAt(3), "Charlottenburg-Wilmersdorf"),
            new(guidGenerator.Create(), cities.ElementAt(4), "Louvre"),
            new(guidGenerator.Create(), cities.ElementAt(5), "El-Midan")
        ];

        return await SeedEntitiesAsync(districts, e => districtRepository.InsertManyAsync(e, true));
    }

    // Patient types
    private async Task<IEnumerable<Guid>> SeedPatientTypesAsync()
    {
        IEnumerable<PatientType> types =
        [
            new(guidGenerator.Create(), "Normal"),
            new(guidGenerator.Create(), "Yabancı"),
            new(guidGenerator.Create(), "VIP")
        ];

        return await SeedEntitiesAsync(types, e => patientTypeRepository.InsertManyAsync(e, true));
    }

    // Patient
    private async Task<IEnumerable<Guid>> SeedPatientsAsync(
        IEnumerable<Country> countries,
        IEnumerable<Guid> patientTypes
    )
    {
        IEnumerable<Patient> patients =
        [
            new(
                guidGenerator.Create(), countries.ElementAt(0).Id, patientTypes.ElementAt(0), "Selçuk", "Şahin",
                new DateTime(1998, 5, 18),
                "12345678900", null, "muselcuksahin@gmail.com", countries.ElementAt(0).PhoneCode, "5555555555", null,
                null, EnumGender.Male, EnumBloodType.AbPositive,
                EnumMaritalStatus.Single
            ),
            new(
                guidGenerator.Create(), countries.ElementAt(1).Id, patientTypes.ElementAt(1), "Joel", "Bond",
                new DateTime(1991, 8, 7),
                null, "64279023471", "johndoe@gmail.com", countries.ElementAt(0).PhoneCode, "07836668374", null, null,
                EnumGender.Male, EnumBloodType.BPositive,
                EnumMaritalStatus.Married
            ),
            new(
                guidGenerator.Create(), countries.ElementAt(2).Id, patientTypes.ElementAt(2), "Kristin", "Saenger",
                new DateTime(1970, 8, 23),
                null, "44748015944", "kristinSaenger@dayrep.com", countries.ElementAt(2).PhoneCode, "0471266747", null,
                null, EnumGender.Female, EnumBloodType.ZeroPositive,
                EnumMaritalStatus.Single
            )
        ];

        return await SeedEntitiesAsync(patients, e => patientRepository.InsertManyAsync(e, true));
    }

    // Address
    private async Task SeedAddressesAsync(IEnumerable<Guid> patients, IEnumerable<Guid> districts)
    {
        IEnumerable<Address> addresses =
        [
            new(guidGenerator.Create(), patients.ElementAt(0), districts.ElementAt(0), "Ev", "Asya Sokak"),
            new(guidGenerator.Create(), patients.ElementAt(1), districts.ElementAt(4), "Ev", "lorem"),
            new(guidGenerator.Create(), patients.ElementAt(2), districts.ElementAt(5), "Ev", "ipsum")
        ];

        await addressRepository.InsertManyAsync(addresses, true);
    }

    // Generic Entities
    private async Task<List<Guid>> SeedEntitiesAsync<T>(
        IEnumerable<T> entities,
        Func<IEnumerable<T>, Task> insertFunction
    )
        where T : AggregateRoot<Guid>
    {
        await insertFunction(entities);
        return entities.Select(e => e.Id).ToList();
    }
}