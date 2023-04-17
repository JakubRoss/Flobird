using Cabanoss.Core.Service;
using Cabanoss.Core.Model.Workspace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Service.Impl;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/users/w")]
    [ApiController]
    [Authorize]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceBussinessLogicService;

        public WorkspacesController(IWorkspaceService workspaceBussinessLogicService)
        {
            _workspaceBussinessLogicService = workspaceBussinessLogicService;
        }

        private int Getid()
        {
            var claims = User.Claims;
            var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return int.Parse(idClaim.Value);
        }

        [HttpGet]
        public async Task<WorkspaceDto> GetUserWorkspace()
        {
            var workspaceDto = await _workspaceBussinessLogicService.GetUserWorkspaceAsync(Getid());
            return workspaceDto;
        }

        [HttpPut]
        public void Put([FromBody] UpdateWorkspaceDto updateWorkspaceDto)
        {
            _workspaceBussinessLogicService.UpdateWorkspaceAsync(Getid(), updateWorkspaceDto);
        }
    }
}
