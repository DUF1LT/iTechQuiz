using System;
using System.Collections.Generic;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Page
    {
        public Guid Id { get; set; }

        public Survey Survey { get; set; }

        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
