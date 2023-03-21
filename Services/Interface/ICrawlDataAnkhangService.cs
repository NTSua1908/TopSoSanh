using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataAnkhangService
    {
        List<CrawlDataModel> CrawlData(string keyword);
        PriceCompare GetPriceByName(string productName);
        double CrawlPrice(string url);
    }
}
