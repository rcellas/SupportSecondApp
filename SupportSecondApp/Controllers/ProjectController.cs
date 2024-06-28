using AutoMapper;
using FluentValidation;
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
        private IValidator<ProjectCreateDto> _validator;
        public ProjectController(IProjectRepository projectRepository, IMapper mapper,IValidator<ProjectCreateDto> createValidator )
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _validator = createValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            var projectsDto = _mapper.Map<IEnumerable<Project>>(projects);
            return Ok(projectsDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectCreateDto>> CreateProject(ProjectCreateDto projectToCreate)
        {
            var validatorResult = await _validator.ValidateAsync(projectToCreate);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var project = _mapper.Map<Project>(projectToCreate);
            var projectId = await _projectRepository.CreateProject(project);

            return Ok(projectId);
        }
        
        
    }
}
