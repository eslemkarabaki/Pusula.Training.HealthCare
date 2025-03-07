using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Patients;

public class EfCorePatientRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Patient, Guid>(dbContextProvider), IPatientRepository
{
    public async Task<Patient> GetAsync(
        Guid? patientId,
        int? patientNo,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), patientId, patientNo)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));

    public async Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid? patientId,
        int? patientNo,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                await GetQueryForNavigationPropertiesAsync(), patientId, patientNo
            )
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));

    public virtual async Task<List<Patient>> GetListAsync(
        string? filterText = null,
        int? no = null,
        Guid? countryId = null,
        string? fullname = null,
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
    ) =>
        await ApplyFilter(
                  await GetQueryableAsync(),
                  filterText,
                  no,
                  countryId,
                  fullname,
                  birthDateMin,
                  birthDateMax,
                  identityNumber,
                  passportNumber,
                  emailAddress,
                  mobilePhoneNumber,
                  homePhoneNumber,
                  gender,
                  bloodType,
                  maritalStatus
              )
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<PatientWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        int? no = null,
        Guid? countryId = null,
        string? fullname = null,
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
    ) =>
        await ApplyFilter(
                  await GetQueryForNavigationPropertiesAsync(),
                  filterText,
                  no,
                  countryId,
                  fullname,
                  birthDateMin,
                  birthDateMax,
                  identityNumber,
                  passportNumber,
                  emailAddress,
                  mobilePhoneNumber,
                  homePhoneNumber,
                  gender,
                  bloodType,
                  maritalStatus
              )
              .OrderBy(GetSorting(sorting, true))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        int? no = null,
        Guid? countryId = null,
        string? fullname = null,
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
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                await GetQueryableAsync(),
                filterText,
                no,
                countryId,
                fullname,
                birthDateMin,
                birthDateMax,
                identityNumber,
                passportNumber,
                emailAddress,
                mobilePhoneNumber,
                homePhoneNumber,
                gender,
                bloodType,
                maritalStatus
            )
            .LongCountAsync(GetCancellationToken(cancellationToken));

    // todo:benchmark
    protected virtual async Task<IQueryable<PatientWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();

        return dbContext
               .Patients
               .Include(e => e.Addresses)
               .ThenInclude(e => e.District.City.Country)
               .Include(e => e.Country)
               .Include(e => e.PatientType)
               .Include(e => e.PatientNotes)
               .ThenInclude(e => e.Creator)
               .Include(e => e.Insurance)
               .Select(
                   p => new PatientWithNavigationProperties()
                   {
                       Patient = p,
                       Country = p.Country,
                       PatientType = p.PatientType,
                       PatientNotes = p.PatientNotes,
                       Insurance = p.Insurance
                   }
               );
    }

#region Delete

    public virtual async Task DeleteAllAsync(
        string? filterText = null,
        int? no = null,
        Guid? countryId = null,
        string? fullname = null,
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
        CancellationToken cancellationToken = default
    )
    {
        var ids = ApplyFilter(
                await GetQueryableAsync(),
                filterText,
                no,
                countryId,
                fullname,
                birthDateMin,
                birthDateMax,
                identityNumber,
                passportNumber,
                emailAddress,
                mobilePhoneNumber,
                homePhoneNumber,
                gender,
                bloodType,
                maritalStatus
            )
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

#endregion

#region Apply Filter

    protected virtual IQueryable<Patient> ApplyFilter(
        IQueryable<Patient> query,
        string? filterText = null,
        int? no = null,
        Guid? countryId = null,
        string? fullname = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None
    ) =>
        query
            .WhereIf(
                !string.IsNullOrWhiteSpace(filterText),
                e => EF.Functions.ILike(e.FullName, $"{filterText}%") ||
                    EF.Functions.ILike(e.IdentityNumber, $"{filterText!}%") ||
                    EF.Functions.ILike(e.PassportNumber, $"{filterText!}%")
            )
            .WhereIf(
                no.HasValue, e => e.No == no!.Value
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(fullname),
                e => e.FullName.Contains(fullname!)
            )
            .WhereIf(birthDateMin.HasValue, e => e.BirthDate >= birthDateMin!.Value)
            .WhereIf(birthDateMax.HasValue, e => e.BirthDate <= birthDateMax!.Value)
            .WhereIf(
                !string.IsNullOrWhiteSpace(identityNumber),
                e => EF.Functions.ILike(e.IdentityNumber, $"{identityNumber!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(passportNumber),
                e => EF.Functions.ILike(e.PassportNumber, $"{passportNumber!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(emailAddress),
                e => EF.Functions.ILike(e.EmailAddress, $"{emailAddress!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(mobilePhoneNumber),
                e => EF.Functions.ILike(e.MobilePhoneNumber, $"{mobilePhoneNumber!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(homePhoneNumber),
                e => EF.Functions.ILike(e.HomePhoneNumber, $"{homePhoneNumber!}%")
            )
            .WhereIf(gender != EnumGender.None, e => e.Gender == gender)
            .WhereIf(bloodType != EnumBloodType.None, e => e.BloodType == bloodType)
            .WhereIf(maritalStatus != EnumMaritalStatus.None, e => e.MaritalStatus == maritalStatus)
            .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);

    protected virtual IQueryable<Patient> ApplyFilter(
        IQueryable<Patient> query,
        Guid? id = null,
        int? no = null
    ) =>
        query
            .WhereIf(id.HasValue, e => e.Id == id)
            .WhereIf(
                no.HasValue, e => e.No == no!.Value
            );

    protected virtual IQueryable<PatientWithNavigationProperties> ApplyFilter(
        IQueryable<PatientWithNavigationProperties> query,
        string? filterText = null,
        int? no = null,
        Guid? countryId = null,
        string? fullname = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? emailAddress = null,
        string? mobilePhoneNumber = null,
        string? homePhoneNumber = null,
        EnumGender gender = EnumGender.None,
        EnumBloodType bloodType = EnumBloodType.None,
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None
    ) =>
        query
            .WhereIf(
                !string.IsNullOrWhiteSpace(filterText),
                e => EF.Functions.ILike(e.Patient.FullName, $"{filterText!}%") ||
                    EF.Functions.ILike(e.Patient.IdentityNumber, $"{filterText!}%") ||
                    EF.Functions.ILike(e.Patient.PassportNumber, $"{filterText!}%")
            )
            .WhereIf(
                no.HasValue, e => e.Patient.No == no!.Value
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(fullname),
                e => e.Patient.FullName.Contains(fullname!)
            )
            .WhereIf(birthDateMin.HasValue, e => e.Patient.BirthDate >= birthDateMin!.Value)
            .WhereIf(birthDateMax.HasValue, e => e.Patient.BirthDate <= birthDateMax!.Value)
            .WhereIf(
                !string.IsNullOrWhiteSpace(identityNumber),
                e => EF.Functions.ILike(e.Patient.IdentityNumber, $"{identityNumber!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(passportNumber),
                e => EF.Functions.ILike(e.Patient.PassportNumber, $"{passportNumber!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(emailAddress),
                e => EF.Functions.ILike(e.Patient.EmailAddress, $"{emailAddress!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(mobilePhoneNumber),
                e => EF.Functions.ILike(e.Patient.MobilePhoneNumber, $"{mobilePhoneNumber!}%")
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(homePhoneNumber),
                e => EF.Functions.ILike(e.Patient.HomePhoneNumber, $"{homePhoneNumber!}%")
            )
            .WhereIf(gender != EnumGender.None, e => e.Patient.Gender == gender)
            .WhereIf(bloodType != EnumBloodType.None, e => e.Patient.BloodType == bloodType)
            .WhereIf(maritalStatus != EnumMaritalStatus.None, e => e.Patient.MaritalStatus == maritalStatus)
            .WhereIf(countryId.HasValue, e => e.Patient.CountryId == countryId!.Value);

    protected virtual IQueryable<PatientWithNavigationProperties> ApplyFilter(
        IQueryable<PatientWithNavigationProperties> query,
        Guid? id = null,
        int? no = null
    ) =>
        query
            .WhereIf(id.HasValue, e => e.Patient.Id == id)
            .WhereIf(
                no.HasValue, e => e.Patient.No == no!.Value
            );

#endregion

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            PatientConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "Patient." : string.Empty)}{sorting}";
}