using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class WorkspaceRepository : BaseRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(DatabaseContext context) : base(context) { }
    }
}
