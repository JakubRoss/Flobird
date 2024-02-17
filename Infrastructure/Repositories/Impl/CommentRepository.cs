using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
