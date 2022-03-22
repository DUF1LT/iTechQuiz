using System;
using System.Collections.Generic;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class AnswerViewModel
    {
        public string Text { get; set; }

        public int Numeric { get; set; }

        public List<string> MultipleAnswer { get; set; }

        public UserViewModel User { get; set; }

        public FileViewModel File { get; set; }
    }
}
