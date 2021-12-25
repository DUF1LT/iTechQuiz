using System.ComponentModel.DataAnnotations;

namespace iTechArt.iTechQuiz.WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Login (email)")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [Display(Name = "Repeat Password")]
        public string RepeatPassword { get; set; }
    }
}
