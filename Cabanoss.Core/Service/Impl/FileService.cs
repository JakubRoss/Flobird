using Cabanoss.Core.Exceptions;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class FileContResult
    {
        public byte[] fileContents { get; set; }
        public string contentType { get; set; }

        public FileContResult(byte[] fileContents, string contentType)
        {
            this.fileContents = fileContents;
            this.contentType = contentType;
        }

    }
    public class FileService : IFileService
    {
        public async Task<FileContResult> GetFile(string fileName, ClaimsPrincipal claimsPrincipal)
        {
            var id = claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var rootPath = Directory.GetParent(Directory.GetCurrentDirectory());
            var filePath = $"{rootPath}\\Cabanoss.Core\\Files\\{id}\\{fileName}";

            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                throw new ResourceNotFoundException("file doesn't exist");
            }

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(filePath, out var contentType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);

            return new FileContResult(fileContents, contentType);
        }

        public async Task UploadFile()
        {
        }
    }
}
