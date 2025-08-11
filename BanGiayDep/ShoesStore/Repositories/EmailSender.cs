using ShoesStore.InterfaceRepositories;
using System.Net;
using System.Net.Mail;

namespace ShoesStore.Repositories
{
	public class EmailSender : IEmailSender
	{
		public void SendEmail(string email,string subject, string message)
		{
			var mail = "ltmichael52@gmail.com";
			var pw = "nfbz lgzn zjkd voty";

			var client = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				Credentials =  new NetworkCredential(mail, pw),
			};

			client.SendMailAsync(new MailMessage(
				from: mail,
				to: email,
				subject,
				message));
		}
	}
}
