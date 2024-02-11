using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
