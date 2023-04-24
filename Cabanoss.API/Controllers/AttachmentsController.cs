using Cabanoss.Core.Model.Attachments;
using Cabanoss.Core.Model.Comment;
using Cabanoss.Core.Service;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/attachments")]
    [ApiController]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }
        [HttpGet("cards")]
        public async Task<List<AttachmentResponseDto>> GetAttachments([FromQuery] int cardId)
        {
            var attachments = await _attachmentService.GetAttachments(cardId, User);
            return attachments;
        }

        [HttpGet]
        public async Task<AttachmentResponseDto> GetAttachment([FromQuery] int attachmentId)
        {
            var attachment = await _attachmentService.GetAttachment(attachmentId, User);
            return attachment;
        }

        [HttpPost("cards")]
        public async Task AddAttachment([FromQuery] int cardId, [FromBody] AttachmentDto attachmentDto)
        {
            await _attachmentService.AddAttachment(cardId, attachmentDto, User);
        }

        [HttpPut]
        public async Task UpdateAttachment([FromQuery] int attachmentId, [FromBody] AttachmentDto attachmentDto)
        {
            await _attachmentService.UpdateAttachment(attachmentId, attachmentDto, User);
        }

        [HttpDelete]
        public async Task DeleteAttachment([FromQuery] int attachmentId)
        {
            await _attachmentService.DeleteAttachment(attachmentId, User);
        }
    }
}
