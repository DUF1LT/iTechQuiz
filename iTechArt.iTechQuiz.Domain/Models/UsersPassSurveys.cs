using System;
using iTechArt.Repositories.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class UsersPassSurveys
    {
        public Guid Id;

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid SurveyId { get; set; }

        public Survey Survey { get; set; }

        public DateTime PassedAt { get; set; }
    }
}
