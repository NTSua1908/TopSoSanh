using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataPhongVuService
    {
        Task<List<CrawlDataModel>> CrawlData(string keyword);
    }
}
