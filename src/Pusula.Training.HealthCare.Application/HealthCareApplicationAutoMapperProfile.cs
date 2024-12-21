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
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Tests;
using Pusula.Training.HealthCare.TestTypes;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Pusula.Training.HealthCare.Extensions;
using Pusula.Training.HealthCare.PatientTypes;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Pusula.Training.HealthCare.PatientNotes;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.ProtocolTypeActions;
using Pusula.Training.HealthCare.ExaminationsPhysical;

namespace Pusula.Training.HealthCare;

public class HealthCareApplicationAutoMapperProfile : Profile
{
    public HealthCareApplicationAutoMapperProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(e => e.IdentityNumber, o => o.MapFrom(p => p.IdentityNumber.Censor('*', 3)))
            .ForMember(e => e.PassportNumber, o => o.MapFrom(p => p.PassportNumber.Censor('*', 3)));
        CreateMap<Patient, PatientUpdateDto>();
        CreateMap<PatientDto, PatientUpdateDto>();
        CreateMap<PatientWithNavigationProperties, PatientUpdateDto>()
            .IncludeMembers(e => e.Patient)
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<PatientWithNavigationPropertiesDto, PatientUpdateDto>()
            .IncludeMembers(e => e.Patient)
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>();

        CreateMap<Patient, PatientExcelDto>()
            .ForMember(e => e.IdentityNumber, o => o.MapFrom(p => p.IdentityNumber.Censor('*', 3)))
            .ForMember(e => e.PassportNumber, o => o.MapFrom(p => p.PassportNumber.Censor('*', 3)));
        CreateMap<PatientWithNavigationProperties, PatientExcelDto>()
            .IncludeMembers(e => e.Patient)
            .ForMember(e => e.Race, opt => opt.MapFrom(e => e.Country.Name))
            .ForMember(e => e.Type, opt => opt.MapFrom(e => e.PatientType.Name));

        CreateMap<PatientNote, PatientNoteDto>()
            .ForMember(e => e.CreatorName, e => e.MapFrom(o => o.Creator!.UserName));
        CreateMap<Patient, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FullName));

        CreateMap<PatientNoteCreateDto, PatientNote>().ReverseMap();
        CreateMap<PatientNoteUpdateDto, PatientNote>().ReverseMap();
        CreateMap<PatientNoteDto, PatientNoteUpdateDto>();
        CreateMap<PatientNoteDto, PatientNoteCreateDto>();

        CreateMap<PatientType, PatientTypeDto>();
        CreateMap<ProtocolType, ProtocolTypeDto>();
        CreateMap<ProtocolTypeDto, ProtocolTypeUpdateDto>();
        CreateMap<ProtocolTypeWithNavigationProperties, ProtocolTypeWithNavigationPropertiesDto>();

        CreateMap<ProtocolTypeAction, ProtocolTypeActionDto>();

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<AddressCreateDto, Address>();
        CreateMap<AddressCreateDto, AddressCreateDto>();
        CreateMap<AddressUpdateDto, AddressUpdateDto>();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();
        CreateMap<AddressDto, AddressUpdateDto>().ReverseMap();
        CreateMap<AddressDto, AddressCreateDto>().ReverseMap();

        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<CountryDto, CountryUpdateDto>();

        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<CityDto, CityUpdateDto>();
        CreateMap<Diagnosis, DiagnosisDto>().ReverseMap();
        CreateMap<DiagnosisDto, DiagnosisUpdateDto>();
        CreateMap<District, DistrictDto>().ReverseMap();
        CreateMap<DistrictDto, DistrictUpdateDto>();

        CreateMap<Protocol, ProtocolDto>();
        CreateMap<Protocol, ProtocolExcelDto>();

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

        CreateMap<AppointmentType, AppointmentTypeDto>();
        CreateMap<AppointmentTypeDto, AppointmentTypeUpdateDto>();

        CreateMap<Insurance, InsuranceDto>();
        CreateMap<InsuranceDto, InsuranceUpdateDto>();

        CreateMap<Hospital, HospitalDto>();
        CreateMap<Hospital, HospitalExcelDto>();
        CreateMap<HospitalDto, HospitalUpdateDto>();
        CreateMap<HospitalWithDepartment, HospitalDto>();
        CreateMap<Hospital, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Examination, ExaminationDto>();
        CreateMap<Examination, ExaminationExcelDto>();
        CreateMap<ExaminationDto, ExaminationUpdateDto>();

        CreateMap<ExaminationPhysical, ExaminationPhysicalDto>();
        CreateMap<ExaminationPhysicalDto, ExaminationPhysicalUpdateDto>();

        CreateMap<Doctor, DoctorDto>();
        CreateMap<Doctor, DoctorExcelDto>();
        CreateMap<DoctorDto, DoctorUpdateDto>();
        CreateMap<Doctor, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));
        CreateMap<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>();

        CreateMap<Title, TitleDto>();
        CreateMap<Title, TitleExcelDto>();
        CreateMap<TitleDto, TitleUpdateDto>();
        CreateMap<Title, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Test, TestDto>();
        CreateMap<Test, TestExcelDto>();
        CreateMap<TestDto, TestUpdateDto>();
        CreateMap<Test, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<TestType, TestTypeDto>();
        CreateMap<TestType, TestTypeExcelDto>();
        CreateMap<TestTypeDto, TestTypeUpdateDto>();
        CreateMap<TestType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<TestGroup, TestGroupDto>();
        CreateMap<TestGroup, TestGroupExcelDto>();
        CreateMap<TestGroupDto, TestGroupUpdateDto>();
        CreateMap<TestGroup, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        #region Radiology

        CreateMap<RadiologyExaminationGroup, RadiologyExaminationGroupDto>();
        CreateMap<RadiologyExaminationGroup, RadiologyExaminationGroupExcelDto>();
        CreateMap<RadiologyExaminationGroupDto, RadiologyExaminationGroupUpdateDto>();
        CreateMap<RadiologyExaminationGroup, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<RadiologyExamination, RadiologyExaminationDto>();
        CreateMap<RadiologyExamination, RadiologyExaminationExcelDto>();
        CreateMap<RadiologyExaminationDto, RadiologyExaminationUpdateDto>();
        CreateMap<RadiologyExamination, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<RadiologyExaminationProcedure, RadiologyExaminationProcedureDto>();
        CreateMap<RadiologyExaminationProcedure, RadiologyExaminationProcedureExcelDto>();
        CreateMap<RadiologyExaminationProcedureDto, RadiologyExaminationProcedureUpdateDto>();
        CreateMap<RadiologyExaminationProcedure, LookupDto<Guid>>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Result));

        CreateMap<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>();
        CreateMap<RadiologyExaminationDocumentDto, RadiologyExaminationDocumentUpdateDto>(); 

        CreateMap<RadiologyRequest, RadiologyRequestDto>();
        CreateMap<RadiologyRequest, RadiologyRequestExcelDownloadDto>();
        CreateMap<RadiologyRequestDto, RadiologyRequestUpdateDto>();
        CreateMap<RadiologyRequestWithNavigationProperties, RadiologyRequestWithNavigationPropertiesDto>();

        CreateMap<RadiologyRequestItem, RadiologyRequestItemExcelDownloadDto>();
        CreateMap<RadiologyRequestItemDto, RadiologyRequestItemUpdateDto>(); 
        CreateMap<RadiologyRequestItem, RadiologyRequestItemDto>();
        CreateMap<RadiologyRequestItemWithNavigationProperties, RadiologyRequestItemWithNavigationPropertiesDto>()
          .ForMember(dest => dest.RadiologyRequestItem, opt => opt.MapFrom(src => src.RadiologyRequestItem))
          .ForMember(dest => dest.RadiologyExamination, opt => opt.MapFrom(src => src.RadiologyExamination))
          .ForMember(dest => dest.RadiologyRequest, opt => opt.MapFrom(src => src.RadiologyRequest))
          .ForMember(dest => dest.Protocol, opt => opt.MapFrom(src => src.Protocol))
          .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
          .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
          .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient));

        #endregion
    }
}