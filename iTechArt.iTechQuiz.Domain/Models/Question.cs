using System;
using System.Collections.Generic;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Question : IEntity<Guid>
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
