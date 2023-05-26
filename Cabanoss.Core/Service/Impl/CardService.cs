﻿using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Card;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class CardService : ICardService
    {
        private ICardRepository _cardRepository;
        private IMapper _mapper;
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;
        private IUserRepository _userRepository;
        private ICardUserRepository _cardUserRepository;

        public CardService(
            ICardRepository cardRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService,
            IUserRepository userRepository,
            ICardUserRepository cardUserRepository)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _userRepository = userRepository;
            _cardUserRepository = cardUserRepository;
        }
        #region Utils
        private async Task<Card> GetCardById(int cardId)
        {
            var card = await _cardRepository.GetFirstAsync(p => p.Id == cardId, i=>i.CardUsers);
            if (card == null)
                throw new ResourceNotFoundException("ResourceNotFound");
            return card;
        }
        private async Task<Board> GetBoard(int cardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.BoardUsers.Any(card => card.CardUsers.Any(cardUser => cardUser.CardId == cardId)));
            if (board == null)
                throw new ResourceNotFoundException("Resource not found");
            return board;
        }
        private async System.Threading.Tasks.Task<AuthorizationResult> CheckBoardMembership(int cardId, ClaimsPrincipal user)
        {
            var board = GetBoard(cardId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new MembershipRequirements());
            return authorizationResult;
        }
        private async System.Threading.Tasks.Task CheckBoardMembership(int cardId, int userId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.BoardUsers.Any(card => card.CardUsers.Any(cardUser => cardUser.CardId == cardId)), c=>c.BoardUsers);
            var user = board.BoardUsers.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                throw new UnauthorizedException("the user you want to add does not belong to the table or does not exist");
        }
        private async Task<AuthorizationResult> CheckAdminRole(int BoardId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(i => i.Id == BoardId, i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new AdminRoleRequirements());
            return authorizationResult;
        }

        #endregion

        public async Task<List<CardDto>> GetCards(int listId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Id == listId), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

            var lists = await _cardRepository.GetAllAsync(p => p.ListId == listId);
            var listsDto = _mapper.Map<List<CardDto>>(lists);
            return listsDto;
        }

        public async Task<CardDto> GetCard(int cardId, ClaimsPrincipal claims)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(claims, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

            var card = await GetCardById(cardId);
            var cardDto = _mapper.Map<CardDto>(card);
            return cardDto;
        }

        public async Task AddCard(int listId, ClaimsPrincipal user, CreateCardDto createCard)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Id == listId), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await CheckAdminRole(board.Id, user);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

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
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await CheckAdminRole(board.Id, user);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

            var card = await GetCardById(cardId);
            await _cardRepository.DeleteAsync(card);
        }

        public async Task UpdateCard(int cardId, ClaimsPrincipal user, UpdateCardDto createCard)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await CheckAdminRole(board.Id, user);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

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

        public async Task SetDeadline(int cardId, DateOnly date, ClaimsPrincipal claims)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Id == cardId)), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await CheckAdminRole(board.Id, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

            var card = await GetCardById(cardId);

            card.Deadline = date.ToDateTime(new TimeOnly());
            await _cardRepository.UpdateAsync(card);
        }

        //For members
        public async Task<List<ResponseUserDto>> GetCardUsers(int cardId, ClaimsPrincipal claims)
        {
            await CheckBoardMembership(cardId, claims);
            var card = await GetCardById(cardId);

            var cardUsers = await _userRepository.GetAllAsync(b => b.BoardUsers.Any(c => c.CardUsers.Any(id => id.CardId == cardId)));
            if (cardUsers == null)
                throw new ResourceNotFoundException("Resource not Found");

            var usersDto = _mapper.Map<List<ResponseUserDto>>(cardUsers);
            return usersDto;

        }
        public async Task AddUserToCard(int cardId, int userId, ClaimsPrincipal claims)
        {
            var board = await GetBoard(cardId);
            var authorizationResult = await CheckAdminRole(board.Id, claims);
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            await CheckBoardMembership(cardId, userId);

            var userCard = new CardUser()
            {
                BoardUserId = userId,
                CardId = cardId
            };

            await _cardUserRepository.AddAsync(userCard);
        }
        public async Task DeleteUserFromCard(int cardId, int userId, ClaimsPrincipal claims)
        {
            var board = await GetBoard(cardId);
            var authorizationResult = await CheckAdminRole(board.Id, claims);
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            await CheckBoardMembership(cardId, userId);

            var card = await GetCardById(cardId);
            var cardUser = card.CardUsers.FirstOrDefault(id=>id.BoardUserId == userId);
            if (cardUser == null)
                throw new ResourceNotFoundException("the user you want to add does not belong to the card or does not exist");
            await _cardUserRepository.DeleteAsync(cardUser);
        }
    }
}
