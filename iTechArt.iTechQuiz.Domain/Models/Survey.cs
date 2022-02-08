using System;
using System.Collections.Generic;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Survey : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public User CreatedBy { get; set; }

        public string Title { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAnonymous { get; set; }

        public bool HasQuestionNumeration { get; set; }

        public bool HasRandomSequence { get; set; }

        public bool RenderStarsAtRequiredFields { get; set; }

        public bool HasProgressBar { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Page> Pages { get; set; }

        public ICollection<UsersPassSurveys> UsersPassed { get; set; }
    }
}
