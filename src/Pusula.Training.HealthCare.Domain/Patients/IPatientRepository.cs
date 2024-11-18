using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientRepository : IRepository<Patient, Guid>
{
    Task<PatientView> GetViewAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task<List<Patient>> GetListAsync(
        string? filterText = null,
        Guid? countryId = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<List<PatientView>> GetViewListAsync(
        string? filterText = null,
        Guid? countryId = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string? filterText = null,
        Guid? countryId = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        CancellationToken cancellationToken = default);

    Task DeleteAllAsync(
        string? filterText = null,
        Guid? countryId = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None,
        CancellationToken cancellationToken = default);

    Task<bool> IdentityNumberExistsAsync(
        Guid? id,
        string identityNumber,
        CancellationToken cancellationToken = default);

    Task<bool> PassportNumberExistsAsync(
        Guid? id,
        string passportNumber,
        CancellationToken cancellationToken = default);
}