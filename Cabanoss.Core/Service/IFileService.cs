using System.Security.Claims;
using Cabanoss.Core.Service.Impl;

namespace Cabanoss.Core.Service
{
    public interface IFileService
    {
        Task<FileContResult> GetFile(string fileName, ClaimsPrincipal claimsPrincipal);
        Task UploadFile();
    }
}