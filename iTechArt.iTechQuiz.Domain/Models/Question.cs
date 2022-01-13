﻿using System;
using System.Collections.Generic;
using iTechArt.Repositories.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }

        public Survey Survey { get; set; }

        public QuestionType Type { get; set; }

        public string Content { get; set; }

        public string Options { get; set; }

        public bool IsRequired { get; set; }

        public int SurveyPage { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
