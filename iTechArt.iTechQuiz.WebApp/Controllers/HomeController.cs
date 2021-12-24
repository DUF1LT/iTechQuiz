using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //GET: Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
