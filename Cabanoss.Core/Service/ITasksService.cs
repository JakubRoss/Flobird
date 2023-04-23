using Cabanoss.Core.Model.Task;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface ITasksService
    {
        Task AddTask(int cardId, TaskDto createTaskDto, ClaimsPrincipal claims);
        Task DeleteTask(int taskId, ClaimsPrincipal claims);
        Task<List<TaskDto>> GetCardTasks(int cardId, ClaimsPrincipal claims);
        Task<TaskDto> GetTask(int taskId, ClaimsPrincipal claims);
        Task UpdateTask(int taskId, TaskDto taskDto, ClaimsPrincipal claims);
    }
}