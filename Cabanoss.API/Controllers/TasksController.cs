using Cabanoss.Core.Model.Task;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpPost("cards")]
        public async Task AddNewTask([FromQuery]int cardId,[FromBody]TaskDto createTaskDto)
        {
            await _tasksService.AddTask(cardId, createTaskDto, User);
        }

        [HttpGet("cards")]
        public async Task<List<TaskDto>> GetCardTasks([FromQuery] int cardId)
        {
            var tasks = await _tasksService.GetCardTasks(cardId,User);
            return tasks;
        }

        [HttpGet]
        public async Task<TaskDto> GetTask([FromQuery] int taskId)
        {
           var task = await _tasksService.GetTask(taskId,User);
           return task;
        }

        [HttpPut]
        public async Task UpdateTask([FromQuery] int taskId, [FromBody] TaskDto updateTask)
        {
            await _tasksService.UpdateTask(taskId, updateTask, User);
        }

        [HttpDelete]
        public async Task DeleteTask([FromQuery] int taskId)
        {
            await _tasksService.DeleteTask(taskId, User);
        }

    }
}
