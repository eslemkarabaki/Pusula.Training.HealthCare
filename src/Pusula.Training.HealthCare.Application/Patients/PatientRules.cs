using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Patients;

public class PatientRules(IPatientRepository patientRepository) : IPatientRules
{
    public async Task<bool> PassportNumberExistsAsync(string? passportNumber) =>
        !passportNumber.IsNullOrWhiteSpace() &&
        await patientRepository.AnyAsync(e => e.PassportNumber == passportNumber);

    public async Task<bool> IdentityNumberExistsAsync(string? identityNumber) =>
        !identityNumber.IsNullOrWhiteSpace() &&
        await patientRepository.AnyAsync(e => e.IdentityNumber == identityNumber);
}