using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataCommon : ICrawlDataCommon
    {
        private readonly ICrawlDataGearvnService _crawlDataGearvnService;
        private readonly ICrawlDataAnphatService _crawlDataAnphatService;
        private readonly ICrawlDataZShopService _crawlDataZShopService;
        private readonly ICrawlDataAnkhangService _crawlDataAnkhangService;

        public CrawlDataCommon(ICrawlDataGearvnService crawlDataGearvnService,
            ICrawlDataAnphatService crawlDataAnphatService,
            ICrawlDataZShopService crawlDataZShopService,
            ICrawlDataAnkhangService crawlDataAnkhangService)
        {
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataZShopService = crawlDataZShopService;
            _crawlDataAnkhangService = crawlDataAnkhangService;
        }

        public PaginationDataModel getData(PaginationRequestModel req)
        {
            List<CrawlDataModel> result = new List<CrawlDataModel>();

            result.AddRange(_crawlDataAnphatService.CrawlData(req.Keyword));
            result.AddRange(_crawlDataGearvnService.CrawlData(req.Keyword));
            result.AddRange(_crawlDataAnkhangService.CrawlData(req.Keyword));

            if (req.IsAscending)
                result = result.OrderBy(x => x.NewPrice).ToList();
            else result.OrderByDescending(x => x.NewPrice);

            PaginationDataModel data = new PaginationDataModel(result, req);
            GetComparision(data.Data);
            return data;
        }

        private void GetComparision(List<CrawlDataModel> result)
        {
            foreach (var item in result)
            {
                if (item.ShopName != Helper.ShopName.Anphat)
                    item.PriceCompares.Add(_crawlDataAnphatService.GetPriceByName(item.Name));
                if (item.ShopName != Helper.ShopName.Ankhang)
                    item.PriceCompares.Add(_crawlDataAnkhangService.GetPriceByName(item.Name));
                if (item.ShopName != Helper.ShopName.Gearvn)
                    item.PriceCompares.Add(_crawlDataGearvnService.GetPriceByName(item.Name));
            }
        }
    }
}
