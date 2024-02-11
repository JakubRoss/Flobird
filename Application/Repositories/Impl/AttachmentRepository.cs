using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
