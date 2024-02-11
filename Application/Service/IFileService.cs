using Application.Common;
using Application.Service.Impl;
using Microsoft.AspNetCore.Http;

namespace Application.Service
{
    public interface IFileService
    {
        Task<FileContResult> GetFile(AzureProps azureProps);
        Task UploadFile(AzureProps azureProps, IFormFile file);
    }
}