using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.AppointmentReports;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare;

public class HealthCareDataSeederContributor(
    ICityRepository cityRepository,
    ICountryRepository countryRepository,
    IDistrictRepository districtRepository,
    IAddressRepository addressRepository,
    IPatientRepository patientRepository,
    IAppointmentRepository appointmentRepository,
    IAppointmentTypeRepository appointmentTypeRepository,
    IAppointmentReportRepository appointmentReportRepository,
    IGuidGenerator guidGenerator)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await patientRepository.GetCountAsync() == 0)
        {
            var countries = await SeedCountriesAsync();
            var cities = await SeedCitiesAsync(countries);
            var districts = await SeedDistrictsAsync(cities);
            var patients = await SeedPatientsAsync(countries);            
            await SeedAddressesAsync(patients, districts);
        }

        //if (await appointmentRepository.GetCountAsync() == 0)
        //{
        //    var appointmentTypes = await SeedAppointmentTypesAsync(); 
        //    var departments = await SeedDepartmentsAsync();          
        //    var doctors = await SeedDoctorsAsync(departments);       
        //    var patients = await patientRepository.GetListAsync();   

        //    var appointments = await SeedAppointmentsAsync(appointmentTypes, departments, doctors, patients); 
        //    await SeedAppointmentReportAsync(appointments);         
        //}
    }


    // Country
    private async Task<IEnumerable<Guid>> SeedCountriesAsync()
    {
        IEnumerable<Country> countries =
        [
            new(guidGenerator.Create(), "Türkiye", "TR"),
            new(guidGenerator.Create(), "İngiltere", "UK"),
            new(guidGenerator.Create(), "Almanya", "GE"),
            new(guidGenerator.Create(), "Fransa", "FR"),
            new(guidGenerator.Create(), "Suriye", "SY")
        ];

        return await SeedEntitiesAsync(countries, e => countryRepository.InsertManyAsync(e, true));
    }

    // City
    private async Task<IEnumerable<Guid>> SeedCitiesAsync(IEnumerable<Guid> countries)
    {
        IEnumerable<City> cities =
        [
            new(guidGenerator.Create(), countries.ElementAt(0), "İstanbul"),
            new(guidGenerator.Create(), countries.ElementAt(0), "Ankara"),
            new(guidGenerator.Create(), countries.ElementAt(1), "Londra"),
            new(guidGenerator.Create(), countries.ElementAt(2), "Berlin"),
            new(guidGenerator.Create(), countries.ElementAt(3), "Paris"),
            new(guidGenerator.Create(), countries.ElementAt(4), "Şam")
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

    // Patient
    private async Task<IEnumerable<Guid>> SeedPatientsAsync(IEnumerable<Guid> countries)
    {
        IEnumerable<Patient> patients =
        [
            new(guidGenerator.Create(), countries.ElementAt(0), "Selçuk", "Şahin", new DateTime(1998, 5, 18),
                "12345678900", "muselcuksahin@gmail.com", "5555555555", EnumGender.Male, EnumBloodType.AbPositive,
                EnumMaritalStatus.Single),
            new(guidGenerator.Create(), countries.ElementAt(1), "Joel", "Bond", new DateTime(1991, 8, 7),
                "64279023471", "johndoe@gmail.com", "07836668374", EnumGender.Male, EnumBloodType.BPositive,
                EnumMaritalStatus.Married),
            new(guidGenerator.Create(), countries.ElementAt(2), "Kristin", "Saenger", new DateTime(1970, 8, 23),
                "44748015944", "kristinSaenger@dayrep.com", "0471266747", EnumGender.Male, EnumBloodType.ZeroPositive,
                EnumMaritalStatus.Single)
        ];

        return await SeedEntitiesAsync(patients, e => patientRepository.InsertManyAsync(e, true));
    }

    //// Appointment
    //private async Task<IEnumerable<Guid>> SeedAppointmentsAsync(IEnumerable<Guid> appointmentTypes, IEnumerable<Guid> departments, IEnumerable<Guid> doctors, IEnumerable<Guid> patients)
    //{
    //    IEnumerable<Appointment> appointments =
    //    [
    //        new(guidGenerator.Create(), appointmentTypes.ElementAt(0), departments.ElementAt(0), doctors.ElementAt(0), patients.ElementAt(0), new DateTime(2024, 11, 12, 09, 00, 00), new DateTime(2024, 11, 12, 09, 15, 00),
    //        "Lorem ipsum odor amet, consectetuer adipiscing elit.", EnumStatus.Completed),
    //        new(guidGenerator.Create(), appointmentTypes.ElementAt(0), departments.ElementAt(0), doctors.ElementAt(0), patients.ElementAt(0), new DateTime(2024, 11, 12, 14, 15, 00), new DateTime(2024, 11, 12, 14, 30, 00),
    //        "Parturient ipsum quam facilisis facilisi consectetur curabitur enim.", EnumStatus.Scheduled),
    //        new(guidGenerator.Create(), appointmentTypes.ElementAt(1), departments.ElementAt(0), doctors.ElementAt(0), patients.ElementAt(0), new DateTime(2024, 11, 12, 14, 30, 00),  new DateTime(2024, 11, 12, 14, 45, 00),
    //        "Suspendisse nascetur fusce molestie penatibus mi tempus fermentum dis.", EnumStatus.Rescheduled),


    //    ];

    //    return await SeedEntitiesAsync(appointments, e => appointmentRepository.InsertManyAsync(e, true));
    //}

    ////AppointmentType
    //private async Task<IEnumerable<Guid>> SeedAppointmentTypesAsync()
    //{
    //    IEnumerable<AppointmentType> appointmentTypes = [
    //        new(guidGenerator.Create(), "Medical"), //Genel tıbbi randevular (doktor ziyaretleri gibi).
    //        new(guidGenerator.Create(), "Consultation"), //Danışmanlık randevuları.
    //        new(guidGenerator.Create(), "Checkup"), //Düzenli kontrol randevuları.
    //        new(guidGenerator.Create(), "Emergency"), //Acil durum randevuları.
    //        new(guidGenerator.Create(), "FollowUp"), //Daha önceki bir tedavi veya muayeneyi takip eden randevular.
    //        new(guidGenerator.Create(), "Surgery"), //Ameliyat randevuları.
    //        new(guidGenerator.Create(), "Dental"), //Diş hekimliğiyle ilgili randevular.
    //        new(guidGenerator.Create(), "Physiotherapy"), //Fizyoterapi randevuları.
    //        new(guidGenerator.Create(), "Mental Health"), //Psikolojik veya psikiyatrik randevular.
    //        new(guidGenerator.Create(), "Vaccination"), // Aşı randevuları.
    //        new(guidGenerator.Create(), "Lab Test"), //Laboratuvar testleri için randevular.
    //        ];

    //    return await SeedEntitiesAsync(appointmentTypes, e=> appointmentTypeRepository.InsertManyAsync(e, true));
    //}

    ////AppointmentReport
    //private async Task<IEnumerable<Guid>> SeedAppointmentReportAsync(IEnumerable<Guid> appointments)
    //{
    //    IEnumerable<AppointmentReport> appointmentReports = [
    //        new(guidGenerator.Create(), appointments.ElementAt(0), new DateTime(2024, 11, 18), 
    //        "Lorem ipsum odor amet, consectetuer adipiscing elit.", "Parturient ipsum quam facilisis facilisi consectetur curabitur enim."),
    //        new(guidGenerator.Create(), appointments.ElementAt(0), new DateTime(2024, 11, 18),
    //        "Suspendisse nascetur fusce molestie penatibus mi tempus fermentum dis.", "Leo inceptos dapibus semper neque massa eleifend nam."),
    //        new(guidGenerator.Create(), appointments.ElementAt(1), new DateTime(2024, 11, 18),
    //        "Sem placerat eget fermentum leo ullamcorper aenean fames natoque. ", "Nisi nunc pretium metus a vestibulum hac."),
    //        ];

    //    return await SeedEntitiesAsync(appointmentReports, e => appointmentReportRepository.InsertManyAsync(e, true));
    //}

    // Address
    private async Task SeedAddressesAsync(IEnumerable<Guid> patients, IEnumerable<Guid> districts)
    {
        IEnumerable<Address> addresses =
        [
            new(guidGenerator.Create(), patients.ElementAt(0), districts.ElementAt(0), "Asya Sokak"),
            new(guidGenerator.Create(), patients.ElementAt(1), districts.ElementAt(4), "lorem"),
            new(guidGenerator.Create(), patients.ElementAt(2), districts.ElementAt(5), "ipsum")
        ];

        await addressRepository.InsertManyAsync(addresses, true);
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