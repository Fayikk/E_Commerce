namespace ECommerce_ForUdemy_Client.MailService
{
    public interface IMailHelperForgotPassword
    {
        public void SendEmailForResEmail(string subject, string mail,string forgotPasswordNumber);
    }
}
