using Application.Common;
using Application.Exceptions;
using Application.Repositories;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Impl
{
    public class FileContResult
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public FileContResult(byte[] fileContents, string contentType, string fileName)
        {
            this.FileContents = fileContents;
            this.ContentType = contentType;
            this.FileName = fileName;
        }
    }

    public class FileService : IFileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpUserContextService _httpUserContextService;

        public FileService(
            IUserRepository userRepository,
            IHttpUserContextService httpUserContextService)
        {
            _userRepository = userRepository;
            _httpUserContextService = httpUserContextService;

        }
        #region Utils
        private bool GetFileExtension(IFormFile file, out string ext)
        {
            string[] allowedExtensions = {".jpeg",".jpg",".png"}; 
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                ext = extension;
                return false;
            }
            ext = extension;
            return true;
        }
        public async Task<BlobClient?> FindFile(string fileName, AzureProps azureProps)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(azureProps.AzureStorageConnection);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(azureProps.ContainerName);

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                if (blobItem.Name.Contains(fileName))
                {
                    return containerClient.GetBlobClient(blobItem.Name);
                }
            }

            return null;
        }
        private string GetContentType(string fileName)
        {
            var fileExtension = $".{fileName.Split('.').Last()}";
            if (fileExtension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                fileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/jpeg";
            }
            else if (fileExtension.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }
            else
            {
                // Domyślny typ zawartości dla innych plików
                return "application/octet-stream";
            }
        }
        #endregion

        public async Task<FileContResult> GetFile(AzureProps azureProps)
        {
            var id = _httpUserContextService.UserId;
            var login = _httpUserContextService.UserLogin;
            var name = $"{id}_{login}AV";

            var blobClient = await FindFile(name,azureProps);
            if (blobClient is null)
            {
                throw new ResourceNotFoundException("File doesn't exist");
            }

            byte[] file;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(memoryStream);
                 file = memoryStream.ToArray();
            }

            var contentType = GetContentType(blobClient.Name);
            if (contentType != null)
            {
                await blobClient.SetHttpHeadersAsync(new BlobHttpHeaders
                {
                    ContentType = contentType,
                });
            }

            string uri = blobClient.Uri.AbsoluteUri;
            string fileName = uri.Substring(uri.LastIndexOf('/') + 1);

            return new FileContResult(file, contentType, fileName);
        }

        public async Task UploadFile(AzureProps azureProps,IFormFile file)
        {
            var id = _httpUserContextService.UserId;
            var login = _httpUserContextService.UserLogin;

            var ext = string.Empty;
            var allowedExtension = GetFileExtension(file, out ext);
            if (file is null || file.Length > 1048576 || !allowedExtension)
            {
                throw new ConflictExceptions("incorrect file format or size");
            }

            var name = $"{id}_{login}AV{ext}";
            var blobFile = await FindFile(name.Split('.').First(), azureProps);
            if (blobFile != null)
            {
                await blobFile.DeleteIfExistsAsync();
            }

            BlobServiceClient blobServiceClient = new BlobServiceClient(azureProps.AzureStorageConnection);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(azureProps.ContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(name);

            var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);
            await blobClient.SetHttpHeadersAsync(new BlobHttpHeaders
            {
                ContentDisposition = "inline" // Ustaw nagłówek Content-Disposition, aby wskazać, że plik ma być wyświetlany w przeglądarce, a nie pobierany
            });
            var uri = blobClient.Uri;

            var user = await _userRepository.GetFirstAsync(x => x.Id == id);
            user.AvatarPath = uri.ToString();
            await _userRepository.UpdateAsync(user);

        }
    }
}
