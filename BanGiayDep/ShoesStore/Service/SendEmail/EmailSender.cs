using System.Net.Mail;
using System.Net;

namespace ShoesStore.Service.SendEmail
{
    public class EmailSender : IEmailSender
    {
     
            private readonly IConfiguration _configuration;

            public EmailSender(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public void SendEmail(string receiver, string subject, string message)
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SMTPHost"])
                {
                    Port = int.Parse(_configuration["EmailSettings:SMTPPort"]),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:SMTPUser"],
                        _configuration["EmailSettings:SMTPPassword"]),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:SMTPUser"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(receiver);
                smtpClient.Send(mailMessage);
            }
        

    }
}
