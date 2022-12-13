using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface IProductTrackingService
    {
        void SubscribeProduct(SubscribeProductModel model);

        void ProductTracking(string productUrl);

        List<double> GetTrackingResult(Guid productId);
    }
}
