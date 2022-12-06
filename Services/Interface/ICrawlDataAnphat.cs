using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataAnphat
    {
        List<CrawlDataModel> CrawlData(string keyword);
    }
}
