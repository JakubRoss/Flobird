using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly string _serverAddress;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            var httpContextAccessor1 = httpContextAccessor;
            _serverAddress = $"{httpContextAccessor1.HttpContext!.Request.Scheme}://{httpContextAccessor1.HttpContext.Request.Host}";
        }

        /// <summary>
        /// Index redirect us to swagger documentation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return new RedirectResult($"{_serverAddress}/swagger");
        }
    }
}
