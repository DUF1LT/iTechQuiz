using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        //GET: Index
        public IActionResult Index()
        {
            _logger.LogInformation("Get About/Index");
            return View();
        }
    }
}
