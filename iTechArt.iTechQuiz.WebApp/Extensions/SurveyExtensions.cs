﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.WebApp.ViewModels;

namespace iTechArt.iTechQuiz.WebApp.Extensions
{
    public static class SurveyExtensions
    {
        public static SurveyViewModel GetViewModel(this Survey survey)
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                CurrentPage = 0,
                IsAnonymous = survey.IsAnonymous,
                HasQuestionNumeration = survey.HasQuestionNumeration,
                HasRandomSequence = survey.HasRandomSequence,
                RenderStarsAtRequiredFields = survey.RenderStarsAtRequiredFields,
                HasProgressBar = survey.HasProgressBar
            };

            return surveyViewModel;
        }

        public static SurveyViewModel GetViewModelWithQuestions(this Survey survey)
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                CurrentPage = 0,
                Pages = survey.Pages.Select(p => new PageViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Questions = p.Questions.Select(q => new QuestionViewModel
                    {
                        Id = q.Id,
                        IsRequired = q.IsRequired,
                        Content = q.Content,
                        Number = q.Number,
                        Type = q.Type,
                        Options = JsonSerializer.Deserialize<List<string>>(q.Options),
                        Answer = new AnswerViewModel
                        {
                            File = new FileViewModel(),
                            MultipleAnswer = new List<string>()
                        }
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

        public static SurveyViewModel GetViewModelWithAnswers(this Survey survey)
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                CurrentPage = 0,
                PassedUsers = survey.UsersPassed.Select(p => p.User == null ? new UserViewModel
                {
                    Id = default,
                    PassId = p.Id,
                    UserName = $"Anonymous ({p.Id.ToString().Substring(0, 5).ToUpper()})",
                } : new UserViewModel
                {
                    Id = p.User.Id,
                    PassId = p.Id,
                    UserName = p.User.UserName
                }).ToList(),
                Pages = survey.Pages.Select(p => new PageViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Questions = p.Questions.Select(q => new QuestionViewModel
                    {
                        Id = q.Id,
                        IsRequired = q.IsRequired,
                        Content = q.Content,
                        Number = q.Number,
                        Type = q.Type,
                        Options = JsonSerializer.Deserialize<List<string>>(q.Options),
                        Answers = q.Answers.Select(a => new AnswerViewModel
                        {
                            User = a.User == null ? new UserViewModel
                            {
                                Id = default,
                                PassId = a.PassId,
                                UserName = "Anonymous",
                            } : new UserViewModel
                            {
                                Id = a.User.Id,
                                PassId = a.PassId,
                                UserName = a.User.UserName
                            },
                            File = a.File is null ? null : new FileViewModel
                            {
                                Id = a.File.Id,
                                Name = a.File.Name
                            },
                            MultipleAnswer = JsonSerializer.Deserialize<List<string>>(a.MultipleAnswer),
                            Numeric = a.Numeric,
                            Text = a.Text,
                        }).ToList()
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

        public static Survey CreateFromViewModel(SurveyViewModel viewModel)
        {
            var survey = new Survey
            {
                Title = viewModel.Title,
                HasProgressBar = viewModel.HasProgressBar,
                HasQuestionNumeration = viewModel.HasQuestionNumeration,
                HasRandomSequence = viewModel.HasRandomSequence,
                RenderStarsAtRequiredFields = viewModel.RenderStarsAtRequiredFields,
                IsAnonymous = viewModel.IsAnonymous,
                LastModifiedAt = DateTime.Now,
                Pages = new List<Page>(),
            };

            foreach (var page in viewModel.Pages)
            {
                var pageToSave = new Page
                {
                    Name = page.Name,
                    Survey = survey,
                    Questions = new List<Question>()
                };

                foreach (var question in page.Questions)
                {
                    var questionToSave = new Question
                    {
                        Number = question.Number,
                        Type = question.Type,
                        Content = question.Content,
                        SurveyPage = pageToSave,
                        IsRequired = question.IsRequired,
                    };

                    if (question.Options is not null)
                    {
                        questionToSave.Options = JsonSerializer.Serialize(question.Options);
                    }

                    pageToSave.Questions.Add(questionToSave);
                }

                survey.Pages.Add(pageToSave);
            }

            return survey;
        }
    }
}