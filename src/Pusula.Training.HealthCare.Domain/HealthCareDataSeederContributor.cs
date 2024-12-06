using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus; 
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
using Pusula.Training.HealthCare.RadiologyExaminationGroups; 
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.PatientTypes;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Address = Pusula.Training.HealthCare.Addresses.Address;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;

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
    IProtocolTypeRepository protocolTypeRepository,
    IRadiologyExaminationGroupRepository radiologyExaminationGroupRepository,
    IRadiologyExaminationRepository radiologyExaminationRepository,
    IProtocolRepository protocolRepository,
    IRadiologyRequestRepository radiologyRequestRepository,
    IRadiologyRequestItemRepository radiologyRequestItemRepository,
    IGuidGenerator guidGenerator
)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        if (!await patientRepository.AnyAsync())
        {
         
        }

        if (!await protocolRepository.AnyAsync())
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
            var departments = await SeedDepartmentsAsync();
            var hospital = await SeedHospitalsAsync();
            var titles = await SeedTitlesAsync();
            var doctors = await SeedDoctorsAsync(departments, hospital, titles);
            var protocolTypes = await SeedProtocolTypesAsync();

            var radiologyExaminationIds = await radiologyExaminationRepository.GetListAsync();

            var protocols = await SeedProtocolsAsync(patients, doctors, departments, protocolTypes);

            var radiologyRequestIds = await SeedRadiologyRequestsAsync(protocols.Select(e => e.Id).ToList(),
                                              departments,
                                              doctors);

            await SeedRadiologyRequestItemsAsync(radiologyRequestIds,
                                                  radiologyExaminationIds.Select(e => e.Id).ToList());
        }

        if (await radiologyExaminationGroupRepository.GetCountAsync() == 0)
        {
            var radiologyExaminationGroups = await SeedRadiologyExaminationGroupsAsync();
            await SeedRadiologyExaminationsAsync(radiologyExaminationGroups);
        }

        if (!await doctorRepository.AnyAsync())
        {
            var departments = await SeedDepartmentsAsync();
            var hospital = await SeedHospitalsAsync();
            var titles = await SeedTitlesAsync();
            await SeedDoctorsAsync(departments, hospital, titles);
        }

        if (!await appointmentTypeRepository.AnyAsync())
        {
            await SeedAppointmentTypesAsync();
        }

        if (!await protocolTypeRepository.AnyAsync())
        {
            await SeedProtocolTypesAsync();
        }
         
    }

    //Protocol 
    private async Task<List<Protocol>> SeedProtocolsAsync
        (
            List<Guid> patientIds,
            List<Guid> doctorIds,
            List<Guid> departmentIds,
            List<Guid> protocolTypesIds
        )
    { 

        var faker = new Faker<Protocol>("tr")
            .CustomInstantiator(f =>
            {
                var patient = f.PickRandom(patientIds);
                var doctor = f.PickRandom(doctorIds);
                var department = f.PickRandom(departmentIds);
                var protocolType = f.PickRandom(protocolTypesIds);

                return new Protocol(
                    guidGenerator.Create(),
                    patient,
                    doctor,
                    department,
                    protocolType,
                    f.Lorem.Sentence(),
                    f.Random.Enum<EnumProtocolStatus>(),
                f.Date.Past(1),
                    f.Date.Past(0)
                );
            });

        var protocols = faker.Generate(100);
         
        await protocolRepository.InsertManyAsync(protocols, true);
        return protocols;
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

    private async Task<List<Guid>> SeedDepartmentsAsync()
    {
        List<Department> departments =
        [
            new Department(guidGenerator.Create(), "Acil Servis", "Acil durumlar için hızlı müdahale ve tedavi hizmeti.", 15),
            new Department(guidGenerator.Create(), "Kardiyoloji", "Kalp ve damar hastalıklarının tanı ve tedavisi.", 30),
            new Department(guidGenerator.Create(), "Nöroloji", "Sinir sistemi hastalıklarının tanı ve tedavisi.", 30),
            new Department(guidGenerator.Create(), "Ortopedi", "Kas ve iskelet sistemi hastalıklarının tedavisi.", 20),
            new Department(guidGenerator.Create(), "Dahiliye", "İç hastalıklarının teşhis ve tedavisi.", 30),
            new Department(guidGenerator.Create(), "Pediatri", "Çocuk sağlığı ve hastalıklarının teşhis ve tedavisi.", 20),
            new Department(guidGenerator.Create(), "Göz Hastalıkları", "Gözle ilgili hastalıkların tanı ve tedavisi.", 15),
            new Department(guidGenerator.Create(), "Kulak Burun Boğaz (KBB)", "Kulak, burun ve boğaz hastalıklarının tedavisi.", 15),
            new Department(guidGenerator.Create(), "Kadın Hastalıkları ve Doğum", "Kadın sağlığı ve doğumla ilgili hastalıkların tanı ve tedavisi.", 30),
            new Department(guidGenerator.Create(), "Üroloji", "İdrar yolları ve üreme organlarının hastalıklarının tedavisi.", 30),
            new Department(guidGenerator.Create(), "Dermatoloji", "Cilt hastalıklarının tanı ve tedavisi.", 15),
            new Department(guidGenerator.Create(), "Onkoloji", "Kanser hastalıklarının teşhis ve tedavisi.", 30),
            new Department(guidGenerator.Create(), "Psikiyatri", "Ruh sağlığı ve psikolojik hastalıkların tedavisi.", 45),
            new Department(guidGenerator.Create(), "Fizik Tedavi ve Rehabilitasyon", "Fiziksel hareket kabiliyetini artırmaya yönelik tedavi.", 60),
            new Department(guidGenerator.Create(), "Endokrinoloji ve Metabolizma Hastalıkları", "Hormon ve metabolizma hastalıklarının tanı ve tedavisi.", 30)
        ];
        return await SeedEntitiesAsync(departments, e => departmentRepository.InsertManyAsync(e, true));

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
    private async Task<List<Guid>> SeedDoctorsAsync(List<Guid> departmentsId, Hospital hospital, List<Guid> titleId)
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
        return await SeedEntitiesAsync(faker.Generate(100), e => doctorRepository.InsertManyAsync(e, true));
    }

    // Appointment Type
    private async Task SeedAppointmentTypesAsync()
    {
        List<AppointmentType> appointmentTypes =
        [
            new AppointmentType(guidGenerator.Create(),"Genel Muayene"),
            new AppointmentType(guidGenerator.Create(),"Uzman Görüşü"),
            new AppointmentType(guidGenerator.Create(),"Acil Durum"),
            new AppointmentType(guidGenerator.Create(),"Takip Randevusu"),
            new AppointmentType(guidGenerator.Create(),"Laboratuvar Testi"),
            new AppointmentType(guidGenerator.Create(),"Rutin Kontrol"),
            new AppointmentType(guidGenerator.Create(),"Psikolojik Danışmanlık"),
            new AppointmentType(guidGenerator.Create(),"Psikolojik Danışmanlık"),
            new AppointmentType(guidGenerator.Create(),"Fizik Tedavi"),
            new AppointmentType(guidGenerator.Create(),"Aşı Randevusu"),
            new AppointmentType(guidGenerator.Create(),"Göz Muayenesi")
        ];

        await SeedEntitiesAsync(appointmentTypes, e => appointmentTypeRepository.InsertManyAsync(e, true));
    }

    #region Radiology Seeders
    private async Task<List<Guid>> SeedRadiologyExaminationGroupsAsync()
    {
        IEnumerable<RadiologyExaminationGroup> groups =
        [
            new(guidGenerator.Create(), "Radyoloji Genel", "Genel radyoloji işlemleri"),
        new(guidGenerator.Create(), "Manyetik Rezonans (MR)", "Manyetik rezonans görüntüleme işlemleri"),
        new(guidGenerator.Create(), "Doppler Ultrasonografi", "Kan akışı ve damar incelemeleri")
        ];

        return await SeedEntitiesAsync(groups, e => radiologyExaminationGroupRepository.InsertManyAsync(e, true));
    }


    private async Task SeedRadiologyExaminationsAsync(IEnumerable<Guid> radiologyExaminationGroups)
    {
        IEnumerable<RadiologyExamination> examinations =
        [
            new(guidGenerator.Create(), "Akciğer Röntgeni", "72081", radiologyExaminationGroups.ElementAt(0)),
        new(guidGenerator.Create(), "Beyin MR", "70555", radiologyExaminationGroups.ElementAt(1)),
        new(guidGenerator.Create(), "Karotis Doppler Ultrason", "70552", radiologyExaminationGroups.ElementAt(2))
        ];

        await SeedEntitiesAsync(examinations, e => radiologyExaminationRepository.InsertManyAsync(e, true));
    }

    private async Task<List<Guid>> SeedRadiologyRequestsAsync(List<Guid> protocolIds, List<Guid> departmentIds, List<Guid> doctorIds)
    {
        var faker = new Faker<RadiologyRequest>("tr")
            .CustomInstantiator(
                f =>
                    new RadiologyRequest(
                        guidGenerator.Create(),
                        f.Date.Recent(30),
                        f.PickRandom(protocolIds), 
                        f.PickRandom(departmentIds),
                        f.PickRandom(doctorIds)
                    )
            );
         
        var radiologyRequests = faker.Generate(50);

        return await SeedEntitiesAsync(radiologyRequests, e => radiologyRequestRepository.InsertManyAsync(e, true));
    }

    private async Task SeedRadiologyRequestItemsAsync(List<Guid> radiologyRequestIds, List<Guid> examinationIds)
    {
        var faker = new Faker<RadiologyRequestItem>("tr")
            .CustomInstantiator(
                f =>
                    new RadiologyRequestItem(
                        guidGenerator.Create(),
                        f.PickRandom(radiologyRequestIds),
                        f.PickRandom(examinationIds),
                        f.Lorem.Sentence(),
                        f.Date.Recent(10),
                        f.Random.Enum<RadiologyRequestItemState>()
                    )
            );
         
        var radiologyRequestItems = faker.Generate(100);

        await SeedEntitiesAsync(radiologyRequestItems, e => radiologyRequestItemRepository.InsertManyAsync(e, true));
    }


    #endregion


    // Protocol types
    private async Task<List<Guid>> SeedProtocolTypesAsync()
    {
        List<ProtocolType> protocolTypes =
        [
            new(guidGenerator.Create(), "Test"),
            new(guidGenerator.Create(), "Test2"),
            new(guidGenerator.Create(), "Test3"),
            new(guidGenerator.Create(), "Test4"),
            new(guidGenerator.Create(), "Test5")
        ];
        return await SeedEntitiesAsync(protocolTypes, e => protocolTypeRepository.InsertManyAsync(e, true));
    }

    // Generic Entities
    private async Task<List<Guid>> SeedEntitiesAsync<T>(IEnumerable<T> entities,
                                                    Func<IEnumerable<T>, Task> insertFunction)
    where T : AggregateRoot<Guid>
    {
        await insertFunction(entities);
        return entities.Select(e => e.Id).ToList();
    }
}