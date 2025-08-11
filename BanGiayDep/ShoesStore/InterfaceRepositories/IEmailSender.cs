namespace ShoesStore.InterfaceRepositories
{
	public interface IEmailSender
	{
		void SendEmail(string email,string subject, string message);
	}
}
