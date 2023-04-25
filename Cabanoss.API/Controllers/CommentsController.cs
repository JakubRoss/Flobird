using Cabanoss.Core.Model.Comment;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("comments")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private ICommentServices _commentServices;

        public CommentsController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpGet("cards")]
        public async Task<List<ResponseCommentDto>> GetComments([FromQuery] int cardId)
        {
            var comments = await _commentServices.GetComments(cardId, User);
            return comments;
        }

        [HttpGet]
        public async Task<ResponseCommentDto> GetComment([FromQuery] int commentId)
        {
            var comment = await _commentServices.GetComment(commentId, User);
            return comment;
        }

        [HttpPost("cards")]
        public async Task AddComment([FromQuery] int cardId, [FromBody] CommentDto commentDto)
        {
            await _commentServices.AddComment(cardId, commentDto.Text , User);
        }

        [HttpPut]
        public async Task UpdateComment([FromQuery] int commentId, [FromBody] CommentDto commentDto)
        {
            await _commentServices.UpdateComment(commentId , commentDto.Text , User);
        }

        [HttpDelete]
        public async Task DeleteCard([FromQuery] int commentID)
        {
            await _commentServices.DeleteComment(commentID, User);
        }
    }
}
