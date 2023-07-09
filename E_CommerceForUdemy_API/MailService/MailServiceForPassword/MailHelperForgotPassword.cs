using E_CommerceForUdemy_API.MailService;
using ECommerce_ForUdemy_Client.Service.IService;
using MailKit.Net.Smtp;
using MimeKit;

namespace E_CommerceForUdemy_API.MailServiceForPassword
{
    public class MailHelperForgotPassword : IMailHelperForgotPassword
    {
        public bool SendEmailForResEmail(string subject,string mail,string forgotPasswordNumber)
        {

            string body = $"https://localhost:7034/ForgotPassword/{forgotPasswordNumber}";
            try
            {

                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("MyMvcMailService@gmail.com"));

                emailToSend.To.Add(MailboxAddress.Parse(mail));

                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                //send email
                using var emailClient = new SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("veznedaroglufayik2@gmail.com", "wfqvpjvukemfkiam");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
    }
}
