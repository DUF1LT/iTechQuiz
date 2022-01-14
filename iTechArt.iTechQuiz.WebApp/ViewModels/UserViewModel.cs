using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string CurrentRole { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
