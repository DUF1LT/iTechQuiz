using System;
using iTechArt.Repositories.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Answer : IEntity
    {
        public Guid Id { get; set; }

        public Question Question { get; set; }

        public User User { get; set; }

        public Guid FileId { get; set; }

        public File File { get; set; }

        public string Text { get; set; }
        
        public int Numeric { get; set; }

        public string MultipleAnswer { get; set; }
    }
}
