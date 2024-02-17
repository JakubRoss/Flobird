using API.Swagger;
using Application.Model.Comment;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("comments")]
    [ApiController]
    [Authorize]
    [SwaggerControllerOrder(5)]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentServices _commentServices;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="commentServices"></param>
        public CommentsController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        /// <summary>
        /// adds commentary to a given card
        /// </summary>
        /// <param name="cardId">card id</param>
        /// <param name="commentDto">Request's payload</param>
        /// <remarks>
        /// POST flobird.azurewebsites.net/comments/cards?cardId={id}
        /// </remarks>
        [HttpPost("cards")]
        public async Task AddComment([FromQuery] int cardId, [FromBody] CommentDto commentDto)
        {
            await _commentServices.AddComment(cardId, commentDto.Text);
        }

        /// <summary>
        /// updates a given comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <param name="commentDto">Request's payload</param>
        /// <remarks>
        /// PUT flobird.azurewebsites.net/comments?commentId={id}
        /// </remarks>
        [HttpPut]
        public async Task UpdateComment([FromQuery] int commentId, [FromBody] CommentDto commentDto)
        {
            await _commentServices.UpdateComment(commentId, commentDto.Text);
        }

        /// <summary>
        /// downloads comments of a given card
        /// </summary>
        /// <param name="cardId">Card id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/comments/cards?cardId={id}
        /// </remarks>
        [HttpGet("cards")]
        public async Task<List<ResponseCommentDto>> GetComments([FromQuery] int cardId)
        {
            var comments = await _commentServices.GetComments(cardId);
            return comments;
        }

        /// <summary>
        /// downloads a given comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/comments?commentId={id}
        /// </remarks>
        [HttpGet]
        public async Task<ResponseCommentDto> GetComment([FromQuery] int commentId)
        {
            var comment = await _commentServices.GetComment(commentId);
            return comment;
        }

        /// <summary>
        /// deletes a given comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <remarks>
        /// DELETE flobird.azurewebsites.net/comments?commentId={id}
        /// </remarks>
        [HttpDelete]
        public async Task DeleteCard([FromQuery] int commentId)
        {
            await _commentServices.DeleteComment(commentId);
        }
    }
}
