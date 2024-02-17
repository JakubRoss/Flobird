using API.Swagger;
using Application.Model.Task;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// controller with main route set as tasks
    /// </summary>
    [Route("tasks")]
    [ApiController]
    [Authorize]
    [SwaggerControllerOrder(7)]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tasksService"></param>
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        /// <summary>
        /// adds a specific task to a given card
        /// </summary>
        /// <param name="cardId">card id</param>
        /// <param name="createTaskDto">Request's payload id</param>
        /// <remarks>
        /// POST flobird.azurewebsites.net/tasks/cards?cardId={id}
        /// </remarks>
        [HttpPost("cards")]
        public async Task AddNewTask([FromQuery]int cardId,[FromBody]TaskDto createTaskDto)
        {
            await _tasksService.AddTask(cardId, createTaskDto);
        }

        /// <summary>
        /// updates the specified task
        /// </summary>
        /// <param name="taskId">   task id</param>
        /// <param name="updateTask"> properties which we want to change </param>
        /// <remarks>
        /// PUT flobird.azurewebsites.net/tasks?taskId={id}
        /// </remarks>
        [HttpPut]
        public async Task UpdateTask([FromQuery] int taskId, [FromBody] TaskDto updateTask)
        {
            await _tasksService.UpdateTask(taskId, updateTask);
        }

        /// <summary>
        /// downloads certain tasks from a given card
        /// </summary>
        /// <param name="cardId">card id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/tasks/cards?cardId={id}
        /// </remarks>
        [HttpGet("cards")]
        public async Task<List<ResponseTaskDto>> GetCardTasks([FromQuery] int cardId)
        {
            var tasks = await _tasksService.GetCardTasks(cardId);
            return tasks;
        }

        /// <summary>
        /// downloads a specific task
        /// </summary>
        /// <param name="taskId">   task id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/tasks?taskId={id}
        /// </remarks>
        [HttpGet]
        public async Task<ResponseTaskDto> GetTask([FromQuery] int taskId)
        {
           var task = await _tasksService.GetTask(taskId);
           return task;
        }

        /// <summary>
        /// deletes the specified task
        /// </summary>
        /// <param name="taskId">   task id</param>
        /// <remarks>
        /// DELETE flobird.azurewebsites.net/tasks?taskId={id}
        /// </remarks>
        [HttpDelete]
        public async Task DeleteTask([FromQuery] int taskId)
        {
            await _tasksService.DeleteTask(taskId);
        }

    }
}
