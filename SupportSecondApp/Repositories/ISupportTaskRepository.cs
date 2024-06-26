using SupportSecondApp.Models;

namespace SupportSecondApp.Repositories;

public interface ISupportTaskRepository
{
    Task<List<SupportTask>> GetAllSupportTasks();
}