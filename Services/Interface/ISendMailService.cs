using TopSoSanh.Helper;
using TopSoSanh.Entity;

namespace TopSoSanh.Services.Interface
{
    public interface ISendMailService
    {
        void SendMailTrackingAsync(MailContent content, string name, string itemName, string itemUrl, string imageUrl, string unsubscribeUrl);
        void SendMailOrderAsync(MailContent content, string name, string itemName, string itemUrl, string imageUrl, string unsubscribeUrl, Notification notification);
        Task SendMailConfirmAsync(MailContent content, string hostName, string name, string token, string email);
        Task SendMailResetPassword(MailContent content, string name, string password);
    }
}
