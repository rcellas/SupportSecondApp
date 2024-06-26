using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupportSecondApp.DTOs;
using SupportSecondApp.Models;
using SupportSecondApp.Repositories;

namespace SupportSecondApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            var projectsDto = _mapper.Map<List<Project>>(projects);
            return Ok(projectsDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectCreateDto>> CreateProject(ProjectCreateDto projectToCreate)
        {
            var project = _mapper.Map<Project>(projectToCreate);
            var projectId = await _projectRepository.CreateProject(project);

            return Ok(projectId);
        }
        
        
    }
}
