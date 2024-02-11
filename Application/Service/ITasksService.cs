using Application.Model.Task;

namespace Application.Service
{
    public interface ITasksService
    {
        Task AddTask(int cardId, TaskDto createTaskDto);
        Task DeleteTask(int taskId);
        Task<List<ResponseTaskDto>> GetCardTasks(int cardId);
        Task<ResponseTaskDto> GetTask(int taskId);
        Task UpdateTask(int taskId, TaskDto taskDto);
    }
}