using System;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class UsersPassSurveys
    {
        public Guid UserId { get; set; }

        public User<Guid> User { get; set; }

        public Guid SurveyId { get; set; }

        public Survey Survey { get; set; }

        public DateTime PassDate { get; set; }
    }
}
