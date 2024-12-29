using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.GlobalExceptions;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Patients;

public class PatientRules(IPatientRepository patientRepository) : IPatientRules
{
    public async Task EnsurePassportNumberNotExistsAsync(string? passportNumber) =>
        GlobalException.ThrowIf(
            !string.IsNullOrWhiteSpace(passportNumber) &&
            await patientRepository.AnyAsync(e => e.PassportNumber == passportNumber),
            "Passport number is already exist."
        );

    public async Task EnsureIdentityNumberNotExistsAsync(string? identityNumber) =>
        GlobalException.ThrowIf(
            !string.IsNullOrWhiteSpace(identityNumber) &&
            await patientRepository.AnyAsync(
                e => e.IdentityNumber == identityNumber
            ), "Identity number is already exist."
        );
}