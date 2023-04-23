using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Task;
using Cabanoss.Core.Repositories;
using Cabanoss.Core.Repositories.Impl;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _tasksRepository;
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;
        private IMapper _mapper;

        public TasksService(
            IMapper mapper,
            ITasksRepository tasksRepository,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService)
        {
            _tasksRepository = tasksRepository;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        #region Utils
        private async Task<Board> GetBoardByCardId(int cardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task<Board> GetBoardByTaskId(int taskId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Tasks.Any(task => task.Id == taskId))), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task CheckBoardMembership(Board board, ClaimsPrincipal user)
        {
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
        }

        private async Task<AuthorizationResult> ChceckAdminRole(Board board, ClaimsPrincipal user)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new AdminRoleRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");
            return authorizationResult;
        }
        #endregion
        public async Task AddTask(int cardId, TaskDto createTaskDto, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCardId(cardId);

            await ChceckAdminRole(board, claims);

            var task = new Tasks()
            {
                Name = createTaskDto.Name,
                CreatedAt = DateTime.UtcNow,
                CardId = cardId
            };
            await _tasksRepository.AddAsync(task);

        }
        public async Task<List<TaskDto>> GetCardTasks(int cardId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCardId(cardId);

            await CheckBoardMembership(board, claims);

            var tasks = await _tasksRepository.GetAllAsync(p => p.CardId == cardId);
            var tasksDto = _mapper.Map<List<TaskDto>>(tasks);
            return tasksDto;
        }
        public async Task<TaskDto> GetTask(int taskId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByTaskId(taskId);

            await CheckBoardMembership(board, claims);

            var tasks = await _tasksRepository.GetAllAsync(p => p.Id == taskId);
            var tasksDto = _mapper.Map<TaskDto>(tasks);
            return tasksDto;
        }
        public async Task UpdateTask(int taskId, TaskDto taskDto, ClaimsPrincipal claims)
        {
            var board = await GetBoardByTaskId(taskId);

            await CheckBoardMembership(board, claims);

            var task = await _tasksRepository.GetFirstAsync(p => p.Id == taskId);
            if (task == null)
                throw new ResourceNotFoundException("Resource Not Found");

            task.Name = taskDto.Name;
            await _tasksRepository.UpdateAsync(task);
        }
        public async Task DeleteTask(int taskId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByTaskId(taskId);

            await ChceckAdminRole(board, claims);

            var task = await _tasksRepository.GetFirstAsync(p => p.Id == taskId);
            if (task == null)
                throw new ResourceNotFoundException("Resource Not Found");

            await _tasksRepository.DeleteAsync(task);
        }

    }
}
