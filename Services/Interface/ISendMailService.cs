using TopSoSanh.Helper;

namespace TopSoSanh.Services.Interface
{
    public interface ISendMailService
    {
        void SendMailAsync(MailContent content);
    }
}
