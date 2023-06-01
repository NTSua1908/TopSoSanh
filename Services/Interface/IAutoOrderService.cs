using TopSoSanh.Entity;

namespace TopSoSanh.Services.Interface
{
    public interface IAutoOrderService
    {
        bool OrderGearvn(Notification notification, string productUrl);
    }
}
