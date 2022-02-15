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
            var surveyViewModel = CreateSurveyViewModelFromSurvey(survey);

            return Json(surveyViewModel);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult MySurveys()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Save([FromBody] SurveyViewModel survey)
        {
            var user = await _userService.GetUserWithRolesAndSurveysAsync(Guid.Parse(User.GetId()));

            var surveyToSave = await CreateSurveyFromViewModelAsync(survey);

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

            var paginatedSurveys = await _surveyService.GetPageAsync(pageIndex, PageSize, filter);
            var surveys = paginatedSurveys.Items.Select(e => new SurveyViewModel
            {
                Id = e.Id,
                Title = e.Title,
                LastModifiedAt = e.LastModifiedAt.ToShortDateString(),
                AnswersAmount = e.UsersPassed.Count,

            });

            var userViewModels = new PagedData<SurveyViewModel>(surveys,
                paginatedSurveys.TotalCount,
                pageIndex,
                PageSize);

            return View(userViewModels);
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

            var surveyViewModel = new SurveyViewModel
            {
                Id = id,
                Title = survey.Title,
                AnswersAmount = survey.UsersPassed.Count,
                LastModifiedAt = survey.LastModifiedAt.ToShortDateString()
            };

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
            var user = await _userService.GetUserWithRolesAndSurveysAsync(Guid.Parse(User.GetId()));

            var previousSurvey = await _surveyService.GetSurveyAsync(survey.Id);
            previousSurvey.IsDeleted = true;

            var surveyToSave = await CreateSurveyFromViewModelAsync(survey);

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
        [Route("Survey/{id}/Results")]
        public IActionResult Results(Guid id)
        {
            return View();
        }

        public async Task<Survey> CreateSurveyFromViewModelAsync(SurveyViewModel model)
        {
            var user = await _userService.GetUserWithRolesAndSurveysAsync(Guid.Parse(User.GetId()));

            var surveyToSave = new Survey
            {
                CreatedBy = user,
                Title = model.Title,
                HasProgressBar = model.HasProgressBar,
                HasQuestionNumeration = model.HasQuestionNumeration,
                HasRandomSequence = model.HasRandomSequence,
                RenderStarsAtRequiredFields = model.RenderStarsAtRequiredFields,
                IsAnonymous = model.IsAnonymous,
                LastModifiedAt = DateTime.Now,
                Pages = new List<Page>()
            };

            foreach (var page in model.Pages)
            {
                var pageToSave = new Page
                {
                    Name = page.Name,
                    Survey = surveyToSave,
                    Questions = new List<Question>()
                };

                foreach (var question in page.Questions)
                {
                    var questionToSave = new Question
                    {
                        Number = question.Number,
                        Type = question.Type,
                        Content = question.Content,
                        Survey = surveyToSave,
                        SurveyPage = pageToSave,
                        IsRequired = question.IsRequired,
                    };

                    if (question.Options is not null)
                    {
                        questionToSave.Options = JsonSerializer.Serialize(question.Options);
                    }

                    pageToSave.Questions.Add(questionToSave);
                }

                surveyToSave.Pages.Add(pageToSave);
            }

            return surveyToSave;
        }

        public SurveyViewModel CreateSurveyViewModelFromSurvey(Survey survey)
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                CurrentPage = 0,
                Pages = survey.Pages.Select(p => new PageViewModel
                {
                    Name = p.Name,
                    Questions = p.Questions.Select(q => new QuestionViewModel
                    {
                        Content = q.Content,
                        IsRequired = q.IsRequired,
                        Number = q.Number,
                        Type = q.Type,
                        Options = JsonSerializer.Deserialize<List<string>>(q.Options)
                    }).ToList()
                }).ToList(),
                IsAnonymous = survey.IsAnonymous,
                HasQuestionNumeration = survey.HasQuestionNumeration,
                HasRandomSequence = survey.HasRandomSequence,
                RenderStarsAtRequiredFields = survey.RenderStarsAtRequiredFields,
                HasProgressBar = survey.HasProgressBar
            };

            return surveyViewModel;
        }
    }
}
