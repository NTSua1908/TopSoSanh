using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataCommon : ICrawlDataCommon
    {
        private readonly ICrawlDataGearvnService _crawlDataGearvnService;
        private readonly ICrawlDataAnphat _crawlDataAnphatService;
        private readonly ICrawlDataZShopService _crawlDataZShopService;

        public CrawlDataCommon(ICrawlDataGearvnService crawlDataGearvnService,
            ICrawlDataAnphat crawlDataAnphatService,
            ICrawlDataZShopService crawlDataZShopService)
        {
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataZShopService = crawlDataZShopService;
        }

        public PaginationDataModel getData(PaginationRequestModel req)
        {
            List<CrawlDataModel> result = new List<CrawlDataModel>();

            result.AddRange(_crawlDataAnphatService.CrawlData(req.Keyword));
            result.AddRange(_crawlDataGearvnService.CrawlData(req.Keyword));
            result.AddRange(_crawlDataZShopService.CrawlData(req.Keyword));

            if (req.IsAscending)
                result = result.OrderBy(x => x.NewPrice).ToList();
            else result.OrderByDescending(x => x.NewPrice);

            PaginationDataModel data = new PaginationDataModel(result, req);
            return data;
        }
    }
}
