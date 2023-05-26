using Cabanoss.Core.Model.Card;
using Cabanoss.Core.Service;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("cards")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Downloads a list of cards for a given list
        /// </summary>
        /// <param name="listId">list Id</param>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/cards/lists?listId={id}
        /// </remarks>
        [HttpGet("lists")]
        public async Task<List<CardDto>> GetCards([FromQuery] int listId)
        {
            var cards = await _cardService.GetCards(listId, User);
            return cards;
        }

        /// <summary>
        /// Downloads the card
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        /// <returns>Created card</returns>
        [HttpGet]
        public async Task<CardDto> GetCard([FromQuery] int cardId)
        {
            var card = await _cardService.GetCard(cardId, User);
            return card;
        }

        /// <summary>
        /// Adding card to a given list
        /// </summary>
        /// <param name="listId">list Id</param>
        /// <param name="createCard">Request's payload</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/cards/lists?listId={id}
        /// </remarks>
        [HttpPost("lists")]
        public async Task AddCard([FromQuery] int listId , [FromBody] CreateCardDto createCard)
        {
            await _cardService.AddCard(listId,User, createCard);
        }

        /// <summary>
        /// Updates a specified card
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <param name="createCard">Request's payload</param>
        /// <remarks>
        /// PUT cabanoss.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        [HttpPut]
        public async Task UpdateCard([FromQuery] int cardId,[FromBody] UpdateCardDto createCard)
        {
            await _cardService.UpdateCard(cardId, User, createCard);
        }

        /// <summary>
        /// sets card deadline
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <param name="date">short date to set card deadline</param>
        /// <remarks>
        /// PATCH cabanoss.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        [HttpPatch]
        public async Task SetDeadline([FromQuery] int cardId, [FromBody] DateOnly date)
        {
            await _cardService.SetDeadline(cardId, date, User);
        }
        /// <summary>
        /// Deletes a specified card
        /// </summary>
        /// <param name="cardId">card Id</param>
        /// <remarks>
        /// DELETE cabanoss.azurewebsites.net/cards?cardId={id}
        /// </remarks>
        [HttpDelete]
        public async Task DeleteCard([FromQuery] int cardId)
        {
            await _cardService.DeleteCard(cardId, User);
        }
    }
}
