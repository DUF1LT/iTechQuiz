using System;
using iTechArt.Repositories.Entity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class File : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Bytes { get; set; }

        public Guid AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
