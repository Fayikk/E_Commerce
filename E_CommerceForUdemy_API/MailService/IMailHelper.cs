namespace E_CommerceForUdemy_API.MailService
{
    public interface IMailHelper
    {
        public void SendEmailForOrder(string subject, string body, string mail);
    }
}
