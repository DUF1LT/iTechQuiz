using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.Common.Extensions;
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

            var currentPage = model.Pages[model.CurrentPage];
            if (currentPage.Questions is null)
            {
                currentPage.Questions = new List<QuestionViewModel>();
            }

            var id = Guid.NewGuid();
            var question = new QuestionViewModel
            {
                Id = id,
                Content = "Question with single answer",
                Number = currentPage.Questions.Count + 1,
                Options = new List<string>
                {
                    "Answer 1",
                    "Answer 2",
                    "Answer 3"
                },
                Type = QuestionType.SingleAnswer
            };

            currentPage.Questions.Add(question);
            ViewData["PageQuestionsAmount"] = currentPage.Questions.Count;

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

            var currentPage = model.Pages[model.CurrentPage];
            var question = currentPage.Questions
                .FirstOrDefault(p => p.Id == id);

            if (question is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            question.IsEditable = !question.IsEditable;
            ViewData["PageQuestionsAmount"] = currentPage.Questions.Count;

            return PartialView("Survey/_NewSurveyMainPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuestion(Guid id, SurveyViewModel model)
        {
            if (id == Guid.Empty || model is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            var currentPage = model.Pages[model.CurrentPage];
            var question = currentPage.Questions
                .FirstOrDefault(p => p.Id == id);

            if (question is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            var index = currentPage.Questions.IndexOf(question);
            for (int i = index + 1; i < currentPage.Questions.Count; i++)
            {
                currentPage.Questions[i].Number--;
            }

            currentPage.Questions.Remove(question);
            ViewData["PageQuestionsAmount"] = currentPage.Questions.Count;

            return PartialView("Survey/_NewSurveyMainPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveUp(Guid id, SurveyViewModel model)
        {
            if (id == Guid.Empty || model is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }
            var currentPage = model.Pages[model.CurrentPage];

            var question = currentPage.Questions
                .FirstOrDefault(p => p.Id == id);

            if (question is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            var number = question.Number;
            var prevNumberQuestion = currentPage.Questions.FirstOrDefault(p => p.Number == number - 1);
            if (prevNumberQuestion is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            question.Number = prevNumberQuestion.Number;
            prevNumberQuestion.Number = number;

            currentPage.Questions = currentPage.Questions.OrderBy(p => p.Number).ToList();
            ViewData["PageQuestionsAmount"] = currentPage.Questions.Count;

            return PartialView("Survey/_NewSurveyMainPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveDown(Guid id, SurveyViewModel model)
        {
            if (id == Guid.Empty || model is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            var currentPage = model.Pages[model.CurrentPage];
            var question = currentPage.Questions
                .FirstOrDefault(p => p.Id == id);

            if (question is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }


            var number = question.Number;
            var nextNumberQuestion = currentPage.Questions.FirstOrDefault(p => p.Number == number + 1);
            if (nextNumberQuestion is null)
            {
                return PartialView("Survey/_NewSurveyMainPartial", model);
            }

            question.Number = nextNumberQuestion.Number;
            nextNumberQuestion.Number = number;

            currentPage.Questions = currentPage.Questions.OrderBy(p => p.Number).ToList();
            ViewData["PageQuestionsAmount"] = currentPage.Questions.Count;

            return PartialView("Survey/_NewSurveyMainPartial", model);
        }
    }
}
