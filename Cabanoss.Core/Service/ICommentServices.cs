using Cabanoss.Core.Model.Comment;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface ICommentServices
    {
        Task AddComment(int cardId, string text, ClaimsPrincipal claims);
        Task DeleteComment(int commentId, ClaimsPrincipal claims);
        Task<ResponseCommentDto> GetComment(int commentId, ClaimsPrincipal claims);
        Task<List<ResponseCommentDto>> GetComments(int cardId, ClaimsPrincipal claims);
        Task UpdateComment(int commentId, string text, ClaimsPrincipal claims);
    }
}