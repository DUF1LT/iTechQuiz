using System;
using System.Collections.Generic;
using iTechArt.Repositories.Entity;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.iTechQuiz.Domain.Models
{
    public class Role : IdentityRole<Guid>, IEntity
    {
        public Role() : base() { }

        public Role(string name)
            : base(name)
        { }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
