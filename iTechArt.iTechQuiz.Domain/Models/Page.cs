using System;
using System.Collections.Generic;
using iTechArt.Repositories.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Page : IEntity
    {
        public Guid Id { get; set; }

        public Survey Survey { get; set; }

        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
