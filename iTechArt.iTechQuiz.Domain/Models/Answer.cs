using System;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Answer
    {
        public Guid Id { get; set; }

        public Question Question { get; set; }

        public User<Guid> User { get; set; }

        public File File { get; set; }

        public string Text { get; set; }
        
        public int Numeric { get; set; }

        public string MultipleAnswer { get; set; }
    }
}
