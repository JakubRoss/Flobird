using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Attachments;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class AttachmentService : IAttachmentService
    {
        private IAttachmentRepository _attachmentRepository;
        private IMapper _mapper;
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;

        public AttachmentService(
            IAttachmentRepository attachmentRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService)
        {
            _attachmentRepository = attachmentRepository;
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
        private async Task<Board> GetBoardByAttachmentId(int attachmentId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(card => card.Cards.Any(comm => comm.Attachments.Any(id => id.Id == attachmentId))), i => i.BoardUsers);
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
        public async Task<List<AttachmentResponseDto>> GetAttachments(int cardId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCardId(cardId);
            await CheckBoardMembership(board, claims);

            var cardAttachments = await _attachmentRepository.GetAllAsync(p => p.CardId == cardId);
            var cardAttachmentsDto = _mapper.Map<List<AttachmentResponseDto>>(cardAttachments);
            return cardAttachmentsDto;

        }
        public async Task<AttachmentResponseDto> GetAttachment(int attachmentId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByAttachmentId(attachmentId);
            await CheckBoardMembership(board, claims);

            var attachment = _attachmentRepository.GetFirstAsync(p => p.Id == attachmentId);
            var attachmentDto = _mapper.Map<AttachmentResponseDto>(attachment);

            return attachmentDto;
        }
        public async Task AddAttachment(int cardId, AttachmentDto attachment, ClaimsPrincipal claims)
        {
            var board = await GetBoardByCardId(cardId);
            await CheckBoardMembership(board, claims);

            var userId = int.Parse(claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var newAttachment = new Attachment()
            {
                Name = attachment.Name,
                Path = attachment.Path,
                DateCreated = DateTime.UtcNow,
                CardId = cardId,
                UserId = userId
            };

            await _attachmentRepository.AddAsync(newAttachment);
        }
        public async Task UpdateAttachment(int attachmentId, AttachmentDto attachment, ClaimsPrincipal claims)
        {
            var board = await GetBoardByAttachmentId(attachmentId);

            await CheckBoardMembership(board, claims);
            var adminAuthorization = await CheckAdminRole(board, claims);

            var attachmentUpdate = await _attachmentRepository.GetFirstAsync(p => p.Id == attachmentId);

            var userId = int.Parse(claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == attachmentUpdate.UserId || adminAuthorization.Succeeded)
            {
                if (attachmentUpdate.Path != null)
                    attachmentUpdate.Path = attachment.Path;
                if (attachmentUpdate.Name != null)
                    attachmentUpdate.Name = attachmentUpdate.Name;
                await _attachmentRepository.UpdateAsync(attachmentUpdate);
            }
            else
                throw new ResourceNotFoundException("No Access");
        }
        public async Task DeleteAttachment(int attachmentId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByAttachmentId(attachmentId);

            await CheckBoardMembership(board, claims);
            var adminAuthorization = await CheckAdminRole(board, claims);

            var attachment = await _attachmentRepository.GetFirstAsync(p => p.Id == attachmentId);

            var userId = int.Parse(claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == attachment.UserId || adminAuthorization.Succeeded)
                await _attachmentRepository.DeleteAsync(attachment);
            else
                throw new ResourceNotFoundException("No Access");
        }
    }
}
