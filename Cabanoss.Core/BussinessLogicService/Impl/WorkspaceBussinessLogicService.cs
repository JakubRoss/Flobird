using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Workspace;
using Cabanoss.Core.Repositories;

namespace Cabanoss.Core.BussinessLogicService.Impl
{
    public class WorkspaceBussinessLogicService : IWorkspaceBussinessLogicService
    {
        private readonly IWorkspaceBaserepository _workspaceBaserepository;
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBase;

        public WorkspaceBussinessLogicService(IWorkspaceBaserepository workspaceBaserepository, IMapper mapper, IUserBaseRepository userBaseRepository)
        {
            _workspaceBaserepository = workspaceBaserepository;
            _mapper = mapper;
            _userBase = userBaseRepository;
        }
        private async System.Threading.Tasks.Task<User> GetUser(string login)
        {
            var user = await _userBase.GetFirstAsync(u => u.Login.ToLower() == login.ToLower());
            if (user == null)
                throw new ResourceNotFoundException("Uzytkownik nie istnieje");
            return user;
        }
        private async Task<Workspace> GetWorkspaceAsync(string login)
        {
            var user = await GetUser(login);
            var workspace = await _workspaceBaserepository.GetFirstAsync(id => id.UserId == user.Id);
            if (workspace == null)
                throw new ResourceNotFoundException("Uzytkownik nie posiada przestrzeni roboczej");
            return workspace;
        }

        public async System.Threading.Tasks.Task<WorkspaceDto> GetUserWorkspaceAsync(string login)
        {
            var workspace = await GetWorkspaceAsync(login);
            var workspaceDto = _mapper.Map<WorkspaceDto>(workspace);
            return workspaceDto;
        }
        public async System.Threading.Tasks.Task<WorkspaceDto> UpdateWorkspaceAsync(string login, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = await GetWorkspaceAsync(login);
            workspace.Name = updateWorkspaceDto.Name;
            workspace.UpdatedAt = DateTime.Now;
            var updatedWorkspace = await _workspaceBaserepository.UpdateAsync(workspace);
            var updatedWorkspaceDto = _mapper.Map<WorkspaceDto>(updatedWorkspace);
            return updatedWorkspaceDto;
        }

        #region nieuzywane
        public async System.Threading.Tasks.Task AddWorkspaceAsync(string login)
        {
            var user = await GetUser(login);
            var workspaceDto = new WorkspaceDto
            {
                Name = $"{login} Workspace",
                CreatedAt = DateTime.Now,
                UserId = user.Id,
            };
            var workspace = _mapper.Map<Workspace>(workspaceDto);
            await _workspaceBaserepository.AddAsync(workspace);
        }
        //public async System.Threading.Tasks.Task<IEnumerable<WorkspaceDto>> GetUsersWorkspacesAsync()
        //{
        //    var workspaces = await _workspaceBaserepository.GetAllAsync();
        //    var workspacesDto = _mapper.Map<List<WorkspaceDto>>(workspaces);
        //    return workspacesDto;
        //}
        #endregion
    }
}
