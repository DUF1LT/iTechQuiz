using System.Collections.Generic;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class SurveyViewModel
    {
        public string Title { get; set; }

        public int CurrentPage { get; set; }

        public List<PageViewModel> Pages { get; set; }

        public bool IsAnonymous { get; set; }

        public bool HasQuestionNumeration { get; set; }

        public bool HasPagesNumeration { get; set; }

        public bool HasRandomSequence { get; set; }

        public bool RenderStarsAtRequiredFields { get; set; }

        public bool HasProgressBar { get; set; }
    }
}
