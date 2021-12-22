using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //GET: Index
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
