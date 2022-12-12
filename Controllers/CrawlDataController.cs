using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class CrawlDataController : Controller
    {
        private readonly ICrawlDataPhongVuService _crawlDataPhongVuService;
        private readonly ICrawlDataGearvnService _crawlDataGearvnService;
        private readonly ICrawlDataAnphatService _crawlDataAnphatService;
        private readonly ICrawlDataZShopService _crawlDataZShopService;
        private readonly ICrawlDataAnkhangService _crawlDataAnkhangService;
        private readonly ICrawlDataCommon _crawlDataCommon;

        public CrawlDataController(ICrawlDataPhongVuService crawlDataPhongVuService, 
            ICrawlDataGearvnService crawlDataGearvnService, 
            ICrawlDataAnphatService crawlDataAnphatService,
            ICrawlDataZShopService crawlDataZShopService,
            ICrawlDataCommon crawlDataCommon, 
            ICrawlDataAnkhangService crawlDataAnkhangService)
        {
            _crawlDataPhongVuService = crawlDataPhongVuService;
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataZShopService = crawlDataZShopService;
            _crawlDataCommon = crawlDataCommon;
            _crawlDataAnkhangService = crawlDataAnkhangService;
        }

        [HttpGet("Common")]
        public PaginationDataModel getData([FromQuery] PaginationRequestModel req)
        {
            req.Format();
            return _crawlDataCommon.getData(req);
        }

        //[HttpGet("phongvu")]
        //public async Task<List<CrawlDataModel>> getDataFromPhongVu(string keyword)
        //{
        //    return await _crawlDataPhongVuService.CrawlData(keyword);
        //}

        [HttpGet("Gearvn")]
        public List<CrawlDataModel> getDataFromGearVN(string keyword)
        {
            return _crawlDataGearvnService.CrawlData(keyword);
        }

        [HttpGet("GearvnDetail")]
        public CrawlDetailModel getDetailFromGearVN(string url)
        {
            return _crawlDataGearvnService.CrawlDetail(url);
        }

        [HttpGet("AnPhat")]
        public List<CrawlDataModel> getDataFromAnphat(string keyword)
        {
            return _crawlDataAnphatService.CrawlData(keyword);
        }

        [HttpGet("Zshop")]
        public List<CrawlDataModel> getDataFromZShop(string keyword)
        {
            return _crawlDataZShopService.CrawlData(keyword);
        }

        [HttpGet("ZshopDetail")]
        public CrawlDetailModel getDetailFromZShop(string url)
        {
            return _crawlDataZShopService.CrawlDetail(url);
        }

        [HttpGet("Ankhang")]
        public List<CrawlDataModel> getDataFromAnkhang(string keyword)
        {
            return _crawlDataAnkhangService.CrawlData(keyword);
        }

    }
}
