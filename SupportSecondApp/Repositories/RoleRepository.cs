using Microsoft.AspNetCore.Identity;

namespace SupportSecondApp.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        var role = new IdentityRole(roleName);
        return await _roleManager.CreateAsync(role);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }
}