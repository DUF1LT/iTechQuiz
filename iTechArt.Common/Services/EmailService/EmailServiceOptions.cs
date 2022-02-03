namespace iTechArt.Common.Services.EmailService
{
    public class EmailServiceOptions
    {
        public string EmailName { get; set; }
        
        public string EmailAddress { get; set; } 
        
        public string EmailPassword { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpHostPort { get; set; }
    }
}
