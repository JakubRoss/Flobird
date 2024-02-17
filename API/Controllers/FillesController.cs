using API.Swagger;
using Application.Common;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("files")]
    [ApiController]
    [Authorize]
    [SwaggerControllerOrder(9)]
    public class FillesController : ControllerBase
    {
        private readonly IFileService _fileService;
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
        /// GET flobird.azurewebsites.net/files
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetFile()
        {
            var fileContents = await _fileService.GetFile(_azureProps);

            return File(fileContents.FileContents, fileContents.ContentType, fileContents.FileName);
        }

        /// <summary>
        /// uploads files like "jpeg", "jpg", "png" up to 1MB (avatar support)
        /// </summary>
        /// <param name="file">Request's payload</param>
        /// <remarks>
        /// POST flobird.azurewebsites.net/files
        /// </remarks>
        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            await _fileService.UploadFile(_azureProps,file);
        }
    }
}
