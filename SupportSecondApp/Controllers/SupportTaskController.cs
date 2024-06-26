using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportSecondApp.Data;
using SupportSecondApp.DTOs;
using SupportSecondApp.Models;
using SupportSecondApp.Repositories;

namespace SupportSecondApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTaskController : ControllerBase
    {
        private readonly ISupportTaskRepository _supportTaskRepository;
        private readonly IMapper _mapper;

        public SupportTaskController(ISupportTaskRepository supportTaskRepository, IMapper mapper)
        {
            _supportTaskRepository = supportTaskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupportTaskDto>>> GetAllSupportTasks()
        {
            var supportTasks = await _supportTaskRepository.GetAllSupportTasks();
            var supportTasksDtos = _mapper.Map<List<SupportTask>>(supportTasks);

            return Ok(supportTasksDtos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateSupportTask(SupportTaskCreateDto supportTaskToCreate, int projectId)
        {
            var supportTask = _mapper.Map<SupportTask>(supportTaskToCreate);
            supportTask.ProjectId = projectId;
            var supportTaskId = await _supportTaskRepository.CreateSupportTask(supportTask);

            return Ok(supportTaskId);
        }
    }
}
