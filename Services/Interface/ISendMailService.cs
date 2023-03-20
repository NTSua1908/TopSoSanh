using TopSoSanh.Helper;

namespace TopSoSanh.Services.Interface
{
    public interface ISendMailService
    {
        void SendMailTrackingAsync(MailContent content, string name, string itemName, string itemUrl, string imageUrl, string unsubscribeUrl);
        Task SendMailConfirmAsync(MailContent content, string hostName, string name, string token, string email);
        Task SendMailResetPassword(MailContent content, string name, string password);
    }
}
