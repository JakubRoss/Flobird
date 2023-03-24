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

        // GET: api/<WorkspaceController>
        [HttpGet]
        [Route("/api/[controller]")]
        public async Task<IEnumerable<WorkspaceDto>> GetWorkSpaces()
        {
            var workspaces = await _workspaceBussinessLogicService.GetUserWorkspacesAsync();
            return workspaces;
        }

        // GET api/<WorkspaceController>/5
        //[HttpGet("{id}")]
        [HttpGet]
        public async Task<WorkspaceDto> GetUserWorkspace(string login)
        {
            var workspaceDto = await _workspaceBussinessLogicService.GetUserWorkspaceAsync(login);
            return workspaceDto;
        }

        // POST api/<WorkspaceController>
        [HttpPost]
        public async Task CreateWorkspaceForUser(string login)   //To sciezka w tej wersji API bedzie bez uzytku
        {
            await _workspaceBussinessLogicService.AddWorkspaceAsync(login);
        }

        // PUT api/<WorkspaceController>/5
        [HttpPut]
        public void Put(string login,[FromBody] UpdateWorkspaceDto updateWorkspaceDto)
        {
            _workspaceBussinessLogicService.UpdateWorkspaceAsync(login, updateWorkspaceDto);
        }

        //// DELETE api/<WorkspaceController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
