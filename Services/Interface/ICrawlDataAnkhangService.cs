using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataAnkhangService
    {
        List<CrawlDataModel> CrawlData(string keyword, Action<CrawlDataModel> GetPriceAnPhat, Action<CrawlDataModel> GetPriceGearvn);
        void GetPriceByName(CrawlDataModel model);
        double CrawlPrice(string url);
    }
}
