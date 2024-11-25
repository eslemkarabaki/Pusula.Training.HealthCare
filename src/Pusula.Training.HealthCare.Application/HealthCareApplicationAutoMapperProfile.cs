using AutoMapper;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Departments; 
using Pusula.Training.HealthCare.Hospitals; 
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
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;

namespace Pusula.Training.HealthCare;

public class HealthCareApplicationAutoMapperProfile : Profile
{
    public HealthCareApplicationAutoMapperProfile()
    {

        CreateMap<Patient, PatientDto>();
        CreateMap<Patient, PatientExcelDto>();
        CreateMap<PatientDto, PatientUpdateDto>()
            .ForMember(e => e.DistrictId, opt => opt.MapFrom(src => src.Address.DistrictId))
            .ForMember(e => e.Address, opt => opt.MapFrom(src => src.Address.AddressLine));
        CreateMap<PatientWithAddressAndCountry, PatientDto>();
        CreateMap<PatientWithAddressAndCountry, PatientExcelDto>()
            .ForMember(e => e.Race, opt => opt.MapFrom(e => e.Country));
        CreateMap<Patient, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));

        CreateMap<Address, AddressDto>();
        CreateMap<AddressWithRelations, AddressDto>();

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
        CreateMap<Department, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Appointment, AppointmentDto>();
        CreateMap<AppointmentDto, AppointmentUpdateDto>();
        CreateMap<AppointmentWithNavigationProperties, AppointmentWithNavigationPropertiesDto>();

        CreateMap<Hospital, HospitalDto>();
        CreateMap<Hospital, HospitalExcelDto>();
        CreateMap<HospitalDto, HospitalUpdateDto>();
        CreateMap<HospitalWithDepartment, HospitalDto>();
        CreateMap<Hospital, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Examination, ExaminationDto>();
        CreateMap<Examination, ExaminationExcelDto>();

        CreateMap<Doctor, DoctorDto>();
        CreateMap<Doctor, DoctorExcelDto>();
        CreateMap<DoctorDto, DoctorUpdateDto>();
        CreateMap<Doctor, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));
       
        CreateMap<Title, TitleDto>();
        CreateMap<Title, TitleExcelDto>();
        CreateMap<TitleDto, TitleUpdateDto>();
        CreateMap<Title, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        #region Radiology

        CreateMap<RadiologyExaminationGroup, RadiologyExaminationGroupDto>();
        CreateMap<RadiologyExaminationGroup, RadiologyExaminationGroupExcelDto>();
        CreateMap<RadiologyExaminationGroupDto, RadiologyExaminationGroupUpdateDto>();
        CreateMap<RadiologyExaminationGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<RadiologyExamination, RadiologyExaminationDto>();
        CreateMap<RadiologyExamination, RadiologyExaminationExcelDto>();
        CreateMap<RadiologyExaminationDto, RadiologyExaminationUpdateDto>();
        CreateMap<RadiologyExamination, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<RadiologyExaminationProcedure, RadiologyExaminationProcedureDto>();
        CreateMap<RadiologyExaminationProcedure, RadiologyExaminationProcedureExcelDto>();
        CreateMap<RadiologyExaminationProcedureDto, RadiologyExaminationProcedureUpdateDto>();
        CreateMap<RadiologyExaminationProcedure, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Result));

        CreateMap<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(); 
        CreateMap<RadiologyExaminationDocumentDto, RadiologyExaminationDocumentUpdateDto>();
        CreateMap<RadiologyExaminationDocument, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DocumentName));

        #endregion 
    }
}
