using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;
using MimeKit;
using MailKit.Security;
using static TopSoSanh.Helper.Appsettings;

namespace TopSoSanh.Services.Implement
{
    public class SendMailService : ISendMailService
    {
        public async void SendMailAsync(MailContent content)
        {
            content.Body = string.Format(ConstanstHelper.EmailContent, content.UserName, content.ItemUrl, content.ItemName, content.ImageUrl, content.UnsubcribeUrl);
            await SendMail(content);
        }
        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(MailSettings.DisplayName, MailSettings.Mail);
            email.From.Add(new MailboxAddress(MailSettings.DisplayName, MailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            if (!string.IsNullOrWhiteSpace(mailContent.CC))
            {
                email.Cc.Add(MailboxAddress.Parse(mailContent.CC));
            }
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                smtp.Connect(MailSettings.Host, MailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(MailSettings.Mail, MailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception)
            {
                System.IO.Directory.CreateDirectory("MailsSave");
                var emailsavefile = string.Format(@"MailsSave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
            }
            smtp.Disconnect(true);
        }
    }
}
