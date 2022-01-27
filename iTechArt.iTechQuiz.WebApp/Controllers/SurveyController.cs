using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult NewSurvey()
        {
            return View(new Survey());
        }
    }
}
