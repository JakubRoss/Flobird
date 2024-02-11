using Application.Data.Entities;
using Application.Model.Workspace;

namespace Application.Service
{
    public interface IWorkspaceService
    {
        Task<WorkspaceDto> GetUserWorkspaceAsync();
        Task<WorkspaceDto> UpdateWorkspaceAsync(UpdateWorkspaceDto updateWorkspaceDto);
        Task AddWorkspaceAsync(User user);
    }
}