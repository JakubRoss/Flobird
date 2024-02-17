using API.Swagger;
using Application.Model.Card;
using Application.Model.User;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [SwaggerControllerOrder(4)]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="cardService"></param>
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Adding card to a given list
        /// </summary>
        /// <param name="listId">list Id</param>
        /// <param name="createCard">Request's payload</param>
        /// <param name="cardId">We use this only when we want to move an already existing card to another list</param>
        /// <remarks>
        /// POST flobird.azurewebsites.net/cards/lists?listId={id} or transfer to another card POST flobird.azurewebsites.net/cards/lists?listId={id}?cardId={id}
        /// </remarks>
        [HttpPost("cards/lists")]
        public async Task AddCard([FromQuery] int listId,[FromQuery] int? cardId, [FromBody] CreateCardDto createCard)
        {
            await _cardService.AddCard(listId, createCard, cardId);
        }

        /// <summary>
        /// Updates a specified card
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <param name="createCard">Request's payload</param>
        /// <remarks>
        /// PUT flobird.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        [HttpPut("cards")]
        public async Task UpdateCard([FromQuery] int cardId, [FromBody] UpdateCardDto createCard)
        {
            await _cardService.UpdateCard(cardId, createCard);
        }

        /// <summary>
        /// sets card deadline
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <param name="date">short date to set card deadline</param>
        /// <remarks>
        /// PATCH flobird.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        [HttpPatch("cards")]
        public async Task SetDeadline([FromQuery] int cardId, [FromBody] DateOnly date)
        {
            await _cardService.SetDeadline(cardId, date);
        }

        /// <summary>
        /// Downloads a list of cards for a given list
        /// </summary>
        /// <param name="listId">list Id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/cards/lists?listId={id}
        /// </remarks>
        [HttpGet("cards/lists")]
        public async Task<List<CardDto>> GetCards([FromQuery] int listId)
        {
            var cards = await _cardService.GetCards(listId);
            return cards;
        }

        /// <summary>
        /// Downloads the card
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        /// <returns>Created card</returns>
        [HttpGet("cards")]
        public async Task<CardDto> GetCard([FromQuery] int cardId)
        {
            var card = await _cardService.GetCard(cardId);
            return card;
        }

        /// <summary>
        /// Deletes a specified card
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <remarks>
        /// DELETE flobird.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        [HttpDelete("cards")]
        public async Task DeleteCard([FromQuery] int cardId)
        {
            await _cardService.DeleteCard(cardId);
        }

        /// <summary>
        /// downloads users of a certain card
        /// </summary>
        /// <param name="cardId">card id</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/members/cards?cardId={id}
        /// </remarks>
        [HttpGet("members/cards")]
        public async Task<List<ResponseUserDto>> GetCardUsers([FromQuery] int cardId)
        {
            var users = await _cardService.GetCardUsers(cardId);
            return users;
        }

        /// <summary>
        /// adds the specified user to the given card
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="cardId">card to which we want to add a user</param>
        /// <remarks>
        /// POST flobird.azurewebsites.net/members/cards/{cardId}?userId={userId}
        /// </remarks>
        [HttpPost("members/cards/{cardId}")]
        public async Task AddCardUsers([FromRoute] int cardId, [FromQuery] int userId)
        {
            await _cardService.AddUserToCard(cardId, userId);
        }

        /// <summary>
        /// removes the specified user to the given card
        /// </summary>
        /// <param name="cardId">user id</param>
        /// <param name="userId">the user you want to remove from a given card</param>
        /// <remarks>
        /// DELETE flobird.azurewebsites.net/members/cards/{cardId}?userId={userId}
        /// </remarks>
        [HttpDelete("members/cards/{cardId}")]
        public async Task RemoveCardUsers([FromRoute] int cardId, [FromQuery] int userId)
        {
            await _cardService.DeleteUserFromCard(cardId, userId);
        }
    }
}
