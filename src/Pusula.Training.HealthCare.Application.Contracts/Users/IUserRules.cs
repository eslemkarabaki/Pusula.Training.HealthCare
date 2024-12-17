using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Users;

public interface IUserRules : IScopedDependency
{
    Task EnsureUsernameNotExistAsync(string userName);
    Task EnsureUsernameNotExistForOthersAsync(string userName, Guid userId);

    Task EnsureEmailNotExistAsync(string email);
    Task EnsureEmailNotExistForOthersAsync(string email, Guid userId);
}