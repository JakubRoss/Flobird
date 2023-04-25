using Cabanoss.Core.Model.Attachments;
using Cabanoss.Core.Model.Element;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/elements")]
    [ApiController]
    [Authorize]
    public class ElementsController : ControllerBase
    {
        private IElementService _elementService;

        public ElementsController(IElementService elementService)
        {
            _elementService = elementService;
        }

        [HttpGet("tasks")]
        public async Task<List<ResponseElementDto>> GetElements([FromQuery] int taskId)
        {
            var elements = await _elementService.GetElements(taskId, User);
            return elements;
        }

        [HttpGet]
        public async Task<ResponseElementDto> GetElement([FromQuery] int elementId)
        {
            var element = await _elementService.GetElement(elementId, User);
            return element;
        }

        [HttpPost("tasks")]
        public async Task AddElement([FromQuery] int taskId, [FromBody] ElementDto elementDto)
        {
            await _elementService.AddElement(taskId, elementDto, User);
        }

        [HttpPut]
        public async Task UpdateElement([FromQuery] int elementId, [FromBody] UpdateElementDto updateElementDto)
        {
            await _elementService.UpdateElement(elementId, updateElementDto, User);
        }

        [HttpDelete]
        public async Task DeleteElement([FromQuery] int elementId)
        {
            await _elementService.DeleteElement(elementId, User);
        }

        [HttpPatch]
        public async Task CheckElement([FromQuery] int elementId, UpdateElementDto updateElement)
        {
            await _elementService.CheckElement(elementId, updateElement, User);
        }
    }
}
