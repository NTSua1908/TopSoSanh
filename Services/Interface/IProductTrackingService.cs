using Fizzler;
using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface IProductTrackingService
    {
        void SubscribeProduct(SubscribeProductModel model, string hostName, ErrorModel errors);

        void SubscribeProductFromCustomShop(SubscribeProductCustomModel model, string hostName);

        string UnSubscribeProduct(string email, string token);

        void ProductTracking(string productUrl, string hostName);

        void ProductTrackingCustom(string productUrl, string priceSelector, string hostName);

        TrackingResultModel GetTrackingResult(string productUrl);

        void ToggleNotification(Guid notificationId, ErrorModel errors);
    }
}
