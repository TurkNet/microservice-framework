using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Noctools.Application.Configuration;

namespace Noctools.Application.Microservice.Controllers
{
    [Route("")]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly string _basePath;

        public HomeController(IConfiguration config)
        {
            _basePath = config.GetBasePath() ?? "/";
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Redirect($"~{_basePath}swagger");
        }
    }
}
