using Cabanoss.Core.Model.Workspace;

namespace Cabanoss.Core.BussinessLogicService
{
    public interface IWorkspaceBussinessLogicService
    {
        Task AddWorkspaceAsync(string login);
        Task<WorkspaceDto> GetUserWorkspaceAsync(string login);
        Task<IEnumerable<WorkspaceDto>> GetUserWorkspacesAsync();
        Task<WorkspaceDto> UpdateWorkspaceAsync(string login, UpdateWorkspaceDto updateWorkspaceDto);
    }
}