using System;
using iTechArt.iTechQuiz.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Context
{
    public class iTechQuizContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public iTechQuizContext(DbContextOptions options)
        : base(options)
        { }
    }
}
