using Cabanoss.Core.Model.Card;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("lists")]
        public async Task<List<CardDto>> GetCards([FromQuery] int listId)
        {
            var cards = await _cardService.GetCards(listId, User);
            return cards;
        }

        [HttpGet]
        public async Task<CardDto> GetCard([FromQuery] int cardId)
        {
            var card = await _cardService.GetCard(cardId, User);
            return card;
        }

        [HttpPost("lists")]
        public async Task AddCard([FromQuery] int listId , [FromBody] CreateCardDto createCard)
        {
            await _cardService.AddCard(listId,User, createCard);
        }

        [HttpPut]
        public async Task UpdateCard([FromQuery] int cardId,[FromBody] UpdateCardDto createCard)
        {
            await _cardService.UpdateCard(cardId, User, createCard);
        }

        [HttpDelete]
        public async Task DeleteCard([FromQuery] int cardId)
        {
            await _cardService.DeleteCard(cardId, User);
        }
    }
}
