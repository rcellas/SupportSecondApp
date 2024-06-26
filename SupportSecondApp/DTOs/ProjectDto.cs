using System.ComponentModel.DataAnnotations;

namespace SupportSecondApp.DTOs;

public class ProjectDto
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50), Required]
    public required string Name { get; set; }

    [StringLength(500), Required]
    public required string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } = null;
}