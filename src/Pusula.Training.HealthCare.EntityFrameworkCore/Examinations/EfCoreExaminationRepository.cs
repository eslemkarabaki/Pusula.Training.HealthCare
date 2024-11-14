using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Examinations;

public class EfCoreExaminationRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Examination, Guid>(dbContextProvider), IExaminationRepository
{
    public virtual async Task DeleteAllAsync(
      
      CancellationToken cancellationToken = default)
    {

        var query = await GetQueryableAsync();

        query =(query);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? notes = null, string? chronicDiseases = null, string? allergies = null, DateTime? visitDate = null, string? identityNumber = null, 
        string? medications = null, string? diagnosis = null, string? prescription = null, string? imagingResults = null, Guid? patientId = null, Guid? doctorId = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, notes, chronicDiseases, allergies, visitDate, identityNumber, medications, diagnosis, prescription, imagingResults, patientId, doctorId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));

    }

    public virtual async Task<List<Examination>> GetListAsync(string? filterText = null,
string? notes = null, string? chronicDiseases = null, string? allergies = null, DateTime? visitDate = null, string? identityNumber = null,
        string? medications = null, string? diagnosis = null, string? prescription = null, string? imagingResults = null, Guid? patientId = null, Guid? doctorId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, notes, chronicDiseases, allergies, visitDate, identityNumber, medications, diagnosis,prescription,imagingResults,patientId,doctorId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExaminationConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

  
    protected virtual IQueryable<Examination> ApplyFilter(
      IQueryable<Examination> query,
      string? filterText = null,
     string? notes = null, string? chronicDiseases = null, string? allergies = null, DateTime? visitDate = null, string? identityNumber = null,
        string? medications = null, string? diagnosis = null, string? prescription = null, string? imagingResults = null, Guid? patientId = null, Guid? doctorId = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                e => e.IdentityNumber!.Contains(filterText!) || e.Prescription!.Contains(filterText!) ||
                     e.Diagnosis!.Contains(filterText!) || e.Medications!.Contains(filterText!))
            .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber.Contains(identityNumber!))
            .WhereIf(!string.IsNullOrWhiteSpace(notes), e => e.Notes.Contains(notes!))
            .WhereIf(!string.IsNullOrWhiteSpace(chronicDiseases), e => e.ChronicDiseases.Contains(chronicDiseases!))
            .WhereIf(!string.IsNullOrWhiteSpace(allergies), e => e.Allergies.Contains(allergies!))
            .WhereIf(!string.IsNullOrWhiteSpace(medications),e => e.Medications.Contains(medications!))
            .WhereIf(!string.IsNullOrWhiteSpace(diagnosis), e => e.Diagnosis.Contains(diagnosis!))
            .WhereIf(!string.IsNullOrWhiteSpace(prescription), e => e.Prescription.Contains(prescription!))
            .WhereIf(!string.IsNullOrWhiteSpace(imagingResults), e => e.ImagingResults.Contains(imagingResults!))
            .WhereIf(patientId.HasValue, e => e.PatientId == patientId!.Value)
            .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId!.Value);
    }
}
   
   