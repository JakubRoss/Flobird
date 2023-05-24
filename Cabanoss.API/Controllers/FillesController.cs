using Cabanoss.Core.Service;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

namespace Cabanoss.API.Controllers
{
    [Route("files")]
    [ApiController]
    //[Authorize]
    public class FillesController : ControllerBase
    {
        private IFileService _fileService;

        public FillesController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpGet]
        public async Task<IActionResult> GetFile([FromQuery] string fileName)
        {
            var fileContents = await _fileService.GetFile(fileName, User);

            return File(fileContents.fileContents, fileContents.contentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            var id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(file != null && file.Length >0)
            {
                var rootPath = Directory.GetParent(Directory.GetCurrentDirectory());
                var fileName = file.FileName;
                var filePath = $"{rootPath}\\Cabanoss.Core\\Files\\{id}\\{fileName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}
