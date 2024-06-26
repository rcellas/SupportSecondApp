using Microsoft.EntityFrameworkCore;
using SupportSecondApp.Models;

namespace SupportSecondApp.Data;

public class ApplicationDbContext( DbContextOptions options ) : DbContext( options )
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<SupportTask> SupportTasks { get; set; }
}
