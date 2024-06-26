using SupportSecondApp.Models;

namespace SupportSecondApp.Repositories;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllProjects();
    Task<int> CreateProject( Project project );
}