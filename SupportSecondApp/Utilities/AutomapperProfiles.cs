using AutoMapper;
using SupportSecondApp.DTOs;
using SupportSecondApp.Models;

namespace SupportSecondApp.Utilities;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<Project, ProjectDto>();
    }
}