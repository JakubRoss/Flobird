using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        public BoardRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
