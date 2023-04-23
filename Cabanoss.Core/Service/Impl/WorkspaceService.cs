using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Workspace;
using Cabanoss.Core.Repositories;

namespace Cabanoss.Core.Service.Impl
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceBaserepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userBase;

        public WorkspaceService(IWorkspaceRepository workspaceBaserepository, IMapper mapper, IUserRepository userBaseRepository)
        {
            _workspaceBaserepository = workspaceBaserepository;
            _mapper = mapper;
            _userBase = userBaseRepository;
        }
        #region Utils
        private async Task<User> GetUserById(int id)
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
        #endregion
        public async Task<WorkspaceDto> GetUserWorkspaceAsync(int id)
        {
            var workspace = await GetWorkspaceAsync(id);
            var workspaceDto = _mapper.Map<WorkspaceDto>(workspace);
            return workspaceDto;
        }
        public async Task<WorkspaceDto> UpdateWorkspaceAsync(int id, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = await GetWorkspaceAsync(id);
            workspace.Name = updateWorkspaceDto.Name;
            workspace.UpdatedAt = DateTime.Now;
            var updatedWorkspace = await _workspaceBaserepository.UpdateAsync(workspace);
            var updatedWorkspaceDto = _mapper.Map<WorkspaceDto>(updatedWorkspace);
            return updatedWorkspaceDto;
        }

        #region nieuzywane
        public async Task AddWorkspaceAsync(int id)
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
