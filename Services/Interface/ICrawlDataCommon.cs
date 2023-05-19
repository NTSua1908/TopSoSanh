using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataCommon
    {
        PaginationDataModel<CrawlDataModel> getData(PaginationRequestModel req);
    }
}
