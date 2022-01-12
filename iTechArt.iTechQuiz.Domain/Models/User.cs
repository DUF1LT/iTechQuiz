using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class User<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public DateTime RegisteredAt { get; set; }

        public int SurveysAmount { get; set; }

        public bool IsSystemUser { get; set; }

        public List<Survey> Surveys;

        public List<Answer> Answers;

        public List<UsersPassSurveys> PassedSurveys;
    }
}
