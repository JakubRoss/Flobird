using Application.Authorization;
using Application.Model.Attachments;
using AutoMapper;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Application.Service.Impl
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpUserContextService _httpUserContextService;

        public AttachmentService(
            IAttachmentRepository attachmentRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService,
            IHttpUserContextService httpUserContextService)
        {
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _httpUserContextService = httpUserContextService;
        }

        #region Utils
        private async Task<Board> GetBoardByCardId(int cardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource Not Found");
        }
        private async Task<Board> GetBoardByAttachmentId(int attachmentId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(card => card.Cards.Any(comm => comm.Attachments.Any(id => id.Id == attachmentId))), i => i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource Not Found");
        }
        #endregion

        public async Task<List<AttachmentResponseDto>> GetAttachments(int cardId)
        {
            var board = await GetBoardByCardId(cardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var cardAttachments = await _attachmentRepository.GetAllAsync(p => p.CardId == cardId);
            var cardAttachmentsDto = _mapper.Map<List<AttachmentResponseDto>>(cardAttachments);
            return cardAttachmentsDto;

        }
        public async Task<AttachmentResponseDto> GetAttachment(int attachmentId)
        {
            var board = await GetBoardByAttachmentId(attachmentId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var attachment = _attachmentRepository.GetFirstAsync(p => p.Id == attachmentId);
            var attachmentDto = _mapper.Map<AttachmentResponseDto>(attachment);

            return attachmentDto;
        }
        public async Task AddAttachment(int cardId, AttachmentDto attachment)
        {
            var board = await GetBoardByCardId(cardId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var newAttachment = new Attachment
            {
                Path = attachment.Path,
                Name = attachment.Name,
                DateCreated = DateTime.UtcNow,
                CardId = cardId,
                UserId = (int)_httpUserContextService.UserId!
            };

            await _attachmentRepository.AddAsync(newAttachment);
        }
        public async Task UpdateAttachment(int attachmentId, AttachmentDto attachmentDto)
        {
            var board = await GetBoardByAttachmentId(attachmentId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            var attachment = await _attachmentRepository.GetFirstAsync(p => p.Id == attachmentId);

            if (_httpUserContextService.UserId == attachment.UserId || authorizationResult.Succeeded)
            {
                if (attachment.Path != null)
                    attachment.Path = attachmentDto.Path;
                if (attachment.Name != null)
                    attachment.Name = attachmentDto.Name;
                await _attachmentRepository.UpdateAsync(attachment);
            }
            else
                throw new UnauthorizedException("Unauthorized");
        }
        public async Task DeleteAttachment(int attachmentId)
        {
            var board = await GetBoardByAttachmentId(attachmentId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Create));

            var attachment = await _attachmentRepository.GetFirstAsync(p => p.Id == attachmentId);

            if (_httpUserContextService.UserId == attachment.UserId || authorizationResult.Succeeded)
                await _attachmentRepository.DeleteAsync(attachment);
            else
                throw new UnauthorizedException("Unauthorized");
        }
    }
}
