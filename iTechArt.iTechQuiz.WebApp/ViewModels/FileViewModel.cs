using System;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class FileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] ByteArray { get; set; }

        public string Type { get; set; }
    }
}
