using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataAnphatService
    {
        List<CrawlDataModel> CrawlData(string keyword);
    }
}
