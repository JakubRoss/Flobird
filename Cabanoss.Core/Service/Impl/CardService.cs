using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Card;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class CardService : ICardService
    {
        private ICardRepository _cardRepository;
        private IMapper _mapper;
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;

        public CardService(
            ICardRepository cardRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
        }
        #region Utils
        private async Task CheckMembershipByCardId(int cardId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new BelongToRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

        }
        private async Task CheckMembershipByListId(int listId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Id == listId), i => i.BoardUsers);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new BelongToRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
        }
        private async Task<Card> GetCardById(int cardId)
        {
            var card = await _cardRepository.GetFirstAsync(p => p.Id == cardId);
            if (card == null)
                throw new ResourceNotFoundException("ResourceNotFound");
            return card;
        }

        #endregion

        public async Task<List<CardDto>> GetCards(int listId, ClaimsPrincipal user)
        {
            await CheckMembershipByListId(listId, user);

            var lists = await _cardRepository.GetAllAsync(p => p.ListId == listId);
            var listsDto = _mapper.Map<List<CardDto>>(lists);
            return listsDto;
        }

        public async Task<CardDto> GetCard(int cardId, ClaimsPrincipal claims)
        {
            await CheckMembershipByCardId(cardId, claims);

            var card = await GetCardById(cardId);
            var cardDto = _mapper.Map<CardDto>(card);
            return cardDto;
        }

        public async Task AddCard(int listId, ClaimsPrincipal user, CreateCardDto createCard)
        {
            await CheckMembershipByListId(listId, user);

            Card card = new Card
            {
                Name = createCard.Name,
                Description = createCard.Description,
                CreatedAt = DateTime.UtcNow,
                ListId = listId
            };
            await _cardRepository.AddAsync(card);
        }

        public async Task DeleteCard(int cardId, ClaimsPrincipal user)
        {
            await CheckMembershipByCardId(cardId, user);
            var card = await GetCardById(cardId);
            await _cardRepository.DeleteAsync(card);
        }

        public async Task UpdateCard(int cardId, ClaimsPrincipal user, UpdateCardDto createCard)
        {
            await CheckMembershipByCardId(cardId, user);

            var card = await GetCardById(cardId);

            #region updt_properties
            if (createCard.Name != null)
                card.Name = createCard.Name;
            if (createCard.Description != null)
                card.Description = createCard.Description;
            #endregion
            card.UpdateAt = DateTime.UtcNow;
            await _cardRepository.UpdateAsync(card);
        }

        //TUTAJ NIE MA SPRAWDZANIA ROLI UZYTKOWNIKA (DO ZROBIENIA)
    }
}
