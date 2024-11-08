using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

public interface IExaminationRepository : IRepository<Examination, Guid>
{
    Task DeleteAllAsync(
        string? notes = null,
        string? chronicDiseases = null,
        string? allergies = null,
        DateTime? VisitDate = null,
        string? identityNumber = null,
        string? medications = null,
        string? diagnosis = null,
        string? prescription = null,
        string? imagingResults = null, 
        Guid? patientId = null,
        Guid? doctorId = null,
        CancellationToken cancellationToken = default);

    Task<List<Examination>> GetListAsync(
        string? notes = null,
        string? chronicDiseases = null,
        string? allergies = null,
        DateTime? VisitDate = null,
        string? identityNumber = null,
        string? medications = null,
        string? diagnosis = null,
        string? prescription = null,
        string? imagingResults = null,
        Guid? patientId = null,
        Guid? doctorId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
       string? notes = null,
        string? chronicDiseases = null,
        string? allergies = null,
        DateTime? VisitDate = null,
        string? identityNumber = null,
        string? medications = null,
        string? diagnosis = null,
        string? prescription = null,
        string? imagingResults = null,
        Guid? patientId = null,
        Guid? doctorId = null,
        CancellationToken cancellationToken = default);
}
