using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
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
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypeActions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Address = Pusula.Training.HealthCare.Addresses.Address;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Pusula.Training.HealthCare.Allergies;
using Pusula.Training.HealthCare.BloodTransfusions;
using Pusula.Training.HealthCare.Diagnoses;
using Bogus.DataSets;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.Operations;
using Pusula.Training.HealthCare.Vaccines;
using Pusula.Training.HealthCare.Jobs;
using Pusula.Training.HealthCare.Educations;

namespace Pusula.Training.HealthCare;

public class HealthCareDataSeederContributor(
    IGuidGenerator guidGenerator,
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
    IProtocolTypeActionRepository protocolTypeActionRepository,
    IRadiologyRequestRepository radiologyRequestRepository,
    IRadiologyRequestItemRepository radiologyRequestItemRepository,
    IAllergyRepository allergyRepository,
    IBloodTransfusionRepository bloodTransfusionRepository,
    IDiagnosisRepository diagnosisRepository,
    IInsuranceRepository insuranceRepository,
    IMedicineRepository medicineRepository,
    IOperationRepository operationRepository,
    IVaccineRepository vaccineRepository,
    IJobRepository jobRepository,
    IEducationRepository educationRepository,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager
)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedUserAsync("Selçuk", "Şahin", "prm_selcuk", "selcuk@gmail.com", "1q2w3E*", "prm");
        await SeedUserAsync("Berfin", "Tek", "prm_berfin", "berfin@gmail.com", "1q2w3E*", "prm");
        await SeedUserAsync("Yusuf", "Altunsoy", "prm_yusuf", "yusuf@gmail.com", "1q2w3E*", "prm");
        await SeedUserAsync("Zeynep", "Salihoğlu", "zeynep", "zeynep@gmail.com", "1q2w3E*", "technician");
        await SeedUserAsync("Eslem", "Karabaki", "prm_eslem", "eslem@gmail.com", "1q2w3E*", "prm");

        var countries = await SeedCountriesAsync();
        var cityIds = await SeedCitiesAsync(countries);
        var districtIds = await SeedDistrictsAsync(cityIds);
        var patientTypeIds = await SeedPatientTypesAsync();
        var patientIds = await SeedPatientsAsync(countries, patientTypeIds);
        await SeedAddressesAsync(patientIds, districtIds);

        if (!await appDefaultRepository.AnyAsync())
        {
            await appDefaultRepository.InsertAsync(
                new AppDefault(guidGenerator.Create())
                {
                    CurrentCountryId = countries.FirstOrDefault(e => e.IsCurrent)?.Id ?? Guid.Empty
                }
            );
        }
        var departments = await SeedDepartmentsAsync();
        var hospital = await SeedHospitalsAsync();
        var titles = await SeedTitlesAsync();
        var allergies = await SeedAllergiesAsync();
        var bloodTransfusions = await SeedBloodTransfusionsAsync();
        var diagnoses = await SeedDiagnosesAsync();
        var insurances = await SeedInsurancesAsync();
        var medicines = await SeedMedicinesAsync();
        var operations = await SeedOperationsAsync();
        var vaccines = await SeedVaccinesAsync();
        var jobs = await SeedJobsAsync();
        var educations = await SeedEducationsAsync();

        var doctors = await SeedDoctorsAsync(departments, hospital, titles);

        await SeedAppointmentTypesAsync();
        var protocolTypeIds = await SeedProtocolTypesAsync();
        var protocolTypeActions = await SeedProtocolTypeActionsAsync(protocolTypeIds);
        var protocol = await SeedProtocolsAsync(
            patientIds, doctors, departments, protocolTypeIds, protocolTypeActions
        );

        if (!await radiologyExaminationRepository.AnyAsync())
        {
            var radiologyExaminationGroups = await SeedRadiologyExaminationGroupsAsync();
            var radiologyExaminations = await SeedRadiologyExaminationsAsync(radiologyExaminationGroups);

       

            var radiologyRequests = await SeedRadiologyRequestsAsync(
                protocol.Select(d => d.Id).ToList(), protocol.Select(d => d.Department.Id).ToList(), protocol.Select(d => d.DoctorId).ToList()
            );

            await SeedRadiologyRequestItemsAsync(radiologyRequests, radiologyExaminations);
        }

    }

    // Educations
    private async Task<IEnumerable<Education>> SeedEducationsAsync()
    {
        if (await educationRepository.AnyAsync())
        {
            return await educationRepository.GetListAsync();
        }
        IEnumerable<Education> educations = [
            new(guidGenerator.Create(), "Tıp Fakültesi"),
            new(guidGenerator.Create(), "Hemşirelik"),
            new(guidGenerator.Create(), "Eczacılık"),
            new(guidGenerator.Create(), "Sağlık Memurluğu"),
            new(guidGenerator.Create(), "Fizyoterapi"),
            new(guidGenerator.Create(), "Psikoloji"),
            new(guidGenerator.Create(), "Diş Hekimliği"),
            new(guidGenerator.Create(), "Radyoloji"),
            new(guidGenerator.Create(), "Laborant")
            ];
        await educationRepository.InsertManyAsync(educations, true);
        return educations;
    }

    // Jobs
    private async Task<IEnumerable<Job>> SeedJobsAsync()
    {
        if (await jobRepository.AnyAsync())
        {
            return await jobRepository.GetListAsync();
        }
        IEnumerable<Job> jobs = [
            new(guidGenerator.Create(), "Doktor"),
            new(guidGenerator.Create(), "Hemşire"),
            new(guidGenerator.Create(), "Eczacı"),
            new(guidGenerator.Create(), "Sağlık Memuru"),
            new(guidGenerator.Create(), "Fizyoterapist"),
            new(guidGenerator.Create(), "Psikolog"),
            new(guidGenerator.Create(), "Diş Hekimi"),
            new(guidGenerator.Create(), "Radyolog"),
            new(guidGenerator.Create(), "Laborant"),
            new(guidGenerator.Create(), "Acil Tıp Teknisyeni")
            ];
        await jobRepository.InsertManyAsync(jobs, true);
        return jobs;
    }

    // Vaccines
    private async Task<IEnumerable<Vaccine>> SeedVaccinesAsync()
    {
        if (await vaccineRepository.AnyAsync())
        {
            return await vaccineRepository.GetListAsync();
        }
        IEnumerable<Vaccine> vaccines = [
            new(guidGenerator.Create(), "Bcg Aşısı"),
            new(guidGenerator.Create(), "Hepatit B Aşısı"),
            new(guidGenerator.Create(), "Difteri Aşısı"),
            new(guidGenerator.Create(), "Tetanoz Aşısı"),
            new(guidGenerator.Create(), "Kızamık Aşısı"),
            new(guidGenerator.Create(), "Kabakulak Aşısı"),
            new(guidGenerator.Create(), "Suçiçeği Aşısı"),
            new(guidGenerator.Create(), "Hepatit A Aşısı"),
            new(guidGenerator.Create(), "Hepatit B Aşısı"),
            new(guidGenerator.Create(), "Grip Aşısı")
            ];
        await vaccineRepository.InsertManyAsync(vaccines, true);
        return vaccines;
    }

    // Operations
    private async Task<IEnumerable<Operation>> SeedOperationsAsync()
    {
        if (await operationRepository.AnyAsync())
        {
            return await operationRepository.GetListAsync();
        }
        IEnumerable<Operation> operations = [
            new(guidGenerator.Create(), "Apandisit Ameliyatı"),
            new(guidGenerator.Create(), "Katarakt Ameliyatı"),
            new(guidGenerator.Create(), "Varis Ameliyatı"),
            new(guidGenerator.Create(), "Kolonoskopi"),
            new(guidGenerator.Create(), "Laparoskopik Safra Kesesi Ameliyatı"),
            new(guidGenerator.Create(), "Kemik İliği Nakli"),
            new(guidGenerator.Create(), "Kardiyak Kateterizasyon"),
            new(guidGenerator.Create(), "Kemik Kanseri Ameliyatı")
            ];
        await operationRepository.InsertManyAsync(operations, true);
        return operations;
    }

    // Medicines
    private async Task<IEnumerable<Medicine>> SeedMedicinesAsync()
    {
        if (await medicineRepository.AnyAsync())
        {
            return await medicineRepository.GetListAsync();
        }
        IEnumerable<Medicine> medicines = [
            new(guidGenerator.Create(), "Parol"),
            new(guidGenerator.Create(), "Aspirin"),
            new(guidGenerator.Create(), "Nurofen"),
            new(guidGenerator.Create(), "Voltaren"),
            new(guidGenerator.Create(), "Panadol"),
            new(guidGenerator.Create(), "Majezik"),
            new(guidGenerator.Create(), "Novalgin"),
            new(guidGenerator.Create(), "Dolorex")
            ];
        await medicineRepository.InsertManyAsync(medicines, true);
        return medicines;
    }

    // Insurances
    private async Task<IEnumerable<Insurance>> SeedInsurancesAsync()
    {
        if (await insuranceRepository.AnyAsync())
        {
            return await insuranceRepository.GetListAsync();
        }
        IEnumerable<Insurance> insurances =
        [
            new(guidGenerator.Create(), "Anadolu Sigorta"),
            new(guidGenerator.Create(), "Axa Sigorta"),
            new(guidGenerator.Create(), "Allianz Sigorta"),
            new(guidGenerator.Create(), "Ergo Sigorta"),
            new(guidGenerator.Create(), "Sompo Sigorta"),
            new(guidGenerator.Create(), "Zurich Sigorta")
        ];
        await insuranceRepository.InsertManyAsync(insurances, true);
        return insurances;
    }
    // Diagnosis
    private async Task<IEnumerable<Diagnosis>> SeedDiagnosesAsync()
    {
        if (await diagnosisRepository.AnyAsync())
        {
            return await diagnosisRepository.GetListAsync();
        }
        IEnumerable<Diagnosis> diagnosis = [
            new(guidGenerator.Create(), "D001", "Migren"),
            new(guidGenerator.Create(), "D002", "Baş Ağrısı"),
            new(guidGenerator.Create(), "D003", "Grip"),
            new(guidGenerator.Create(), "D004", "Soğuk Algınlığı"),
            new(guidGenerator.Create(), "D005", "Kanser"),
            new(guidGenerator.Create(), "D006", "Kalp Hastalıkları"),
            new(guidGenerator.Create(), "D007", "Diyabet"),
            new(guidGenerator.Create(), "D008", "Hipertansiyon"),
            new(guidGenerator.Create(), "D009", "Astım"),
            new(guidGenerator.Create(), "D010", "Bronşit"),
            ];
        await diagnosisRepository.InsertManyAsync(diagnosis, true);
        return diagnosis;
    }

        // Allergy
        private async Task<IEnumerable<Allergy>> SeedAllergiesAsync()
    {
        if (await allergyRepository.AnyAsync())
        {
            return await allergyRepository.GetListAsync();
        }
        IEnumerable<Allergy> allergies =
        [
            new(guidGenerator.Create(), "Yiyecek Alerjisi"),
            new(guidGenerator.Create(), "İlaç Alerjisi"),
            new(guidGenerator.Create(), "Hayvan Alerjisi"),
            new(guidGenerator.Create(), "Polen Alerjisi"),
            new(guidGenerator.Create(), "Toz Alerjisi"),
            new(guidGenerator.Create(), "Mantar Alerjisi"),
            new(guidGenerator.Create(), "Güneş Alerjisi"),
            new(guidGenerator.Create(), "Soğan Alerjisi"),
            new(guidGenerator.Create(), "Balık Alerjisi"),
            new(guidGenerator.Create(), "Süt Alerjisi")

            ];
        await allergyRepository.InsertManyAsync(allergies, true);
        return allergies;
    }

    // BlodTransfusion
    private async Task<IEnumerable<Guid>> SeedBloodTransfusionsAsync()
    {
        if (await bloodTransfusionRepository.AnyAsync())
        {
            return (await bloodTransfusionRepository.GetListAsync()).Select(e => e.Id);
        }

        IEnumerable<BloodTransfusion> bloodTransfusions =
        [
            new(guidGenerator.Create(), "A Grubu Kan Nakli"),
            new(guidGenerator.Create(), "B Grubu Kan Nakli"),
            new(guidGenerator.Create(), "AB Grubu Kan Nakli"),
            new(guidGenerator.Create(), "0 Grubu Kan Nakli"),
            new(guidGenerator.Create(), "Rh+ Kan Nakli"),
        ];

        return await SeedEntitiesAsync(bloodTransfusions, e => bloodTransfusionRepository.InsertManyAsync(e, true));
    }

    // Country
    private async Task<IEnumerable<Country>> SeedCountriesAsync()
    {
        if (await countryRepository.AnyAsync())
        {
            return await countryRepository.GetListAsync();
        }

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
        if (await cityRepository.AnyAsync())
        {
            return (await cityRepository.GetListAsync()).Select(e => e.Id);
        }

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
    private async Task<IEnumerable<Guid>> SeedDistrictsAsync(IEnumerable<Guid> cityIds)
    {
        if (await districtRepository.AnyAsync())
        {
            return (await districtRepository.GetListAsync()).Select(e => e.Id);
        }

        IEnumerable<District> districts =
        [
            new(guidGenerator.Create(), cityIds.ElementAt(0), "Ümraniye"),
            new(guidGenerator.Create(), cityIds.ElementAt(0), "Maltepe"),
            new(guidGenerator.Create(), cityIds.ElementAt(1), "Çankaya"),
            new(guidGenerator.Create(), cityIds.ElementAt(1), "Etimesgut"),
            new(guidGenerator.Create(), cityIds.ElementAt(2), "Westminster"),
            new(guidGenerator.Create(), cityIds.ElementAt(3), "Charlottenburg-Wilmersdorf"),
            new(guidGenerator.Create(), cityIds.ElementAt(4), "Louvre"),
            new(guidGenerator.Create(), cityIds.ElementAt(5), "El-Midan")
        ];

        return await SeedEntitiesAsync(districts, e => districtRepository.InsertManyAsync(e, true));
    }

    // Patient types
    private async Task<IEnumerable<Guid>> SeedPatientTypesAsync()
    {
        if (await patientTypeRepository.AnyAsync())
        {
            return (await patientTypeRepository.GetListAsync()).Select(e => e.Id);
        }

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
        IEnumerable<Guid> patientTypeIds
    )
    {
        if (await patientRepository.AnyAsync())
        {
            return (await patientRepository.GetListAsync(maxResultCount: 100)).Select(e => e.Id);
        }

        var faker = new Faker<Patient>("tr")
            .CustomInstantiator(
                f =>
                {
                    var country = f.PickRandom(countries);
                    return new Patient(
                        guidGenerator.Create(),
                        country.Id,
                        f.PickRandom(patientTypeIds),
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
    private async Task SeedAddressesAsync(IEnumerable<Guid> patientIds, IEnumerable<Guid> districtIds)
    {
        var faker = new Faker<Address>("tr")
            .CustomInstantiator(
                f =>
                    new Address(
                        guidGenerator.Create(),
                        f.PickRandom(patientIds),
                        f.PickRandom(districtIds),
                        f.Lorem.Word(),
                        f.Address.SecondaryAddress()
                    )
            );

        await addressRepository.InsertManyAsync(faker.Generate(200), true);
    }

    // Department
    private async Task<IEnumerable<Guid>> SeedDepartmentsAsync()
    {
        if (await departmentRepository.AnyAsync())
        {
            return (await departmentRepository.GetListAsync()).Select(e => e.Id);
        }

        IEnumerable<Department> departments =
        [
            new(guidGenerator.Create(), "Kardiyoloji", "Kalp ve damar hastalıklarının tanı ve tedavisi.", 30),
            new(guidGenerator.Create(), "Nöroloji", "Sinir sistemi hastalıklarının tanı ve tedavisi.", 30),
            new(guidGenerator.Create(), "Ortopedi", "Kas ve iskelet sistemi hastalıklarının tedavisi.", 20),
            new(guidGenerator.Create(), "Dahiliye", "İç hastalıklarının teşhis ve tedavisi.", 30),
            new(guidGenerator.Create(), "Pediatri", "Çocuk sağlığı ve hastalıklarının teşhis ve tedavisi.", 20),
            new(guidGenerator.Create(), "Göz Hastalıkları", "Gözle ilgili hastalıkların tanı ve tedavisi.", 15),
            new(guidGenerator.Create(), "Kulak Burun Boğaz (KBB)", "Kulak, burun ve boğaz hastalıklarının tedavisi.", 15),
            new(guidGenerator.Create(), "Kadın Hastalıkları ve Doğum", "Kadın sağlığı ve doğumla ilgili hastalıkların tanı ve tedavisi.", 30),
            new(guidGenerator.Create(), "Üroloji", "İdrar yolları ve üreme organlarının hastalıklarının tedavisi.", 30),
            new(guidGenerator.Create(), "Dermatoloji", "Cilt hastalıklarının tanı ve tedavisi.", 15),
            new(guidGenerator.Create(), "Onkoloji", "Kanser hastalıklarının teşhis ve tedavisi.", 30),
            new(guidGenerator.Create(), "Psikiyatri", "Ruh sağlığı ve psikolojik hastalıkların tedavisi.", 45),
            new(guidGenerator.Create(), "Fizik Tedavi ve Rehabilitasyon", "Fiziksel hareket kabiliyetini artırmaya yönelik tedavi.", 60),
            new(guidGenerator.Create(), "Endokrinoloji ve Metabolizma Hastalıkları", "Hormon ve metabolizma hastalıklarının tanı ve tedavisi.", 30)
        ];
        return await SeedEntitiesAsync(departments, e => departmentRepository.InsertManyAsync(e, true));
    }

    // Hospital
    private async Task<Hospital> SeedHospitalsAsync()
    {
        if (await hospitalRepository.AnyAsync())
        {
            return await hospitalRepository.FirstAsync();
        }

        var hospital = new Hospital(
            guidGenerator.Create(),
            "Medical Park",
            "İstanbul Ümraniye"
        );

        return await hospitalRepository.InsertAsync(hospital, true);
    }

    // Title
    private async Task<IEnumerable<Guid>> SeedTitlesAsync()
    {
        if (await titleRepository.AnyAsync())
        {
            return (await titleRepository.GetListAsync()).Select(e => e.Id);
        }

        IEnumerable<Title> titles =
        [
            new(guidGenerator.Create(), "Prof."),
            new(guidGenerator.Create(), "Dr.")
        ];

        return await SeedEntitiesAsync(titles, e => titleRepository.InsertManyAsync(e, true));
    }

    // Doctor
    private async Task<IEnumerable<Doctor>> SeedDoctorsAsync(
        IEnumerable<Guid> departmentsIds,
        Hospital hospital,
        IEnumerable<Guid> titleIds
    )
    {
        if (await doctorRepository.AnyAsync())
        {
            return await doctorRepository.GetListAsync();
        }

        var faker = new Faker();
        ICollection<Doctor> doctors = [];

        doctors.Add(
            new Doctor(
                guidGenerator.Create(), "Selçuk", "Şahin", 30, faker.PickRandom(titleIds),
                faker.PickRandom(departmentsIds), hospital.Id
            )
        );
        foreach (var departmentId in departmentsIds)
        {
            doctors.Add(
                new Doctor(
                    guidGenerator.Create(), faker.Name.FirstName(), faker.Name.LastName(),
                    faker.Random.Int(10, 30), faker.PickRandom(titleIds), departmentId, hospital.Id
                )
            );
            doctors.Add(
                new Doctor(
                    guidGenerator.Create(), faker.Name.FirstName(), faker.Name.LastName(),
                    faker.Random.Int(10, 30), faker.PickRandom(titleIds), departmentId, hospital.Id
                )
            );
            doctors.Add(
                new Doctor(
                    guidGenerator.Create(), faker.Name.FirstName(), faker.Name.LastName(),
                    faker.Random.Int(10, 30), faker.PickRandom(titleIds), departmentId, hospital.Id
                )
            );
        }

        foreach (var doctor in doctors)
        {
            var user = await SeedUserAsync(
                doctor.FirstName, doctor.LastName,
                faker.Internet.UserName(doctor.FirstName, doctor.LastName),
                faker.Internet.Email(doctor.FirstName, doctor.LastName), "1q2w3E*", "doctor"
            );
            doctor.SetUserId(user.Id);
        }

        await SeedEntitiesAsync(doctors, e => doctorRepository.InsertManyAsync(e, true));
        return doctors;
    }

    // Appointment Type
    private async Task SeedAppointmentTypesAsync()
    {
        if (await appointmentTypeRepository.AnyAsync())
        {
            return;
        }

        IEnumerable<AppointmentType> appointmentTypes =
        [
            new(guidGenerator.Create(), "Genel Muayene"),
            new(guidGenerator.Create(), "Uzman Görüşü"),
            new(guidGenerator.Create(), "Takip Randevusu"),
            new(guidGenerator.Create(), "Laboratuvar Testi"),
            new(guidGenerator.Create(), "Rutin Kontrol"),
            new(guidGenerator.Create(), "Psikolojik Danışmanlık"),
            new(guidGenerator.Create(), "Psikolojik Danışmanlık"),
            new(guidGenerator.Create(), "Fizik Tedavi"),
            new(guidGenerator.Create(), "Aşı Randevusu"),
            new(guidGenerator.Create(), "Göz Muayenesi")
        ];

        await SeedEntitiesAsync(appointmentTypes, e => appointmentTypeRepository.InsertManyAsync(e, true));
    }

    #region Radiology

    private async Task<IEnumerable<Guid>> SeedRadiologyExaminationGroupsAsync()
    {
        IEnumerable<RadiologyExaminationGroup> groups =
        [
            new(guidGenerator.Create(), "Radyoloji Genel", "Genel radyoloji işlemleri"),
            new(guidGenerator.Create(), "Manyetik Rezonans (MR)", "Manyetik rezonans görüntüleme işlemleri"),
            new(guidGenerator.Create(), "Doppler Ultrasonografi", "Kan akışı ve damar incelemeleri")
        ];

        return await SeedEntitiesAsync(groups, e => radiologyExaminationGroupRepository.InsertManyAsync(e, true));
    }

    private async Task<IEnumerable<Guid>> SeedRadiologyExaminationsAsync(IEnumerable<Guid> radiologyExaminationGroups)
    {
        IEnumerable<RadiologyExamination> examinations =
        [
            new(guidGenerator.Create(), "Akciğer Röntgeni", "72081", radiologyExaminationGroups.ElementAt(0)),
            new(guidGenerator.Create(), "Beyin MR", "70555", radiologyExaminationGroups.ElementAt(1)),
            new(guidGenerator.Create(), "Karotis Doppler Ultrason", "70552", radiologyExaminationGroups.ElementAt(2))
        ];

        return await SeedEntitiesAsync(examinations, e => radiologyExaminationRepository.InsertManyAsync(e, true));
    }

    private async Task<List<Guid>> SeedRadiologyRequestsAsync(
        IEnumerable<Guid> protocolIds,
        IEnumerable<Guid> departmentIds,
        IEnumerable<Guid> doctorIds
    )
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

        await SeedEntitiesAsync(radiologyRequests, e => radiologyRequestRepository.InsertManyAsync(e, true));

        return radiologyRequests.Select(r => r.Id).ToList();
    }

    private async Task SeedRadiologyRequestItemsAsync(
     IEnumerable<Guid> radiologyRequestIds,
     IEnumerable<Guid> examinationIds
 )
    {
        var radiologyRequestIdsList = radiologyRequestIds.ToList();
        var examinationIdsList = examinationIds.ToList();
        var radiologyRequestItems = new List<RadiologyRequestItem>();

        for (int i = 0; i < radiologyRequestIdsList.Count; i++)
        {
            var faker = new Faker<RadiologyRequestItem>("tr")
                .CustomInstantiator(
                    f =>
                        new RadiologyRequestItem(
                            guidGenerator.Create(),
                            radiologyRequestIdsList[i],
                            f.PickRandom(examinationIdsList),
                            f.Lorem.Sentence(),
                            f.Date.Recent(10),
                            f.Random.Enum<RadiologyRequestItemState>()
                        )
                );

            radiologyRequestItems.AddRange(faker.Generate(1));
        }

        await SeedEntitiesAsync(radiologyRequestItems, e => radiologyRequestItemRepository.InsertManyAsync(e, true));
    }

    #endregion

    // Protocol types
    private async Task<IEnumerable<Guid>> SeedProtocolTypesAsync()
    {
        if (await protocolRepository.AnyAsync())
        {
            return (await protocolRepository.GetListAsync()).Select(e => e.Id);
        }

        IEnumerable<ProtocolType> protocolTypes =
        [
            new(guidGenerator.Create(), "Konsültasyon"),
            new(guidGenerator.Create(), "Laboratuvar"),
            new(guidGenerator.Create(), "Radyoloji"),
            new(guidGenerator.Create(), "Cerrahi İşlem"),
            new(guidGenerator.Create(), "Diyaliz"),
            new(guidGenerator.Create(), "Psikiyatri"),
            new(guidGenerator.Create(), "Yoğun Bakım")
        ];
        return await SeedEntitiesAsync(protocolTypes, e => protocolTypeRepository.InsertManyAsync(e, true));
    }

    // Protocol type action
    private async Task<IEnumerable<ProtocolTypeAction>> SeedProtocolTypeActionsAsync(IEnumerable<Guid> protocolTypeIds)
    {
        if (await protocolTypeActionRepository.AnyAsync())
        {
            return await protocolTypeActionRepository.GetListAsync();
        }

        IEnumerable<ProtocolTypeAction> protocolTypeActions =
        [
            new(guidGenerator.Create(), "Ek Konsültasyon Talebi", protocolTypeIds.ElementAt(0)),
            new(guidGenerator.Create(), "PCR Testi Talebi", protocolTypeIds.ElementAt(1)),
            new(guidGenerator.Create(), "Kan Testi Talebi", protocolTypeIds.ElementAt(1)),
            new(guidGenerator.Create(), "İdrar Testi Talebi", protocolTypeIds.ElementAt(1)),
            new(guidGenerator.Create(), "MR Çekimi", protocolTypeIds.ElementAt(2)),
            new(guidGenerator.Create(), "Apendektomi", protocolTypeIds.ElementAt(3)),
            new(guidGenerator.Create(), "Hemodiyaliz Seansı", protocolTypeIds.ElementAt(4)),
            new(guidGenerator.Create(), "Psikoterapi Görüşmesi", protocolTypeIds.ElementAt(5)),
            new(guidGenerator.Create(), "Ventilatör Desteği", protocolTypeIds.ElementAt(6))
        ];
        await SeedEntitiesAsync(protocolTypeActions, e => protocolTypeActionRepository.InsertManyAsync(e, true));
        return protocolTypeActions;
    }

    private async Task<IEnumerable<Protocol>> SeedProtocolsAsync(
        IEnumerable<Guid> patientIds,
        IEnumerable<Doctor> doctors,
        IEnumerable<Guid> departmentIds,
        IEnumerable<Guid> protocolTypeIds,
        IEnumerable<ProtocolTypeAction> protocolTypeActions
    )
    {
        if (await protocolRepository.AnyAsync())
        {
            return (await protocolRepository.GetListAsync());
        }

        var faker = new Faker<Protocol>("tr")
            .CustomInstantiator(
                f =>
                {
                    var departmentId = f.PickRandom(departmentIds);
                    var protocolTypeId = f.PickRandom(protocolTypeIds);
                    var protocolStartDate = f.Date.Past(3);
                    var status = f.PickRandomWithout<EnumProtocolStatus>(EnumProtocolStatus.None);
                    return new Protocol(
                        guidGenerator.Create(),
                        f.PickRandom(patientIds),
                        f.PickRandom(doctors.Where(e => e.DepartmentId == departmentId)).Id,
                        departmentId,
                        protocolTypeId,
                        f.PickRandom(protocolTypeActions.Where(e => e.ProtocolTypeId == protocolTypeId)).Id,
                        null,
                        status,
                        protocolStartDate,
                        status == EnumProtocolStatus.Completed ?
                            f.Date.Between(protocolStartDate, protocolStartDate.AddMonths(1)) :
                            null
                    );
                }
            );

        var protocols = faker.Generate(500);
        await SeedEntitiesAsync(protocols, e => protocolRepository.InsertManyAsync(e, true));
        return protocols;
    }

    // User
    private async Task<IdentityUser> SeedUserAsync(
        string name,
        string surname,
        string userName,
        string email,
        string password,
        string? roleName = null
    )
    {
        await userManager.CreateAsync(
            new IdentityUser(guidGenerator.Create(), userName, email)
            {
                Name = name,
                Surname = surname
            }, password
        );
        var user = await userManager.FindByNameAsync(userName);
        if (!roleName.IsNullOrWhiteSpace())
        {
            var role = await SeedRoleAsync(roleName);
            await userManager.AddToRoleAsync(user!, role.Name);
        }

        return user!;
    }

    //Role
    private async Task<IdentityRole> SeedRoleAsync(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role != null) return role;

        await roleManager.CreateAsync(new IdentityRole(guidGenerator.Create(), roleName));
        return (await roleManager.FindByNameAsync(roleName))!;
    }

    // Generic Entities
    private async Task<IEnumerable<Guid>> SeedEntitiesAsync<T>(
        IEnumerable<T> entities,
        Func<IEnumerable<T>, Task> insertFunction
    )
        where T : AggregateRoot<Guid>
    {
        await insertFunction(entities);
        return entities.Select(e => e.Id).ToList();
    }
}