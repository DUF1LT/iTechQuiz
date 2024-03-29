﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
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
        private readonly AnswerService _answerService;
        private readonly QuestionService _questionService;
        private readonly FileService _fileService;


        public SurveyController(UserService userService,
            SurveyService surveyService,
            QuestionService questionService,
            AnswerService answerService, 
            FileService fileService)
        {
            _userService = userService;
            _surveyService = surveyService;
            _questionService = questionService;
            _answerService = answerService;
            _fileService = fileService;
        }


        [HttpGet]
        [Route("NewSurvey")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult New()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSurvey(Guid id)
        {
            var survey = await _surveyService.GetSurveyAsync(id);
            if (!survey.IsAnonymous && !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            
            var surveyViewModel = survey.GetViewModel();

            return Json(surveyViewModel);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSurveyWithQuestions(Guid id)
        {
            var survey = await _surveyService.GetSurveyWithQuestionsAsync(id);
            if (!survey.IsAnonymous && !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            
            var surveyViewModel = survey.GetViewModelWithQuestions();

            return Json(surveyViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSurveyWithAnswers(Guid id)
        {
            var survey = await _surveyService.GetSurveyWithAnswersAsync(id);
            var surveyViewModel = survey.GetViewModelWithAnswers();

            return Json(surveyViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var file = await _fileService.GetFile(id);

            return File(file.Bytes, file.Type, file.Name);
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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        [Route("Survey/Delete/{id}")]
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
        [Authorize(Roles = Roles.Admin)]
        [Route("Survey/Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit([FromBody] SurveyViewModel model)
        {
            var user = await _userService.GetUserWithRolesAndSurveysAsync(Guid.Parse(User.GetId()));

            var previousSurvey = await _surveyService.GetSurveyAsync(model.Id);
            previousSurvey.IsDeleted = true;

            var surveyToSave = SurveyExtensions.CreateFromViewModel(model);
            surveyToSave.CreatedBy = user;

            await _surveyService.SaveSurveyAsync(surveyToSave);

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Survey/{id}")]
        public async Task<IActionResult> Survey(Guid id)
        {
            var survey = await _surveyService.GetSurveyAsync(id);
            if (!survey.IsAnonymous && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new {ReturnUrl = Url.Action("Survey", new {id})});
            }

            if (User.Identity.IsAuthenticated &&
                survey.UsersPassed.Any(p => p.SurveyId == survey.Id && p.UserId == Guid.Parse(User.GetId())))
            {
                return View("SurveyPassed");
            }

            return View(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Pass([FromBody] SurveyViewModel model)
        {
            var survey = await _surveyService.GetSurveyAsync(model.Id);

            var questions = model.Pages.SelectMany(p => p.Questions);

            var user = !User.Identity.IsAuthenticated && survey.IsAnonymous
                ? await _userService.GetUserWithRolesAndSurveysAsync(default)
                : await _userService.GetUserWithRolesAndSurveysAsync(Guid.Parse(User.GetId()));

            var userPassed = new UsersPassSurveys
            {
                Id = Guid.NewGuid(),
                Survey = survey,
                User = user,
                PassedAt = DateTime.Now
            };

            survey.UsersPassed.Add(userPassed);
            await _surveyService.UpdateSurveyAsync(survey);

            foreach (var question in questions)
            {
                var answer = new Answer
                {
                    PassId = userPassed.Id,
                    MultipleAnswer = JsonSerializer.Serialize(question.Answer.MultipleAnswer),
                    Numeric = question.Answer.Numeric,
                    Text = question.Answer.Text,
                    Question = await _questionService.GetQuestionAsync(question.Id),
                    User = user,
                    File = question.Answer.File.ByteArray is null ? null : new File
                    {
                        Bytes = question.Answer.File.ByteArray,
                        Name = question.Answer.File.Name,
                        Type = question.Answer.File.Type
                    }
                };

                await _answerService.SaveAnswerAsync(answer);
            }

            return Ok();
        }

        [HttpGet]
        [Route("Survey/Results/{id}")]
        public IActionResult Results(Guid id)
        {
            return View(id);
        }
    }
}
