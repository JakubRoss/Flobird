using Cabanoss.Core.Model.Element;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("elements")]
    [ApiController]
    [Authorize]
    public class ElementsController : ControllerBase
    {
        private IElementService _elementService;

        public ElementsController(IElementService elementService)
        {
            _elementService = elementService;
        }

        /// <summary>
        /// downloads the elements of a given task 
        /// </summary>
        /// <param name="taskId">task id</param>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/elements?taskId={id}
        /// </remarks>
        [HttpGet("tasks")]
        public async Task<List<ResponseElementDto>> GetElements([FromQuery] int taskId)
        {
            var elements = await _elementService.GetElements(taskId, User);
            return elements;
        }

        /// <summary>
        /// downloads the indicated task element
        /// </summary>
        /// <param name="elementId">element id</param>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/elements?elementId={id}
        /// </remarks>
        [HttpGet]
        public async Task<ResponseElementDto> GetElement([FromQuery] int elementId)
        {
            var element = await _elementService.GetElement(elementId, User);
            return element;
        }

        /// <summary>
        /// adds a new element to the task
        /// </summary>
        /// <param name="elementDto">Request's payload</param>
        /// <param name="taskId">task id</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/elements/tasks?taskId={id}
        /// </remarks>
        [HttpPost("tasks")]
        public async Task AddElement([FromQuery] int taskId, [FromBody] ElementDto elementDto)
        {
            await _elementService.AddElement(taskId, elementDto, User);
        }

        /// <summary>
        /// updates a given task element 
        /// </summary>
        /// <param name="updateElementDto">Request's payload</param>
        /// <param name="elementId">element id</param>
        /// <remarks>
        /// PUT cabanoss.azurewebsites.net/elements?elementId={id}
        /// </remarks>
        [HttpPut]
        public async Task UpdateElement([FromQuery] int elementId, [FromBody] UpdateElementDto updateElementDto)
        {
            await _elementService.UpdateElement(elementId, updateElementDto, User);
        }

        /// <summary>
        /// removes a given task element
        /// </summary>
        /// <param name="elementId">element id</param>
        /// <remarks>
        /// DELETE cabanoss.azurewebsites.net/elements?elementId={id}
        /// </remarks>
        [HttpDelete]
        public async Task DeleteElement([FromQuery] int elementId)
        {
            await _elementService.DeleteElement(elementId, User);
        }

        /// <summary>
        /// checks the "checkbox" (true or false)
        /// </summary>
        /// <param name="elementId">element id</param>
        /// <remarks>
        /// PATCH cabanoss.azurewebsites.net/elements?elementId={id}
        /// </remarks>
        [HttpPatch]
        public async Task CheckElement([FromQuery] int elementId, UpdateElementDto updateElement)
        {
            await _elementService.CheckElement(elementId, updateElement, User);
        }
    }
}
