using Application.Authorization;
using Application.Model.Task;
using AutoMapper;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Application.Service.Impl
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IHttpUserContextService _httpUserContextService;

        public TasksService(
            IMapper mapper,
            ITasksRepository tasksRepository,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService,
            IHttpUserContextService httpUserContextService)
        {
            _tasksRepository = tasksRepository;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _httpUserContextService = httpUserContextService;
        }

        #region Utils
        private async Task<Board> GetBoardByCardId(int cardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource Not Found");
        }
        private async Task<Board> GetBoardByTaskId(int taskId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Tasks.Any(task => task.Id == taskId))), i => i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource Not Found");
        }
        #endregion

        public async Task AddTask(int cardId, TaskDto createTaskDto)
        {
            var board = await GetBoardByCardId(cardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Create));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var task = new Tasks(createTaskDto.Name)
            {
                CreatedAt = DateTime.UtcNow,
                CardId = cardId
            };
            await _tasksRepository.AddAsync(task);

        }
        public async Task<List<ResponseTaskDto>> GetCardTasks(int cardId)
        {
            var board = await GetBoardByCardId(cardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var tasks = await _tasksRepository.GetAllAsync(p => p.CardId == cardId);
            var tasksDto = _mapper.Map<List<ResponseTaskDto>>(tasks);
            return tasksDto;
        }
        public async Task<ResponseTaskDto> GetTask(int taskId)
        {
            var board = await GetBoardByTaskId(taskId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var tasks = await _tasksRepository.GetFirstAsync(p => p.Id == taskId);
            var tasksDto = _mapper.Map<ResponseTaskDto>(tasks);
            return tasksDto;
        }
        public async Task UpdateTask(int taskId, TaskDto taskDto)
        {
            var board = await GetBoardByTaskId(taskId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var task = await _tasksRepository.GetFirstAsync(p => p.Id == taskId);
            if (task == null)
                throw new ResourceNotFoundException("Resource Not Found");

            task.Name = taskDto.Name;
            await _tasksRepository.UpdateAsync(task);
        }
        public async Task DeleteTask(int taskId)
        {
            var board = await GetBoardByTaskId(taskId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Delete));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var task = await _tasksRepository.GetFirstAsync(p => p.Id == taskId);
            if (task == null)
                throw new ResourceNotFoundException("Resource Not Found");

            await _tasksRepository.DeleteAsync(task);
        }

    }
}
