using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SupportSecondApp.DTOs;
using SupportSecondApp.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SupportSecondApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountRepository accountRepository, SignInManager<ApplicationUser> signInManager)
        {
            _accountRepository = accountRepository;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _accountRepository.Login(model);
            if (result == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Desautenticar al usuario

            // Eliminar la cookie de sesión explícitamente
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { message = "Logout successful" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountRepository.Register(model);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
        {
            var result = await _accountRepository.AssignRole(model);

            if (result)
            {
                return Ok(new { message = $"Role '{model.RoleName}' assigned to user '{model.Email}' successfully" });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
