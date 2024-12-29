using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientRules : IScopedDependency
{
    Task EnsurePassportNumberNotExistsAsync(string? passportNumber);
    Task EnsureIdentityNumberNotExistsAsync(string? identityNumber);
}