﻿using System;
using System.Collections.Generic;
using iTechArt.Repositories.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Survey : IEntity
    {
        public Guid Id { get; set; }

        public User Founder { get; set; }

        public string Title { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public int AnswerAmount { get; set; }

        public int PagesAmount { get; set; }

        public bool IsAnonymous { get; set; }

        public bool HasQuestionNumeration { get; set; }

        public bool HasPagesNumeration { get; set; }

        public bool HasRandomSequence { get; set; }

        public bool RenderStarsAtRequiredFields { get; set; }

        public bool HasProgressBar { get; set; }

        public List<Question> Questions { get; set; }

        public List<UsersPassSurveys> UsersPassed { get; set; }
    }
}
