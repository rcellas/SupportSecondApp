using Microsoft.EntityFrameworkCore;
using SupportSecondApp.Data;
using SupportSecondApp.Models;

namespace SupportSecondApp.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllProjects()
    {
        return await _context.Projects.Include( x => x.SupportTasks).ToListAsync();
    }

    public async Task<int> CreateProject( Project project )
    {
        _context.Add(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }
}