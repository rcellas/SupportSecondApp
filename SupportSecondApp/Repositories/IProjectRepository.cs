using SupportSecondApp.Models;

namespace SupportSecondApp.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllProjects();
}