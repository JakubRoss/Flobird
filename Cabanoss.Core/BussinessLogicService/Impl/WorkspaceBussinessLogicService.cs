using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Model.Workspace;
using Cabanoss.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cabanoss.Core.BussinessLogicService.Impl
{
    public class WorkspaceBussinessLogicService : IWorkspaceBussinessLogicService
    {
        private readonly IWorkspaceBaserepository _workspaceBaserepository;
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;

        public WorkspaceBussinessLogicService(IWorkspaceBaserepository workspaceBaserepository, IMapper mapper, IUserBaseRepository userBaseRepository)
        {
            _workspaceBaserepository = workspaceBaserepository;
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
        }

        public async System.Threading.Tasks.Task AddWorkspaceAsync(string login)
        {
            var userWorkspaceDto = GetUserWorkspaceAsync(login).Result;
            if (userWorkspaceDto != null)
                return;
            var id = GetUserIdByLogin(login).Result;
            var workspaceDto = new WorkspaceDto
            {
                Name = $"{login} Workspace",
                CreatedAt = DateTime.Now,
                UserId = id,
            };
            var workspace = _mapper.Map<Workspace>(workspaceDto);
            await _workspaceBaserepository.AddAsync(workspace);
        }
        public async System.Threading.Tasks.Task<WorkspaceDto> GetUserWorkspaceAsync(string login)
        {
            var workspace = await GetWorkspaceAsync(login);
            var workspaceDto = _mapper.Map<WorkspaceDto>(workspace);
            return workspaceDto;
        }
        public async System.Threading.Tasks.Task<IEnumerable<WorkspaceDto>> GetUserWorkspacesAsync()
        {
            var workspaces = await _workspaceBaserepository.GetAllAsync();
            var workspacesDto = _mapper.Map<List<WorkspaceDto>>(workspaces);
            return workspacesDto;
        }
        public async System.Threading.Tasks.Task<WorkspaceDto> UpdateWorkspaceAsync(string login, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = GetWorkspaceAsync(login).Result;
            workspace.Name = updateWorkspaceDto.Name;
            workspace.UpdatedAt = DateTime.Now;
            var updatedWorkspace = await _workspaceBaserepository.UpdateAsync(workspace);
            var updatedWorkspaceDto = _mapper.Map<WorkspaceDto>(updatedWorkspace);
            return updatedWorkspaceDto;
        }
        private async Task<int> GetUserIdByLogin(string login)
        {
            var user = await _userBaseRepository.GetFirstAsync(l => l.Login == login);
            return user.Id;
        }
        private async Task<Workspace> GetWorkspaceAsync(string login)
        {
            var userId = GetUserIdByLogin(login).Result;
            var workspace = await _workspaceBaserepository.GetFirstAsync(id => id.UserId == userId);
            return workspace;
        }
    }
}
