using System.ComponentModel.DataAnnotations;

namespace SupportSecondApp.DTOs
{
    public class AssignRoleDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}