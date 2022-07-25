using Microsoft.AspNetCore.Mvc;

namespace Noctools.Application.Microservice.Controllers
{
    [Route("")]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        [HttpGet("/error")]
        public IActionResult Index()
        {
            return new BadRequestResult();
        }
    }
}
