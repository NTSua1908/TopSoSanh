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
        private readonly ICrawlDataTheGioDiDongService _crawlDataTheGioDiDongService;
        private readonly ICrawlDataZShopService _crawlDataZShopService;

        public CrawlDataController(ICrawlDataPhongVuService crawlDataPhongVuService, 
            ICrawlDataGearvnService crawlDataGearvnService, 
            ICrawlDataTheGioDiDongService crawlDataTheGioDiDongService,
            ICrawlDataZShopService crawlDataZShopService)
        {
            _crawlDataPhongVuService = crawlDataPhongVuService;
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataTheGioDiDongService = crawlDataTheGioDiDongService;
            _crawlDataZShopService = crawlDataZShopService;
        }

        [HttpGet("phongvu")]
        public async Task<List<CrawlDataModel>> getDataFromPhongVu(string keyword)
        {
            return await _crawlDataPhongVuService.CrawlData(keyword);
        }

        [HttpGet("gearvn")]
        public List<CrawlDataModel> getDataFromGearVN(string keyword)
        {
            return _crawlDataGearvnService.CrawlData(keyword);
        }

        [HttpGet("gearvnDetail")]
        public CrawlDetailModel getDetailFromGearVN(string url)
        {
            return _crawlDataGearvnService.CrawlDetail(url);
        }

        [HttpGet("thegioididong")]
        public List<CrawlDataModel> getDataFromTheGioiDiDong(string keyword)
        {
            return _crawlDataTheGioDiDongService.CrawlData(keyword);
        }

        [HttpGet("zshop")]
        public List<CrawlDataModel> getDataFromZShop(string keyword)
        {
            return _crawlDataZShopService.CrawlData(keyword);
        }

        [HttpGet("zshopDetail")]
        public CrawlDetailModel getDetailFromZShop(string url)
        {
            return _crawlDataZShopService.CrawlDetail(url);
        }
    }
}
