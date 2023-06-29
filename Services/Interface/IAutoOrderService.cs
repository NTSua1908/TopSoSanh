using TopSoSanh.Entity;

namespace TopSoSanh.Services.Interface
{
    public interface IAutoOrderService
    {
        bool OrderGearvn(Notification notification, string productUrl);
        bool OrderAnPhat(Notification notification, string productUrl);
        bool OrderAnKhang(Notification notification, string productUrl);
    }
}
