using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class WorkspaceBaserepository : BaseRepository<Workspace>, IWorkspaceBaserepository
    {
        public WorkspaceBaserepository(CabanossDbContext context) : base(context) { }
    }
}
