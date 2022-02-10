using System;
using iTechArt.Repositories;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class File : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Bytes { get; set; }

        public string Type { get; set; }

        public Guid AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
