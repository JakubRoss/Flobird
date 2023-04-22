using Cabanoss.Core.Model.Card;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface ICardService
    {
        Task AddCard(int listId, ClaimsPrincipal user, CreateCardDto createCard);
        Task DeleteCard(int cardId, ClaimsPrincipal user);
        Task<CardDto> GetCard(int cardId, ClaimsPrincipal claims);
        Task<List<CardDto>> GetCards(int listId, ClaimsPrincipal user);
        Task UpdateCard(int cardId, ClaimsPrincipal user, UpdateCardDto createCard);
    }
}