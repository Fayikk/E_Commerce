namespace E_CommerceForUdemy_API.MailService
{
    public interface IMailHelperForgotPassword
    {
        public bool SendEmailForResEmail(string subject, string mail,string forgotPasswordNumber);
    }
}
