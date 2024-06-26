using Microsoft.EntityFrameworkCore;
using SupportSecondApp.Models;

namespace SupportSecondApp.Data;

public class ApplicationDbContext( DbContextOptions options ) : DbContext( options )
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<SupportTask> SupportTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>()
            .HasMany(pr => pr.SupportTasks)
            .WithOne(sp => sp.Project)
            .HasForeignKey(sp => sp.ProjectId);
    }
}


