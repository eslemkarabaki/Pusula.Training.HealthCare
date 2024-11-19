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
using Pusula.Training.HealthCare.PatientTypes;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Patients;

public class EfCorePatientRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Patient, Guid>(dbContextProvider), IPatientRepository
{
    public async Task<PatientView> GetViewAsync(Guid id,
                                                CancellationToken cancellationToken = default)
    {
        return await (await GetQueryForViewAsync())
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<List<Patient>> GetListAsync(
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
        CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryableAsync(), filterText, countryId, firstName, lastName, birthDateMin,
                         birthDateMax, identityNumber, passportNumber, emailAddress, mobilePhoneNumber, homePhoneNumber,
                         gender, bloodType, maritalStatus)
                     .OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientConsts.GetDefaultSorting(false) : sorting)
                     .PageBy(skipCount, maxResultCount)
                     .ToListAsync(cancellationToken);
    }

    public async Task<List<PatientView>> GetViewListAsync(
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
        CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryForViewAsync(), filterText, countryId, firstName, lastName,
                         birthDateMin, birthDateMax, identityNumber, passportNumber, emailAddress, mobilePhoneNumber,
                         homePhoneNumber, gender, bloodType, maritalStatus)
                     .OrderBy(string.IsNullOrWhiteSpace(sorting) ? PatientConsts.GetDefaultSorting(false) : sorting)
                     .PageBy(skipCount, maxResultCount)
                     .ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(
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
        CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryableAsync(), filterText, countryId, firstName, lastName, birthDateMin,
                birthDateMax, identityNumber, passportNumber, emailAddress, mobilePhoneNumber, homePhoneNumber, gender,
                bloodType, maritalStatus)
            .LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> IdentityNumberExistsAsync(
        Guid? id,
        string identityNumber,
        CancellationToken cancellationToken = default)
    {
        var query = (await GetQueryableAsync()).WhereIf(id.HasValue, e => e.Id != id);
        return await query.AnyAsync(e => e.IdentityNumber == identityNumber, cancellationToken);
    }

    public virtual async Task<bool> PassportNumberExistsAsync(
        Guid? id,
        string passportNumber,
        CancellationToken cancellationToken = default)
    {
        var query = (await GetQueryableAsync()).WhereIf(id.HasValue, e => e.Id != id);
        return await query.AnyAsync(e => e.PassportNumber == passportNumber, cancellationToken);
    }


    protected virtual async Task<IQueryable<PatientView>> GetQueryForViewAsync()
    {
        var dbContext = await GetDbContextAsync();

        // var x = (await GetDbSetAsync())
        //         .Join(dbContext.Countries, e => e.CountryId, c => c.Id, (patient, country) => new { patient, country })
        //         .Join(dbContext.PatientTypes, e => e.patient.PatientTypeId, pt => pt.Id,
        //             (patientCountries, patientType) => new { patientCountries, patientType })

        var patients = from patient in await GetDbSetAsync()
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

        var addresses = (from address in dbContext.Addresses
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
                         select new AddressView()
                         {
                             Id = address.Id,
                             PatientId = address.PatientId,
                             DistrictId = district.Id,
                             District = district.Name,
                             CityId = city.Id,
                             City = city.Name,
                             CountryId = country.Id,
                             Country = country.Name,
                             AddressTitle = address.AddressTitle,
                             AddressLine = address.AddressLine
                         }).AsEnumerable();

        return patients.Select(e => new PatientView()
        {
            Id = e.patient.Id,
            IdentityNumber = e.patient.IdentityNumber,
            FirstName = e.patient.FirstName,
            LastName = e.patient.LastName,
            BirthDate = e.patient.BirthDate,
            Gender = e.patient.Gender,
            BloodType = e.patient.BloodType,
            MaritalStatus = e.patient.MaritalStatus,
            MobilePhoneNumberCode = e.patient.MobilePhoneNumberCode,
            MobilePhoneNumber = e.patient.MobilePhoneNumber,
            HomePhoneNumberCode = e.patient.HomePhoneNumberCode,
            HomePhoneNumber = e.patient.HomePhoneNumber,
            EmailAddress = e.patient.EmailAddress,
            PassportNumber = e.patient.PassportNumber,
            CountryId = e.country.Id,
            Country = e.country.Name,
            PatientType = e.patientType.Name,
            PatientTypeId = e.patientType.Id,
            Addresses = addresses.Where(a => a.PatientId == e.patient.Id).ToList()
        });
    }

#region Delete

    public virtual async Task DeleteAllAsync(
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
        CancellationToken cancellationToken = default)
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, countryId, firstName, lastName, birthDateMin,
                birthDateMax, identityNumber, passportNumber, emailAddress, mobilePhoneNumber, homePhoneNumber, gender,
                bloodType, maritalStatus)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

#endregion

#region Apply Filter

    protected virtual IQueryable<Patient> ApplyFilter(
        IQueryable<Patient> query,
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
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None)
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
               .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber!.Contains(identityNumber!))
               .WhereIf(!string.IsNullOrWhiteSpace(passportNumber), e => e.PassportNumber!.Contains(passportNumber!))
               .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.EmailAddress.Contains(emailAddress!))
               .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber),
                   e => e.MobilePhoneNumber.Contains(mobilePhoneNumber!))
               .WhereIf(!string.IsNullOrWhiteSpace(homePhoneNumber),
                   e => e.HomePhoneNumber != null && e.HomePhoneNumber.Contains(homePhoneNumber!))
               .WhereIf(gender != EnumGender.None, e => e.Gender == gender)
               .WhereIf(bloodType != EnumBloodType.None, e => e.BloodType == bloodType)
               .WhereIf(maritalStatus != EnumMaritalStatus.None, e => e.MaritalStatus == maritalStatus)
               .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);
    }

    protected virtual IQueryable<PatientView> ApplyFilter(
        IQueryable<PatientView> query,
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
        EnumMaritalStatus maritalStatus = EnumMaritalStatus.None)
    {
        return query
               .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                   e => e.FirstName!.Contains(filterText!) || e.LastName!.Contains(filterText!) ||
                       e.IdentityNumber!.Contains(filterText!) || e.EmailAddress!.Contains(filterText!) ||
                       e.MobilePhoneNumber!.Contains(filterText!) ||
                       e.HomePhoneNumber!.Contains(filterText!))
               .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName!))
               .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName!))
               .WhereIf(birthDateMin.HasValue, e => e.BirthDate >= birthDateMin!.Value)
               .WhereIf(birthDateMax.HasValue, e => e.BirthDate <= birthDateMax!.Value)
               .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber!.Contains(identityNumber!))
               .WhereIf(!string.IsNullOrWhiteSpace(passportNumber), e => e.PassportNumber!.Contains(passportNumber!))
               .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.EmailAddress.Contains(emailAddress!))
               .WhereIf(!string.IsNullOrWhiteSpace(mobilePhoneNumber),
                   e => e.MobilePhoneNumber.Contains(mobilePhoneNumber!))
               .WhereIf(!string.IsNullOrWhiteSpace(homePhoneNumber),
                   e => e.HomePhoneNumber != null && e.HomePhoneNumber.Contains(homePhoneNumber!))
               .WhereIf(gender != EnumGender.None, e => e.Gender == gender)
               .WhereIf(bloodType != EnumBloodType.None, e => e.BloodType == bloodType)
               .WhereIf(maritalStatus != EnumMaritalStatus.None, e => e.MaritalStatus == maritalStatus)
               .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);
    }

#endregion
}