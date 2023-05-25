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

        [HttpGet]
        public async Task<IActionResult> GetFile()
        {
            var fileContents = await _fileService.GetFile(User);

            return File(fileContents.fileContents, fileContents.contentType, fileContents.fileName);
        }

        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            await _fileService.UploadFile(User, file);
        }
    }
}
