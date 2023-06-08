using Cabanoss.API.Swagger;
using Cabanoss.Core.Common;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("files")]
    [ApiController]
    [Authorize]
    [SwaggerControllerOrder(9)]
    public class FillesController : ControllerBase
    {
        private IFileService _fileService;
        private readonly AzureProps _azureProps;

        public FillesController(IFileService fileService, AzureProps azureProps)
        {
            _fileService = fileService;
            _azureProps = azureProps;
        }

        /// <summary>
        /// downloads last uploaded image
        /// </summary>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/files
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetFile()
        {
            var fileContents = await _fileService.GetFile(User, _azureProps);

            return File(fileContents.fileContents, fileContents.contentType, fileContents.fileName);
        }

        /// <summary>
        /// uploads files like "jpeg", "jpg", "png" up to 1MB (eventually avatar support)
        /// </summary>
        /// <param name="file">Request's payload</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/files
        /// </remarks>
        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            await _fileService.UploadFile(_azureProps,User, file);
        }
    }
}
