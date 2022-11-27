using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataZShopService
    {
        List<CrawlDataModel> CrawlData(string keyword);
    }
}
