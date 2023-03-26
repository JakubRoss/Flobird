using Cabanoss.Core.BussinessLogicService;
using Cabanoss.Core.Model.Workspace;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/users/{login}/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceBussinessLogicService _workspaceBussinessLogicService;

        public WorkspaceController(IWorkspaceBussinessLogicService workspaceBussinessLogicService)
        {
            _workspaceBussinessLogicService = workspaceBussinessLogicService;
        }


            [HttpGet]
        public async Task<WorkspaceDto> GetUserWorkspace(string login)
        {
            var workspaceDto = await _workspaceBussinessLogicService.GetUserWorkspaceAsync(login);
            return workspaceDto;
        }



        // PUT api/<WorkspaceController>/
        [HttpPut]
        public void Put(string login,[FromBody] UpdateWorkspaceDto updateWorkspaceDto)
        {
            _workspaceBussinessLogicService.UpdateWorkspaceAsync(login, updateWorkspaceDto);
        }
    }
}
