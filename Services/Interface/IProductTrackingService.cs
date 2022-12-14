using Fizzler;
using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface IProductTrackingService
    {
        void SubscribeProduct(SubscribeProductModel model, string hostName);

        string UnSubscribeProduct(string email, string token);

        void ProductTracking(string productUrl, string hostName);

        List<double> GetTrackingResult(string productUrl);
    }
}
