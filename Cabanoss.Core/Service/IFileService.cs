using System.Security.Claims;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Http;

namespace Cabanoss.Core.Service
{
    public interface IFileService
    {
        Task<FileContResult> GetFile(ClaimsPrincipal claimsPrincipal);
        Task UploadFile(ClaimsPrincipal claims, IFormFile file);
    }
}