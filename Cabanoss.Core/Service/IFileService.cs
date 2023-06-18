using Cabanoss.Core.Common;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Http;

namespace Cabanoss.Core.Service
{
    public interface IFileService
    {
        Task<FileContResult> GetFile(AzureProps azureProps);
        Task UploadFile(AzureProps azureProps, IFormFile file);
    }
}