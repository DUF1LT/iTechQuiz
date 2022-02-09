using System;
using System.Collections.Generic;
using iTechArt.iTechQuiz.Domain.Models;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        
        public bool IsEditable { get; set; }

        public bool IsRequired { get; set; }

        public string Content { get; set; }

        public int Number { get; set; }

        public QuestionType Type { get; set; }

        public int NumericOption { get; set; }

        public List<string> Options { get; set; }

        public AnswerViewModel Answer { get; set; }

    }
}
