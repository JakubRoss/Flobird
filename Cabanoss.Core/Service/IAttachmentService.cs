using Cabanoss.Core.Model.Attachments;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IAttachmentService
    {
        Task AddAttachment(int cardId, AttachmentDto attachment, ClaimsPrincipal claims);
        Task DeleteAttachment(int attachmentId, ClaimsPrincipal claims);
        Task<AttachmentResponseDto> GetAttachment(int attachmentId, ClaimsPrincipal claims);
        Task<List<AttachmentResponseDto>> GetAttachments(int cardId, ClaimsPrincipal claims);
        Task UpdateAttachment(int attachmentId, AttachmentDto attachment, ClaimsPrincipal claims);
    }
}