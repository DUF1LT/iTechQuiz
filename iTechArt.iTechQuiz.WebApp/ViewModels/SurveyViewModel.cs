using System;
using System.Collections.Generic;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class SurveyViewModel
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public int CurrentPage { get; set; }

        public List<PageViewModel> Pages { get; set; }

        public string LastModifiedAt { get; set; }

        public int AnswersAmount { get; set; }

        public bool IsAnonymous { get; set; }

        public bool HasQuestionNumeration { get; set; }

        public bool HasRandomSequence { get; set; }

        public bool RenderStarsAtRequiredFields { get; set; }

        public bool HasProgressBar { get; set; }
    }
}
