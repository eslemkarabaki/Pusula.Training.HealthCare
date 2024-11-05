using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientRepository : IRepository<Patient, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender? gender = null,
        EnumBloodType? bloodType = null,
        EnumMaritalStatus? maritalStatus = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);

    Task<List<Patient>> GetListAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender? gender = null,
        EnumBloodType? bloodType = null,
        EnumMaritalStatus? maritalStatus = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task<List<PatientWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender? gender = null,
        EnumBloodType? bloodType = null,
        EnumMaritalStatus? maritalStatus = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender? gender = null,
        EnumBloodType? bloodType = null,
        EnumMaritalStatus? maritalStatus = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);
}