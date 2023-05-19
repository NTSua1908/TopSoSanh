using TopSoSanh.DTO;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataCommon : ICrawlDataCommon
    {
        private readonly ICrawlDataGearvnService _crawlDataGearvnService;
        private readonly ICrawlDataAnphatService _crawlDataAnphatService;
        private readonly ICrawlDataZShopService _crawlDataZShopService;
        private readonly ICrawlDataAnkhangService _crawlDataAnkhangService;
        private readonly ApiDbContext _dbContext;

        public CrawlDataCommon(ICrawlDataGearvnService crawlDataGearvnService,
            ICrawlDataAnphatService crawlDataAnphatService,
            ICrawlDataZShopService crawlDataZShopService,
            ICrawlDataAnkhangService crawlDataAnkhangService, ApiDbContext dbContext)
        {
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataZShopService = crawlDataZShopService;
            _crawlDataAnkhangService = crawlDataAnkhangService;
            _dbContext = dbContext;
        }

        public PaginationDataModel<CrawlDataModel> getData(PaginationRequestModel req)
        {
            
            List<CrawlDataModel> result = new List<CrawlDataModel>();
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() =>
                {
                    result.AddRange(
                        _crawlDataAnphatService.CrawlData(
                            req.Keyword,
                            _crawlDataAnkhangService.GetPriceByName,
                            _crawlDataGearvnService.GetPriceByName
                        )
                     );
                })
            );
            tasks.Add(Task.Run(() =>
            {
                result.AddRange(
                    _crawlDataAnkhangService.CrawlData(
                        req.Keyword,
                        _crawlDataAnphatService.GetPriceByName,
                        _crawlDataGearvnService.GetPriceByName
                    )
                 );
            })
            );
            tasks.Add(Task.Run(() =>
            {
                result.AddRange(
                    _crawlDataGearvnService.CrawlData(
                        req.Keyword,
                        _crawlDataAnkhangService.GetPriceByName,
                        _crawlDataAnphatService.GetPriceByName
                    )
                 );
            })
            );

            Task.WaitAll(tasks.ToArray());

            //result.AddRange(_crawlDataGearvnService.CrawlData(req.Keyword));
            //result.AddRange(_crawlDataAnkhangService.CrawlData(req.Keyword));

            if (req.IsAscending)
                result = result.OrderBy(x => x.NewPrice).ToList();
            else result.OrderByDescending(x => x.NewPrice);

            PaginationDataModel<CrawlDataModel> data = new PaginationDataModel<CrawlDataModel>(result, req);
            GetComparision(data.Data);
            return data;
        }

        private void GetComparision(List<CrawlDataModel> result)
        {
            List<Task> tasks = new List<Task>();
            foreach (var item in result)
            {
                if (item.Shop != Shop.Anphat)
                    tasks.Add(Task.Run(() => _crawlDataAnphatService.GetPriceByName(item)));
                if (item.Shop != Shop.Ankhang)
                    tasks.Add(Task.Run(() => _crawlDataAnkhangService.GetPriceByName(item)));
                if (item.Shop != Shop.Gearvn)
                    tasks.Add(Task.Run(() => _crawlDataGearvnService.GetPriceByName(item)));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
