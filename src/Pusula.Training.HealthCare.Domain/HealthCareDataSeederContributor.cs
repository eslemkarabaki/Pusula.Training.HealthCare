using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.AppDefaults;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.PatientTypes;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Address = Pusula.Training.HealthCare.Addresses.Address;

namespace Pusula.Training.HealthCare;

public class HealthCareDataSeederContributor(
    ICityRepository cityRepository,
    ICountryRepository countryRepository,
    IDistrictRepository districtRepository,
    IAddressRepository addressRepository,
    IPatientRepository patientRepository,
    IPatientTypeRepository patientTypeRepository,
    IAppDefaultRepository appDefaultRepository,
    IDepartmentRepository departmentRepository,
    IHospitalRepository hospitalRepository,
    ITitleRepository titleRepository,
    IDoctorRepository doctorRepository,
    IAppointmentTypeRepository appointmentTypeRepository,
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
            await appDefaultRepository.InsertAsync(
                new AppDefault(guidGenerator.Create())
                {
                    CurrentCountryId = countries.FirstOrDefault(e => e.IsCurrent)?.Id ?? Guid.Empty
                }
            );
        }

        if (!await doctorRepository.AnyAsync())
        {
           var departments= await SeedDepartmentsAsync();
           var hospital=  await SeedHospitalsAsync();
           var titles= await SeedTitlesAsync();
           await SeedDoctorsAsync(departments, hospital, titles);
        }

        if (!await appointmentTypeRepository.AnyAsync())
        {
            await SeedAppointmentTypesAsync();
        }

    }

    // Country
    private async Task<List<Country>> SeedCountriesAsync()
    {
        List<Country> countries =
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
    private async Task<List<Guid>> SeedCitiesAsync(List<Country> countries)
    {
        List<City> cities =
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
    private async Task<List<Guid>> SeedDistrictsAsync(List<Guid> cities)
    {
        List<District> districts =
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
    private async Task<List<Guid>> SeedPatientTypesAsync()
    {
        List<PatientType> types =
        [
            new(guidGenerator.Create(), "Normal"),
            new(guidGenerator.Create(), "Yabancı"),
            new(guidGenerator.Create(), "VIP")
        ];

        return await SeedEntitiesAsync(types, e => patientTypeRepository.InsertManyAsync(e, true));
    }

    // Patient
    private async Task<List<Guid>> SeedPatientsAsync(
        List<Country> countries,
        List<Guid> patientTypes
    )
    {
        var faker = new Faker<Patient>("tr")
            .CustomInstantiator(
                f =>
                {
                    var country = f.PickRandom(countries);
                    return new Patient(
                        guidGenerator.Create(),
                        country.Id,
                        f.PickRandom(patientTypes),
                        f.Name.FirstName(),
                        f.Name.LastName(),
                        f.Date.Past(100),
                        country.IsCurrent ? f.Random.String2(11, "0123456789") : null,
                        country.IsCurrent ? null : f.Random.String2(11, "0123456789"),
                        f.Internet.Email(),
                        country.PhoneCode,
                        f.Phone.PhoneNumber("##########"),
                        null,
                        null,
                        f.PickRandomWithout<EnumGender>(EnumGender.None),
                        f.PickRandomWithout<EnumBloodType>(EnumBloodType.None),
                        f.PickRandomWithout<EnumMaritalStatus>(EnumMaritalStatus.None)
                    );
                }
            );

        return await SeedEntitiesAsync(faker.Generate(100), e => patientRepository.InsertManyAsync(e, true));
    }

    // Address
    private async Task SeedAddressesAsync(List<Guid> patients, List<Guid> districts)
    {
        var faker = new Faker<Address>("tr")
            .CustomInstantiator(
                f =>
                    new Address(
                        guidGenerator.Create(),
                        f.PickRandom(patients),
                        f.PickRandom(districts),
                        f.Lorem.Word(),
                        f.Address.SecondaryAddress()
                    )
            );

        await addressRepository.InsertManyAsync(faker.Generate(200), true);
    }

    // Department
    private async Task<List<Guid>> SeedDepartmentsAsync()
    {
        var faker = new Faker<Department>("tr")
            .CustomInstantiator(
                f => new Department(
                    guidGenerator.Create(),
                    f.Company.CompanyName(),
                    f.Random.Words(3),
                    f.Random.Number(5, 60)
                )
            );

        return await SeedEntitiesAsync(faker.Generate(25), e => departmentRepository.InsertManyAsync(e, true));
    }

    // Hospital
    private async Task<Hospital> SeedHospitalsAsync()
    {
        var hospital = new Hospital(
            guidGenerator.Create(),
            "Medical Park",
            "İstanbul Ümraniye"
        );

        return await hospitalRepository.InsertAsync(hospital, true);
    }

    // Title
    private async Task<List<Guid>> SeedTitlesAsync()
    {
        List<Title> titles =
        [
            new(guidGenerator.Create(), "Prof."),
            new(guidGenerator.Create(), "Dr.")
        ];

       return await SeedEntitiesAsync(titles, e => titleRepository.InsertManyAsync(e, true));
    }
    
    // Doctor
    private async Task SeedDoctorsAsync(List<Guid> departmentsId,Hospital hospital,List<Guid> titleId)
    {
        
        var faker = new Faker<Doctor>("tr")
            .CustomInstantiator(
                f =>
                     new Doctor(
                        guidGenerator.Create(),
                        f.Name.FirstName(),
                        f.Name.LastName(),
                        string.Empty,
                        f.PickRandom(titleId),
                        f.PickRandom(departmentsId),
                        hospital.Id
                     )
                
            );
        await SeedEntitiesAsync(faker.Generate(100), e => doctorRepository.InsertManyAsync(e, true));
    }

    // Appointment Type
    private async Task SeedAppointmentTypesAsync()
    {
        List<AppointmentType> appointmentTypes =
        [
            new AppointmentType(guidGenerator.Create(),"Kardiyoloji"),
            new AppointmentType(guidGenerator.Create(),"Muayene"),
            new AppointmentType(guidGenerator.Create(),"Chech-up"),
            new AppointmentType(guidGenerator.Create(),"Radyoloji"),
            new AppointmentType(guidGenerator.Create(),"Aşı")
        ];
       
        await SeedEntitiesAsync(appointmentTypes, e => appointmentTypeRepository.InsertManyAsync(e, true));
    }
    
    // Generic Entities
    private async Task<List<Guid>> SeedEntitiesAsync<T>(
        List<T> entities,
        Func<List<T>, Task> insertFunction
    )
        where T : AggregateRoot<Guid>
    {
        await insertFunction(entities);
        return entities.Select(e => e.Id).ToList();
    }
}