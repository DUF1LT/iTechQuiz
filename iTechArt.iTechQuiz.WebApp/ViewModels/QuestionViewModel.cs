using System.Collections.Generic;
using System.Text.Json.Serialization;
using iTechArt.iTechQuiz.Domain.Models;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class QuestionViewModel
    {
        public bool IsEditable { get; set; }

        public bool IsRequired { get; set; }

        public string Content { get; set; }

        public int Number { get; set; }

        public QuestionType Type { get; set; }

        public int NumericOption { get; set; }

        public List<string> Options { get; set; }

    }
}
