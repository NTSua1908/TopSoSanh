using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataAnphatService
    {
        List<CrawlDataModel> CrawlData(string keyword, Action<CrawlDataModel> GetPriceAnKhang, Action<CrawlDataModel> GetPriceGearvn);
        void GetPriceByName(CrawlDataModel model);
        void GetPriceByName(string productName, List<PriceCompare> priceCompares);

		double CrawlPrice(string url);
    }
}
