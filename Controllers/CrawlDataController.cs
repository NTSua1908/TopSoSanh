﻿using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.Helper;
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
        private readonly ICrawlDataAnkhangService _crawlDataAnkhangService;
        private readonly ICrawlDataCustomShopService _crawlDataCustomShopService;
        private readonly ICrawlDataZShopService _crawlDataZShopService;
        private readonly ICrawlDataCommon _crawlDataCommon;
        private readonly ISendMailService _sendMailService;

        public CrawlDataController(ICrawlDataPhongVuService crawlDataPhongVuService, 
            ICrawlDataGearvnService crawlDataGearvnService, 
            ICrawlDataAnphatService crawlDataAnphatService,
            ICrawlDataZShopService crawlDataZShopService,
            ICrawlDataCustomShopService crawlDataCustomShopService,
            ICrawlDataCommon crawlDataCommon, 
            ICrawlDataAnkhangService crawlDataAnkhangService, 
            ISendMailService sendMailService)
        {
            _crawlDataPhongVuService = crawlDataPhongVuService;
            _crawlDataGearvnService = crawlDataGearvnService;
            _crawlDataAnphatService = crawlDataAnphatService;
            _crawlDataZShopService = crawlDataZShopService;
            _crawlDataCustomShopService = crawlDataCustomShopService;
            _crawlDataCommon = crawlDataCommon;
            _crawlDataAnkhangService = crawlDataAnkhangService;
            _sendMailService = sendMailService;
        }

        [HttpGet("Common")]
        public PaginationDataModel<CrawlDataModel> getData([FromQuery] PaginationRequestModel req)
        {
            req.Format();
            return _crawlDataCommon.getData(req);
        }

        [HttpGet("GetPriceOtherShop")]
        public List<PriceCompare> PriceCompares(string productName, Shop shop)
        {
            return _crawlDataCommon.GetPriceOtherShop(productName, shop);
        }

		//[HttpGet("phongvu")]
		//public async Task<List<CrawlDataModel>> getDataFromPhongVu(string keyword)
		//{
		//    return await _crawlDataPhongVuService.CrawlData(keyword);
		//}

		[HttpGet("Gearvn")]
        public List<CrawlDataModel> getDataFromGearVN(string keyword)
        {
            return _crawlDataGearvnService.CrawlData(keyword, _crawlDataAnkhangService.GetPriceByName, _crawlDataAnphatService.GetPriceByName);
        }

        [HttpGet("GearvnDetail")]
        public CrawlDetailModel getDetailFromGearVN(string url)
        {
            return _crawlDataGearvnService.CrawlDetail(url);
        }

        [HttpGet("AnPhat")]
        public List<CrawlDataModel> getDataFromAnphat(string keyword)
        {
            return _crawlDataAnphatService.CrawlData(keyword, _crawlDataAnkhangService.GetPriceByName, _crawlDataGearvnService.GetPriceByName);
        }

        [HttpGet("TestCrawlPriceNewShop")]
        public IActionResult TestCrawlPriceNewShop(string productUrl, string priceSelector)
        {
            double price = _crawlDataCustomShopService.CrawlPrice(productUrl, priceSelector);
            if (price == Double.MaxValue)
            {
                return BadRequest("Không thể lấy giá sản phẩm");
            }
            return Ok(price);
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
            return _crawlDataAnkhangService.CrawlData(keyword, _crawlDataAnphatService.GetPriceByName, _crawlDataGearvnService.GetPriceByName);
        }

        [HttpGet("Gearvn/CrawlPrice")]
        public double GearvnCrawlPrice(string productUrl)
        {
            return _crawlDataGearvnService.CrawlPrice(productUrl);
        }

        [HttpGet("SendDemo")]
        public void SendDemo()
        {
            _sendMailService.SendMailTrackingAsync(
                new Helper.MailContent()
                {
                    To = "pts.uit.group@gmail.com",
                    Subject = "Thông tin giảm giá"
                },
                "Thiện Sua",
                "https://www.ankhang.vn//ban-phim-co-logitech-game-pro-x.html",
                "Bàn phím cơ Logitech Game PRO X",
                "https://cdn.ankhang.vn/media/product/250_16633-ban-phim-co-logitech-game-pro-x.png",
                "https://localhost:7134/api/ProductTracking/UnSubscribe?email=123&token=123"
            );
        }
    }
}
