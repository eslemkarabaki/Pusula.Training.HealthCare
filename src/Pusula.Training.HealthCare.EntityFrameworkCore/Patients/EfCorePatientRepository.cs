using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Patients;

public class EfCorePatientRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Patient, Guid>(dbContextProvider), IPatientRepository
{
    public async Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        await (await GetQueryForNavigationPropertiesAsync())
            .FirstOrDefaultAsync(e => e.Patient.Id == id, cancellationToken);

    public async Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(
        int patientNo,
        CancellationToken cancellationToken = default
    ) =>
        await (await GetQueryForNavigationPropertiesAsync())
            .FirstOrDefaultAsync(e => e.Patient.No == patientNo, cancellationToken);

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
              .ToListAsync(cancellationToken);

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
              .ToListAsync(cancellationToken);

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

    public virtual async Task<bool> IdentityNumberExistsAsync(
        Guid? id,
        string identityNumber,
        CancellationToken cancellationToken = default
    )
    {
        var query = (await GetQueryableAsync()).WhereIf(id.HasValue, e => e.Id != id);
        return await query.AnyAsync(e => e.IdentityNumber == identityNumber, cancellationToken);
    }

    public virtual async Task<bool> PassportNumberExistsAsync(
        Guid? id,
        string passportNumber,
        CancellationToken cancellationToken = default
    )
    {
        var query = (await GetQueryableAsync()).WhereIf(id.HasValue, e => e.Id != id);
        return await query.AnyAsync(e => e.PassportNumber == passportNumber, cancellationToken);
    }

    protected virtual async Task<IQueryable<PatientWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();

        var patients =
            from patient in dbContext.Patients
            join country in dbContext.Countries
                on patient.CountryId equals country.Id
                into countries
            from country in countries.DefaultIfEmpty()
            join patientType in dbContext.PatientTypes
                on patient.PatientTypeId equals patientType.Id
                into patientTypes
            from patientType in patientTypes.DefaultIfEmpty()
            select new
            {
                patient,
                country,
                patientType
            };

        var addresses = (
            from address in dbContext.Addresses
            where patients.Select(e => e.patient.Id).Contains(address.PatientId)
            join district in dbContext.Districts
                on address.DistrictId equals district.Id into districts
            from district in districts.DefaultIfEmpty()
            join city in dbContext.Cities
                on district.CityId equals city.Id into cities
            from city in cities.DefaultIfEmpty()
            join country in dbContext.Countries
                on city.CountryId equals country.Id into countries
            from country in countries.DefaultIfEmpty()
            select new AddressWithNavigationProperties()
            {
                Address = address,
                District = district,
                Country = country,
                City = city
            }).AsEnumerable();

        return patients.Select(
            p => new PatientWithNavigationProperties()
            {
                Patient = p.patient,
                Country = p.country,
                PatientType = p.patientType,
                Addresses = addresses.Where(a => a.Address.PatientId == p.patient.Id).ToList()
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
                e => e.FullName!.Contains(filterText!) ||
                    e.IdentityNumber!.Contains(filterText!) ||
                    e.PassportNumber!.Contains(filterText!)
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
                e => e.IdentityNumber!.Contains(identityNumber!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(passportNumber),
                e => e.PassportNumber!.Contains(passportNumber!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(emailAddress),
                e => e.EmailAddress.Contains(emailAddress!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(mobilePhoneNumber),
                e => e.MobilePhoneNumber.Contains(mobilePhoneNumber!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(homePhoneNumber),
                e => e.HomePhoneNumber != null &&
                    e.HomePhoneNumber.Contains(homePhoneNumber!)
            )
            .WhereIf(gender != EnumGender.None, e => e.Gender == gender)
            .WhereIf(bloodType != EnumBloodType.None, e => e.BloodType == bloodType)
            .WhereIf(maritalStatus != EnumMaritalStatus.None, e => e.MaritalStatus == maritalStatus)
            .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);

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
                e => e.Patient.FullName!.Contains(filterText!) ||
                    e.Patient.IdentityNumber!.Contains(filterText!) ||
                    e.Patient.PassportNumber!.Contains(filterText!)
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
                e => e.Patient.IdentityNumber!.Contains(identityNumber!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(passportNumber),
                e => e.Patient.PassportNumber!.Contains(passportNumber!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(emailAddress),
                e => e.Patient.EmailAddress.Contains(emailAddress!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(mobilePhoneNumber),
                e => e.Patient.MobilePhoneNumber.Contains(mobilePhoneNumber!)
            )
            .WhereIf(
                !string.IsNullOrWhiteSpace(homePhoneNumber),
                e => e.Patient.HomePhoneNumber != null &&
                    e.Patient.HomePhoneNumber.Contains(homePhoneNumber!)
            )
            .WhereIf(gender != EnumGender.None, e => e.Patient.Gender == gender)
            .WhereIf(bloodType != EnumBloodType.None, e => e.Patient.BloodType == bloodType)
            .WhereIf(maritalStatus != EnumMaritalStatus.None, e => e.Patient.MaritalStatus == maritalStatus)
            .WhereIf(countryId.HasValue, e => e.Patient.CountryId == countryId!.Value);

#endregion

    private string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            PatientConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "Patient." : string.Empty)}{sorting}";
}