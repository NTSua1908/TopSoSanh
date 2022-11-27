using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataGearvnService
    {
        public List<CrawlDataModel> CrawlData(string keyword);
    }
}
