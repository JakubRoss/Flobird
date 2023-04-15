using Cabanoss.Core.Model.Workspace;

namespace Cabanoss.Core.Service
{
    public interface IWorkspaceService
    {
        Task<WorkspaceDto> GetUserWorkspaceAsync(int id);
        Task<WorkspaceDto> UpdateWorkspaceAsync(int id, UpdateWorkspaceDto updateWorkspaceDto);
        Task AddWorkspaceAsync(int id);
    }
}