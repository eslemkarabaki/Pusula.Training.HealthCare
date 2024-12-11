using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Users;

public interface IUserRules: IScopedDependency
{
    Task EnsureUsernameNotExistAsync(string userName);
    Task EnsureEmailNotExistAsync(string email);
}