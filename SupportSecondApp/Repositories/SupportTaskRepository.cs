using Microsoft.EntityFrameworkCore;
using SupportSecondApp.Data;
using SupportSecondApp.Models;

namespace SupportSecondApp.Repositories;

public class SupportTaskRepository : ISupportTaskRepository
{
    private readonly ApplicationDbContext _context;

    public SupportTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SupportTask>> GetAllSupportTasks()
    {
        return await _context.SupportTasks.ToListAsync();
    }
    
}