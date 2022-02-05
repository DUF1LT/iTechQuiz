﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Foundation.Services;
using iTechArt.iTechQuiz.Repositories;
using iTechArt.iTechQuiz.WebApp.ViewModels.Constructor;
using iTechArt.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly UserService _userService;
        private readonly UnitOfWork _unitOfWork;

        public SurveyController(UserService userService, UnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> New()
        {
            var user = await _userService.GetUser(Guid.Parse(User.GetId()));
            var newSurveyNumber = user.Surveys.Count + 1;

            return View(newSurveyNumber);
        }

        [HttpGet]
        public IActionResult MySurveys()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SurveyViewModel survey)
        {
            var user = await _userService.GetUser(Guid.Parse(User.GetId()));

            var surveyToSave = new Survey
            {
                CreatedBy = user,
                Title = survey.Title,
                HasProgressBar = survey.HasProgressBar,
                HasQuestionNumeration = survey.HasQuestionNumeration,
                HasRandomSequence = survey.HasRandomSequence,
                RenderStarsAtRequiredFields = survey.RenderStarsAtRequiredFields,
                IsAnonymous = survey.IsAnonymous,
                LastModifiedAt = DateTime.Now,
                Pages = new List<Page>()
            };

            foreach (var page in survey.Pages)
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

            await _unitOfWork.GetRepository<Survey, Guid, Repository<Survey, Guid>>().CreateAsync(surveyToSave);

            return Ok();
        }
    }
}
