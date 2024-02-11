using Application.Model.Attachments;

namespace Application.Service
{
    public interface IAttachmentService
    {
        Task AddAttachment(int cardId, AttachmentDto attachment);
        Task DeleteAttachment(int attachmentId);
        Task<AttachmentResponseDto> GetAttachment(int attachmentId);
        Task<List<AttachmentResponseDto>> GetAttachments(int cardId);
        Task UpdateAttachment(int attachmentId, AttachmentDto attachment);
    }
}