using MailKit.Security;
using MimeKit;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;
using static TopSoSanh.Helper.Appsettings;
using static TopSoSanh.Helper.ConstanstHelper;

namespace TopSoSanh.Services.Implement
{
    public class SendMailService : ISendMailService
    {
        public async void SendMailTrackingAsync(MailContent content, string name, string itemName, string itemUrl, string imageUrl, string unsubscribeUrl)
        {
            content.Body = string.Format(EmailConstant.EmailTracking, name, itemUrl, itemName, imageUrl, unsubscribeUrl);
            await SendMail(content);
        }

        public async void SendMailOrderAsync(MailContent content, string name, string itemName, string itemUrl, string imageUrl, string unsubscribeUrl, Notification notification)
        {
            string[] address = new string[] { notification.Address, notification.Commune, notification.District, notification.Province };
            content.Body = string.Format(EmailConstant.ConfirmOrder, name, itemUrl, notification.OrderName, notification.OrderEmail, notification.PhoneNumber, String.Join(", ", address), itemName, imageUrl, unsubscribeUrl);
            await SendMail(content);
        }

        private async Task SendMail(MailContent mailContent) 
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

        public async Task SendMailConfirmAsync(MailContent content, string hostName, string name, string token, string email)
        {
            var confirmationLink = @$"https://{hostName}/api/auth/confirmemail?token={token}&email={email}";
            content.Body = string.Format(EmailConstant.ConfirmEmail, name, confirmationLink);
            await SendMail(content);
        }

        public async Task SendMailResetPassword(MailContent content, string name, string password)
        {
            content.Body = string.Format(EmailConstant.ResetPassword, name, password);
            await SendMail(content);
        }
    }
}
