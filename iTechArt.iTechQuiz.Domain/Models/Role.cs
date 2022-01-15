using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
