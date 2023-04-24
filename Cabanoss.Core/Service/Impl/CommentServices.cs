using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Comment;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class CommentServices : ICommentServices
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;

        public CommentServices(
            ICommentRepository commentRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
        }

        #region Utils
        private async Task<Board> GetBoardByCardId(int cardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task<Board> GetBoardByCommentId(int commentId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(card => card.Cards.Any(comm => comm.Comments.Any(id => id.Id == commentId))), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task CheckBoardMembership(Board board, ClaimsPrincipal user)
        {
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
        }

        private async Task<AuthorizationResult> CheckAdminRole(Board board, ClaimsPrincipal user)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new AdminRoleRequirements());
            return authorizationResult;
        }
        #endregion
        public async Task<List<ResponseCommentDto>> GetComments(int cardId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCardId(cardId);
            await CheckBoardMembership(board, claims);

            var cardComments = await _commentRepository.GetAllAsync(p => p.CardId == cardId);
            var cardDtoComments = _mapper.Map<List<ResponseCommentDto>>(cardComments);
            return cardDtoComments;

        }
        public async Task<ResponseCommentDto> GetComment(int commentId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCommentId(commentId);
            await CheckBoardMembership(board, claims);

            var comment = _commentRepository.GetFirstAsync(p => p.Id == commentId);
            var commentDto = _mapper.Map<ResponseCommentDto>(comment);

            return commentDto;
        }
        public async Task AddComment(int cardId, string text, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCardId(cardId);
            await CheckBoardMembership(board, claims);

            var userId = int.Parse(claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var comment = new Comment()
            {
                Text = text,
                CreatedAt = DateTime.Now,
                UserId = userId,
                CardId = cardId
            };

            await _commentRepository.AddAsync(comment);
        }
        public async Task UpdateComment(int commentId, string text, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCommentId(commentId);

            await CheckBoardMembership(board, claims);
            var adminAuthorization = await CheckAdminRole(board, claims);

            var comment = await _commentRepository.GetFirstAsync(p => p.Id == commentId);

            var userId = int.Parse(claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == comment.UserId || adminAuthorization.Succeeded)
            {
                comment.Text = text;
                await _commentRepository.UpdateAsync(comment);
            }
            else
                throw new ResourceNotFoundException("No Access");
        }
        public async Task DeleteComment(int commentId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCommentId(commentId);

            await CheckBoardMembership(board, claims);
            var adminAuthorization = await CheckAdminRole(board, claims);

            var comment = await _commentRepository.GetFirstAsync(p => p.Id == commentId);

            var userId = int.Parse(claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == comment.UserId || adminAuthorization.Succeeded)
                await _commentRepository.DeleteAsync(comment);
            else
                throw new ResourceNotFoundException("No Access");
        }
    }
}
