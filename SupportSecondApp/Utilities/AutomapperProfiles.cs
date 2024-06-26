using AutoMapper;
using SupportSecondApp.DTOs;
using SupportSecondApp.Models;

namespace SupportSecondApp.Utilities;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(
                dest => dest.SupportTasks, 
                opt => 
                    opt.MapFrom( src => src.SupportTasks )
                    );
        CreateMap<ProjectCreateDto, Project>();
        
        CreateMap<SupportTask, SupportTaskDto>();
        CreateMap<SupportTaskCreateDto, SupportTask>();
    }
}