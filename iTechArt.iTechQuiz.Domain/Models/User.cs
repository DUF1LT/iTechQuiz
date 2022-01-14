using System;
using System.Collections.Generic;
using iTechArt.Repositories.Entity;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public DateTime RegisteredAt { get; set; }

        public int SurveysAmount { get; set; }

        public bool IsSystemUser { get; set; }

        public ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

        public ICollection<Survey> Surveys{ get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<UsersPassSurveys> PassedSurveys { get; set; }
    }
}
