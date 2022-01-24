﻿using System;
using System.Collections.Generic;
using iTechArt.iTechQuiz.Domain.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }

        public Survey Survey { get; set; }

        public Page SurveyPage { get; set; }

        public QuestionType Type { get; set; }

        public string Content { get; set; }

        public string Options { get; set; }

        public int Number { get; set; }

        public bool IsRequired { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
