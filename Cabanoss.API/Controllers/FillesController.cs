using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("files")]
    [ApiController]
    [Authorize]
    public class FillesController : ControllerBase
    {
        private IFileService _fileService;

        public FillesController(IFileService fileService)
        {
            _fileService = fileService;
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
            var fileContents = await _fileService.GetFile(User);

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
            await _fileService.UploadFile(User, file);
        }
    }
}
