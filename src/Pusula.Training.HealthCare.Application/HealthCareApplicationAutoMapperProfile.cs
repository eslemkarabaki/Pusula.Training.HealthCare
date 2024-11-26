using AutoMapper;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.HospitalDepartments;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Notifications;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using System;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.AppointmentReports;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.AppointmentReports;
using Pusula.Training.HealthCare.PatientTypes;

namespace Pusula.Training.HealthCare;

public class HealthCareApplicationAutoMapperProfile : Profile
{
    public HealthCareApplicationAutoMapperProfile()
    {
        CreateMap<Patient, PatientDto>();
        CreateMap<Patient, PatientUpdateDto>();
        CreateMap<PatientDto, PatientUpdateDto>();
        CreateMap<PatientWithNavigationProperties, PatientUpdateDto>()
            .IncludeMembers(e => e.Patient)
            .ForMember(e => e.Addresses, opt => opt.MapFrom(e => e.Addresses))
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<PatientWithNavigationPropertiesDto, PatientUpdateDto>()
            .IncludeMembers(e => e.Patient)
            .ForMember(e => e.Addresses, opt => opt.MapFrom(e => e.Addresses))
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>();

        CreateMap<Patient, PatientExcelDto>();
        CreateMap<PatientWithNavigationProperties, PatientExcelDto>()
            .IncludeMembers(e => e.Patient)
            .ForMember(e => e.Race, opt => opt.MapFrom(e => e.Country.Name))
            .ForMember(e => e.Type, opt => opt.MapFrom(e => e.PatientType.Name))
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<PatientType, PatientTypeDto>();

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<AddressCreateDto, Address>();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();
        CreateMap<AddressDto, AddressUpdateDto>().ReverseMap();
        CreateMap<AddressDto, AddressCreateDto>().ReverseMap();
        CreateMap<AddressWithNavigationProperties, AddressWithNavigationPropertiesDto>();
        CreateMap<AddressWithNavigationPropertiesDto, AddressUpdateDto>()
            .IncludeMembers(e => e.Address)
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<AddressWithNavigationProperties, AddressUpdateDto>()
            .IncludeMembers(e => e.Address)
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, CountryUpdateDto>();

        CreateMap<City, CityDto>();
        CreateMap<CityWithCountry, CityDto>();
        CreateMap<CityDto, CityUpdateDto>();

        CreateMap<District, DistrictDto>();
        CreateMap<DistrictWithCity, DistrictDto>();
        CreateMap<DistrictDto, DistrictUpdateDto>();

        CreateMap<Protocol, ProtocolDto>();
        CreateMap<Protocol, ProtocolExcelDto>();
        CreateMap<ProtocolDto, ProtocolUpdateDto>();
        CreateMap<ProtocolWithNavigationProperties, ProtocolWithNavigationPropertiesDto>();

        CreateMap<Department, DepartmentDto>();
        CreateMap<Department, DepartmentExcelDto>();
        CreateMap<DepartmentDto, DepartmentUpdateDto>();
        CreateMap<Department, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Department, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Appointment, AppointmentDto>();
        CreateMap<AppointmentDto, AppointmentUpdateDto>();
        CreateMap<AppointmentWithNavigationProperties, AppointmentWithNavigationPropertiesDto>();
        CreateMap<AppointmentDto, AppointmentCreateDto>();

        CreateMap<AppointmentReport, AppointmentReportDto>();
        CreateMap<AppointmentReportDto, AppointmentReportUpdateDto>();
        CreateMap<AppointmentReportWithNavigationProperties, AppointmentReportWithNavigationPropertiesDto>();

        CreateMap<AppointmentType, AppointmentTypeDto>();
        CreateMap<AppointmentTypeDto, AppointmentTypeUpdateDto>();       

        CreateMap<Hospital, HospitalDto>();
        CreateMap<Hospital, HospitalExcelDto>();
        CreateMap<HospitalDto, HospitalUpdateDto>();
        CreateMap<HospitalWithDepartment, HospitalDto>();
        CreateMap<Hospital, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Examination, ExaminationDto>();
        CreateMap<Examination, ExaminationExcelDto>();
        CreateMap<ExaminationDto, ExaminationUpdateDto>();

        CreateMap<Doctor, DoctorDto>();
        CreateMap<Doctor, DoctorExcelDto>();
        CreateMap<DoctorDto, DoctorUpdateDto>();
        CreateMap<Doctor, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));

        CreateMap<Title, TitleDto>();
        CreateMap<Title, TitleExcelDto>();
        CreateMap<TitleDto, TitleUpdateDto>();
        CreateMap<Title, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        //CreateMap<Notification, NotificationDto>();
        //CreateMap<Notification, NotificationExcelDto>();
        //CreateMap<NotificationDto, NotificationUpdateDto>();


        //Burası önemli


    }
}
