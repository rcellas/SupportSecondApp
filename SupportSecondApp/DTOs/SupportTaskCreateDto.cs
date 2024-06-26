using System.ComponentModel.DataAnnotations;
using SupportSecondApp.Models;

namespace SupportSecondApp.DTOs;

public class SupportTaskCreateDto
{
    [StringLength(50), Required]
    public required string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}