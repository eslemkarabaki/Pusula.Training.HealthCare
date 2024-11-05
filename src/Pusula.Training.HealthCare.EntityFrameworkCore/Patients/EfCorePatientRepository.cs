using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Patients;

public class EfCorePatientRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Patient, Guid>(dbContextProvider), IPatientRepository
{
    public virtual async Task<List<Patient>> GetListAsync(
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
        CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryableAsync(), filterText, firstName, lastName, birthDateMin, birthDateMax,
                identityNumber,
                emailAddress, mobilePhoneNumber, homePhoneNumber, gender, bloodType, maritalStatus, countryId)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientConsts.GetDefaultSorting(false) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();

        var patient = (await GetDbSetAsync()).Where(patient => patient.Id == id)
            .Select(patient => new PatientWithNavigationProperties()
            {
                Patient = patient,
                Address = new AddressWithNavigationProperties()
                {
                    Address = dbContext.Set<Address>().FirstOrDefault(address => address.PatientId == patient.Id)!
                },
                Country = dbContext.Set<Country>().FirstOrDefault(country => country.Id == patient.CountryId)!
            }).FirstOrDefault()!;

        patient.Address.District =
            dbContext.Set<District>().FirstOrDefault(address => address.Id == patient.Address.Address.DistrictId)!;
        patient.Address.City =
            dbContext.Set<City>().FirstOrDefault(city => city.Id == patient.Address.District.CityId)!;
        patient.Address.Country =
            dbContext.Set<Country>().FirstOrDefault(country => country.Id == patient.Address.City.CountryId)!;

        return patient;
    }

    public async Task<List<PatientWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
        CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryForNavigationPropertiesAsync(), filterText, firstName, lastName,
                birthDateMin, birthDateMax, identityNumber, emailAddress, mobilePhoneNumber, homePhoneNumber, gender,
                bloodType, maritalStatus, countryId)
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientConsts.GetDefaultSorting(true) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }


    public virtual async Task<long> GetCountAsync(
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
        CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetDbSetAsync(), filterText, firstName, lastName, birthDateMin, birthDateMax,
                identityNumber, emailAddress, mobilePhoneNumber, homePhoneNumber, gender, bloodType, maritalStatus,
                countryId)
            .LongCountAsync(GetCancellationToken(cancellationToken));
    }

    #region Delete

    public virtual async Task DeleteAllAsync(
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
        CancellationToken cancellationToken = default)
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, firstName, lastName, birthDateMin, birthDateMax,
                identityNumber,
                emailAddress, mobilePhoneNumber, homePhoneNumber, gender, bloodType, maritalStatus, countryId)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    #endregion

    protected virtual async Task<IQueryable<PatientWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from patient in await GetDbSetAsync()
            join patient_country in (await GetDbContextAsync()).Set<Country>() on patient.CountryId equals
                patient_country.Id into
                patient_countries
            from patient_country in patient_countries.DefaultIfEmpty()
            join address in (await GetDbContextAsync()).Set<Address>() on patient.Id equals address.PatientId into
                addresses
            from address in addresses.DefaultIfEmpty()
            join district in (await GetDbContextAsync()).Set<District>() on address.DistrictId equals district.Id into
                districts
            from district in districts.DefaultIfEmpty()
            join city in (await GetDbContextAsync()).Set<City>() on district.CityId equals city.Id into cities
            from city in cities.DefaultIfEmpty()
            join address_country in (await GetDbContextAsync()).Set<Country>() on city.CountryId equals address_country
                .Id into address_countries
            from address_country in address_countries.DefaultIfEmpty()
            select new PatientWithNavigationProperties
            {
                Address = new AddressWithNavigationProperties()
                {
                    Address = address,
                    District = district,
                    City = city,
                    Country = address_country
                },
                Patient = patient,
                Country = patient_country
            };
    }

    #region Apply Filter

    protected virtual IQueryable<Patient> ApplyFilter(
        IQueryable<Patient> query,
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
        Guid? countryId = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                e => e.FirstName!.Contains(filterText!) || e.LastName!.Contains(filterText!) ||
                     e.IdentityNumber!.Contains(filterText!) || e.EmailAddress!.Contains(filterText!) ||
                     e.MobilePhoneNumber!.Contains(filterText!) || e.HomePhoneNumber!.Contains(filterText!))
            .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName!))
            .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName!))
            .WhereIf(birthDateMin.HasValue, e => e.BirthDate >= birthDateMin!.Value)
            .WhereIf(birthDateMax.HasValue, e => e.BirthDate <= birthDateMax!.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber.Contains(identityNumber!))
            .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.EmailAddress.Contains(emailAddress!))
            .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber),
                e => e.MobilePhoneNumber.Contains(mobilePhoneNumber!))
            .WhereIf(!string.IsNullOrWhiteSpace(homePhoneNumber),
                e => e.HomePhoneNumber != null && e.HomePhoneNumber.Contains(homePhoneNumber!))
            .WhereIf(gender.HasValue, e => e.Gender == gender!.Value)
            .WhereIf(bloodType.HasValue, e => e.BloodType == bloodType!.Value)
            .WhereIf(maritalStatus.HasValue, e => e.MaritalStatus == maritalStatus!.Value)
            .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);
    }

    protected virtual IQueryable<PatientWithNavigationProperties> ApplyFilter(
        IQueryable<PatientWithNavigationProperties> query,
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
        Guid? countryId = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                e => e.Patient.FirstName!.Contains(filterText!) || e.Patient.LastName!.Contains(filterText!) ||
                     e.Patient.IdentityNumber!.Contains(filterText!) || e.Patient.EmailAddress!.Contains(filterText!) ||
                     e.Patient.MobilePhoneNumber!.Contains(filterText!) ||
                     e.Patient.HomePhoneNumber!.Contains(filterText!))
            .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.Patient.FirstName.Contains(firstName!))
            .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.Patient.LastName.Contains(lastName!))
            .WhereIf(birthDateMin.HasValue, e => e.Patient.BirthDate >= birthDateMin!.Value)
            .WhereIf(birthDateMax.HasValue, e => e.Patient.BirthDate <= birthDateMax!.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(identityNumber),
                e => e.Patient.IdentityNumber.Contains(identityNumber!))
            .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.Patient.EmailAddress.Contains(emailAddress!))
            .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber),
                e => e.Patient.MobilePhoneNumber.Contains(mobilePhoneNumber!))
            .WhereIf(!string.IsNullOrWhiteSpace(homePhoneNumber),
                e => e.Patient.HomePhoneNumber != null && e.Patient.HomePhoneNumber.Contains(homePhoneNumber!))
            .WhereIf(gender.HasValue, e => e.Patient.Gender == gender!.Value)
            .WhereIf(bloodType.HasValue, e => e.Patient.BloodType == bloodType!.Value)
            .WhereIf(maritalStatus.HasValue, e => e.Patient.MaritalStatus == maritalStatus!.Value)
            .WhereIf(countryId.HasValue, e => e.Country.Id == countryId!.Value);
    }

    #endregion
}