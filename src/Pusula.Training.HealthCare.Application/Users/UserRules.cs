using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pusula.Training.HealthCare.GlobalExceptions;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Users;

public class UserRules(IdentityUserManager userManager) : IUserRules
{
    public async Task EnsureUsernameNotExistAsync(string userName) =>
        GlobalException.ThrowIf(
            await userManager.FindByNameAsync(userName) != null, "User already exists with the given username."
        );

    public async Task EnsureEmailNotExistAsync(string email) =>
        GlobalException.ThrowIf(
            await userManager.FindByNameAsync(email) != null, "User already exists with the given username."
        );
}