using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportSecondApp.Data;
using SupportSecondApp.DTOs;
using SupportSecondApp.Models;
using SupportSecondApp.Repositories;
using SupportSecondApp.Utilities;

namespace SupportSecondApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTaskController : ControllerBase
    {
        private readonly ISupportTaskRepository _supportTaskRepository;
        private readonly IMapper _mapper;
        private static readonly string container="supportTasks";

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
        public async Task<ActionResult<int>> CreateSupportTask([FromForm]SupportTaskCreateDto supportTaskToCreate, int projectId, IFileStorage fileStorage)
        {
            var supportTask = _mapper.Map<SupportTask>(supportTaskToCreate);
            if (supportTaskToCreate.File is not null)
            {
                string url = await fileStorage.Storage(container, supportTaskToCreate.File);
                supportTask.File = url;
            }
            supportTask.ProjectId = projectId;
            await _supportTaskRepository.CreateSupportTask(supportTask);

            return Ok(supportTask);
        }
    }
}
