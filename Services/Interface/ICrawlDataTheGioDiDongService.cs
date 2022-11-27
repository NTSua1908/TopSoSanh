using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataTheGioDiDongService
    {
        public List<CrawlDataModel> CrawlData(string keyword);
    }
}
