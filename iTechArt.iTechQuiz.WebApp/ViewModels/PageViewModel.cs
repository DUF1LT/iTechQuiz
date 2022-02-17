using System;
using System.Collections.Generic;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class PageViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
