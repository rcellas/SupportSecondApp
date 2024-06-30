using Microsoft.AspNetCore.Identity;

namespace SupportSecondApp.Repositories;

public interface IRoleRepository
{
    Task<IdentityResult> CreateRoleAsync(string roleName);
    Task<bool> RoleExistsAsync(string roleName);
}