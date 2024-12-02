using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientRules : IScopedDependency
{
    Task<bool> PassportNumberExistsAsync(string? passportNumber);
    Task<bool> IdentityNumberExistsAsync(string? identityNumber);
}