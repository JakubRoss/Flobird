using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _serverAddress;

        //public HomeController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _serverAddress = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        //}

        /// <summary>
        /// Index redirect us to project on GitHub
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return new RedirectResult("https://github.com/JakubRoss/flobird");
        }
    }
}
