using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.WebApp.ViewModels.Constructor;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.iTechQuiz.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        [HttpGet]
        public IActionResult New()
        {
            var page = new PageViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Page 1",
                Questions = new List<QuestionViewModel>()
            };

            var surveyViewModel = new SurveyViewModel
            {
                Pages = new List<PageViewModel> { page },
                CurrentPage = 0
            };

            return View(surveyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSingleAnswerQuestion(SurveyViewModel model)
        {
            if (model is null)
            {
                var page = new PageViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Page 1",
                    Questions = new List<QuestionViewModel>()
                };

                var surveyViewModel = new SurveyViewModel
                {
                    Pages = new List<PageViewModel> { page },
                    CurrentPage = 0
                };

                return PartialView("Survey/_NewSurveyMainPartial", surveyViewModel);
            }

            if (model.Pages[model.CurrentPage].Questions is null)
            {
                model.Pages[model.CurrentPage].Questions = new List<QuestionViewModel>();
            }

            var id = Guid.NewGuid();
            var question = new QuestionViewModel
            {
                Id = id,
                Content = "Question with single answer",
                Number = model.Pages[model.CurrentPage].Questions.Count + 1,
                Options = new List<SingleOptionViewModel>
                {
                    new(){ Option = "Answer 1" },
                    new(){ Option = "Answer 2" },
                    new(){ Option = "Answer 3" }
                },
                Type = QuestionType.SingleAnswer
            };

            model.Pages[model.CurrentPage].Questions.Add(question);

            return PartialView("Survey/_NewSurveyMainPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuestion(Guid id, SurveyViewModel model)
        {
            if (id == Guid.Empty || model is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            var question = model.Pages[model.CurrentPage]
                .Questions
                .FirstOrDefault(p => p.Id == id);

            if (question is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            question.IsEditable = true;

            return PartialView("Survey/_NewSurveyMainPartial", model);
        }
    }
}
