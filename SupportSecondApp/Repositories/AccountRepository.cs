using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SupportSecondApp.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SupportSecondApp.Repositories;
public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRepository(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new { message = "Unauthorized" };
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false); // Establecer la cookie de sesión
                var roles = await _userManager.GetRolesAsync(user); // Obtener roles del usuario
                return BuildLoginResponse(user, roles); // Construir objeto de respuesta
            }
            else
            {
                return new { message = "Unauthorized" };
            }
        }

        public async Task<object> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = string.IsNullOrWhiteSpace(model.FirstName) ? "" : model.FirstName,
                LastName = string.IsNullOrWhiteSpace(model.LastName) ? "" : model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user"); // Asignar automáticamente el rol "user"
                return new { message = "User registered successfully" };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return new { message = "Failed to register user", errors };
            }
        }

        public async Task<bool> AssignRole(AssignRoleDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
            {
                return false;
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles); // Remover roles actuales

            if (!removeResult.Succeeded)
            {
                return false;
            }

            var addResult = await _userManager.AddToRoleAsync(user, model.RoleName); // Asignar nuevo rol
            return addResult.Succeeded;
        }

        private async Task SignInUserAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                // Agregar más claims según sea necesario
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Cookie persistente
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Tiempo de expiración de la cookie
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        private object BuildLoginResponse(ApplicationUser user, IList<string> roles)
        {
            return new
            {
                message = "Login successful",
                userInfo = new
                {
                    userId = user.Id,
                    userName = user.UserName,
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    roles = roles
                }
            };
        }
    }

