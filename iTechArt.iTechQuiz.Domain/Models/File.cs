using System;
using System.Collections.Generic;
using System.Net;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class File
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Bytes { get; set; }

        public Answer Answer { get; set; }
    }
}
