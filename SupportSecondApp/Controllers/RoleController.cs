using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SupportSecondApp.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SupportSecondApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateRole([FromBody] RolesCreateDto createRoleDto)
        {
            if (string.IsNullOrWhiteSpace(createRoleDto.RoleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(createRoleDto.RoleName);
            if (roleExist)
            {
                return Conflict("Role already exists.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(createRoleDto.RoleName));
            if (result.Succeeded)
            {
                return Ok(new { Result = "Role created successfully." });
            }
            else
            {
                return StatusCode(500, new { Result = "An error occurred while creating the role." });
            }
        }

        [HttpGet("AllRoles")]
        [Authorize(Policy = "RequireAuthenticatedUser")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }
    }
}