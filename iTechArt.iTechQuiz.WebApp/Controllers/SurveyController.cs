using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.Common.Lists;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Foundation.Services;
using iTechArt.iTechQuiz.Repositories.Constants;
using iTechArt.iTechQuiz.WebApp.Extensions;
using iTechArt.iTechQuiz.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private const int PageSize = 5;

        private readonly UserService _userService;
        private readonly SurveyService _surveyService;

        public SurveyController(UserService userService, SurveyService surveyService)
        {
            _userService = userService;
            _surveyService = surveyService;
        }


        [HttpGet]
        [Route("NewSurvey")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult New()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetSurvey(Guid id)
        {
            var survey = await _surveyService.GetSurveyAsync(id);
            var surveyViewModel = survey.GetViewModel();

            return Json(surveyViewModel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Save([FromBody] SurveyViewModel model)
        {
            var user = await _userService.GetUserWithRolesAndSurveysAsync(Guid.Parse(User.GetId()));

            var surveyToSave = SurveyExtensions.CreateFromViewModel(model);
            surveyToSave.CreatedBy = user;

            await _surveyService.SaveSurveyAsync(surveyToSave);

            return Ok();
        }

        [HttpGet]
        [Route("MySurveys")]
        public async Task<IActionResult> MySurveys(int pageIndex = 1, string filter = null)
        {
            ViewData["NameFilter"] = filter;

            if (pageIndex <= 1)
            {
                pageIndex = 1;
            }

            var paginatedSurveys = await _surveyService.GetPageWithCreatedSurveysAsync(pageIndex, PageSize, Guid.Parse(User.GetId()), filter);
            var surveys = paginatedSurveys.Items.Select(e => new SurveyViewModel
            {
                Id = e.Id,
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
        [Route("Survey/{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var survey = await _surveyService.GetSurveyAsync(id);

            if (survey is null)
            {
                return NotFound();
            }

            var surveyViewModel = survey.GetViewModel();

            return View(surveyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            await _surveyService.DeleteSurveyAsync(id);

            return RedirectToAction("MySurveys");
        }

        [HttpGet]
        [Route("Survey/{id}/Edit")]
        public IActionResult Edit(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit([FromBody] SurveyViewModel survey)
        {
            await _surveyService.DeleteSurveyAsync(survey.Id);

            var surveyToSave = SurveyExtensions.CreateFromViewModel(survey);
            await _surveyService.SaveSurveyAsync(surveyToSave);

            return Ok();
        }

        [HttpGet]
        [Route("Survey/{id}")]
        public IActionResult Survey(Guid id)
        {
            return View();
        }

        [HttpGet]
        [Route("Survey/Results/{id}")]
        public IActionResult Results(Guid id)
        {
            return View();
        }
    }
}
