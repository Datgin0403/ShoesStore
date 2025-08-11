namespace ShoesStore.Service.SendEmail
{
    public interface IEmailSender
    {
        void SendEmail(string receiver, string subject, string message);
    }
}
