using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.Common.Lists;
using iTechArt.iTechQuiz.Foundation.Services;
using iTechArt.iTechQuiz.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 5;


        private readonly SurveyService _surveyService;


        public HomeController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1, string filter = null)
        {
            ViewData["NameFilter"] = filter;

            if (pageIndex <= 1)
            {
                pageIndex = 1;
            }

            var paginatedSurveys = await _surveyService.GetPageWithPassedSurveysAsync(pageIndex, PageSize, Guid.Parse(User.GetId()), filter);

            var surveys = paginatedSurveys.Items.Select(e => new SurveyViewModel
            {
                Id = e.Id,
                CreatedBy = e.CreatedBy.UserName,
                Title = e.Title,
                LastModifiedAt = e.LastModifiedAt.ToShortDateString(),
                AnswersAmount = e.UsersPassed.Count,
            });

            var surveyViewModels = new PagedData<SurveyViewModel>(surveys,
                paginatedSurveys.TotalCount,
                pageIndex,
                PageSize);

            return View(surveyViewModels);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
    }
}
