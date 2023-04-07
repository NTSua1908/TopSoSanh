using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataGearvnService
    {
        List<CrawlDataModel> CrawlData(string keyword, Action<CrawlDataModel> GetPriceAnKhang, Action<CrawlDataModel> GetPriceAnPhat);
        CrawlDetailModel CrawlDetail(string url);
        void GetPriceByName(CrawlDataModel model);
        double CrawlPrice(string url);
    }
}
