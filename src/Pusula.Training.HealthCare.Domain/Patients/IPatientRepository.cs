using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientRepository : IRepository<Patient, Guid>
{
    Task<PatientWithAddressAndCountry> GetWithAddressAndCountryAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

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
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<List<PatientWithAddressAndCountry>> GetListWithAddressAndCountryAsync(
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
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);

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
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);
}