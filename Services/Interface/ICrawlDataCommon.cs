using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataCommon
    {
        PaginationDataModel getData(PaginationRequestModel req);
    }
}
