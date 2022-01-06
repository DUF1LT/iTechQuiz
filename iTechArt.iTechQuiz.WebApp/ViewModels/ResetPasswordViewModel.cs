using System;
using System.ComponentModel.DataAnnotations;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class ResetPasswordViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [Display(Name = "Repeat Password")]
        public string RepeatPassword { get; set; }

        public string Token { get; set; }
    }
}
