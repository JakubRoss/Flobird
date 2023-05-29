using Microsoft.AspNetCore.Mvc;


namespace Cabanoss.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serverAddress;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _serverAddress = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return new RedirectResult("https://github.com/JakubRoss/Cabanoss");
        }
    }
}
