using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult NewSurvey()
        {
            return View();
        }
    }
}
