using Cabanoss.Core.Model.Workspace;

namespace Cabanoss.Core.BussinessLogicService
{
    public interface IWorkspaceBussinessLogicService
    {
        Task<WorkspaceDto> GetUserWorkspaceAsync(string login);
        Task<WorkspaceDto> UpdateWorkspaceAsync(string login, UpdateWorkspaceDto updateWorkspaceDto);
        Task AddWorkspaceAsync(string login);

        //Task<IEnumerable<WorkspaceDto>> GetUsersWorkspacesAsync();
    }
}