using System.Security.Claims;
using Cabanoss.Core.Common;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Http;

namespace Cabanoss.Core.Service
{
    public interface IFileService
    {
        Task<FileContResult> GetFile(ClaimsPrincipal claimsPrincipal, AzureProps azureProps);
        Task UploadFile(AzureProps azureProps, ClaimsPrincipal claims, IFormFile file);
    }
}