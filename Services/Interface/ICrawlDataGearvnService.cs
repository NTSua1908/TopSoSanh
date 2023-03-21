using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataGearvnService
    {
        List<CrawlDataModel> CrawlData(string keyword);
        CrawlDetailModel CrawlDetail(string url);
        PriceCompare GetPriceByName(string productName);
        double CrawlPrice(string url);
    }
}
