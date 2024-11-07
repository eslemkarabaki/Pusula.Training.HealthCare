using AutoMapper;
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
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare;

public class HealthCareApplicationAutoMapperProfile : Profile
{
    public HealthCareApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Patient, PatientDto>();
        CreateMap<Patient, PatientExcelDto>();
        CreateMap<PatientDto, PatientUpdateDto>();
        CreateMap<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>();
        CreateMap<Patient, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));

        CreateMap<Address, AddressDto>();
        CreateMap<AddressWithNavigationProperties, AddressWithNavigationPropertiesDto>();

        CreateMap<Country, CountryDto>();

        CreateMap<City, CityDto>();

        CreateMap<District, DistrictDto>();

        CreateMap<Protocol, ProtocolDto>();
        CreateMap<Protocol, ProtocolExcelDto>();
        CreateMap<ProtocolDto, ProtocolUpdateDto>();
        CreateMap<ProtocolWithNavigationProperties, ProtocolWithNavigationPropertiesDto>();

        CreateMap<Department, DepartmentDto>();
        CreateMap<Department, DepartmentExcelDto>();
        CreateMap<DepartmentDto, DepartmentUpdateDto>();
        CreateMap<Department, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Department, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Hospital, HospitalDto>();
        CreateMap<Hospital, HospitalExcelDto>();
        CreateMap<HospitalDto, HospitalUpdateDto>();
        CreateMap<HospitalWithDepartment, HospitalDto>();
        CreateMap<Hospital, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        //CreateMap<Notification, NotificationDto>();
        //CreateMap<Notification, NotificationExcelDto>();
        //CreateMap<NotificationDto, NotificationUpdateDto>();
         
         

        //Burası önemli
 

    }
}