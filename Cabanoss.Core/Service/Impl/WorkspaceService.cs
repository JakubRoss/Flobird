using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Workspace;
using Cabanoss.Core.Repositories;

namespace Cabanoss.Core.Service.Impl
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceBaserepository _workspaceBaserepository;
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBase;

        public WorkspaceService(IWorkspaceBaserepository workspaceBaserepository, IMapper mapper, IUserBaseRepository userBaseRepository)
        {
            _workspaceBaserepository = workspaceBaserepository;
            _mapper = mapper;
            _userBase = userBaseRepository;
        }
        private async System.Threading.Tasks.Task<User> GetUserById(int id)
        {
            var user = await _userBase.GetFirstAsync(p => p.Id == id);
            if (user == null)
                throw new ResourceNotFoundException("User don't exists");
            return user;
        }
        private async Task<Workspace> GetWorkspaceAsync(int id)
        {
            var user = await GetUserById(id);
            var workspace = await _workspaceBaserepository.GetFirstAsync(id => id.UserId == user.Id);
            if (workspace == null)
                throw new ResourceNotFoundException("Uzytkownik nie posiada przestrzeni roboczej");
            return workspace;
        }

        public async System.Threading.Tasks.Task<WorkspaceDto> GetUserWorkspaceAsync(int id)
        {
            var workspace = await GetWorkspaceAsync(id);
            var workspaceDto = _mapper.Map<WorkspaceDto>(workspace);
            return workspaceDto;
        }
        public async System.Threading.Tasks.Task<WorkspaceDto> UpdateWorkspaceAsync(int id, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = await GetWorkspaceAsync(id);
            workspace.Name = updateWorkspaceDto.Name;
            workspace.UpdatedAt = DateTime.Now;
            var updatedWorkspace = await _workspaceBaserepository.UpdateAsync(workspace);
            var updatedWorkspaceDto = _mapper.Map<WorkspaceDto>(updatedWorkspace);
            return updatedWorkspaceDto;
        }

        #region nieuzywane
        public async System.Threading.Tasks.Task AddWorkspaceAsync(int id)
        {
            var user = await GetUserById(id);
            var workspaceDto = new WorkspaceDto
            {
                Name = $"{user.Login} Workspace",
                CreatedAt = DateTime.Now,
                UserId = user.Id,
            };
            var workspace = _mapper.Map<Workspace>(workspaceDto);
            await _workspaceBaserepository.AddAsync(workspace);
        }
        #endregion
    }
}
