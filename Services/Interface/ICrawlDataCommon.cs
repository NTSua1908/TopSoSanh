using TopSoSanh.DTO;
using TopSoSanh.Helper;

namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataCommon
    {
        PaginationDataModel<CrawlDataModel> getData(PaginationRequestModel req);
        List<PriceCompare> GetPriceOtherShop(string productName, Shop shop);

	}
}
