using System;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Answer : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PassId { get; set; }

        public Question Question { get; set; }

        public User User { get; set; }

        public File File { get; set; }

        public string Text { get; set; }
        
        public int Numeric { get; set; }

        public string MultipleAnswer { get; set; }
    }
}
