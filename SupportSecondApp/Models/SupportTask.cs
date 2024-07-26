using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportSecondApp.Models;

public class SupportTask
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50), Required]
    public required string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public string? File { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}