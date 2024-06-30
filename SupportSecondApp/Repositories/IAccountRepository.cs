using SupportSecondApp.DTOs;

namespace SupportSecondApp.Repositories;

public interface IAccountRepository
{
    Task<object> Login(LoginDto model);
    Task<object> Register(RegisterDto model);
    Task<bool> AssignRole(AssignRoleDto model);
}