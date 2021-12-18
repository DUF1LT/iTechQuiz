using System;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
