using HtmlAgilityPack;
using System.Net;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using Fizzler.Systems.HtmlAgilityPack;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataZShopService : ICrawlDataZShopService
    {
        public List<CrawlDataModel> CrawlData(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://zshop.vn/?match=all&subcats=Y&pcode_from_q=Y&pshort=Y&pfull=Y&pname=Y&pkeywords=Y&search_performed=Y&q={keyword.Replace(" ", "+")}&dispatch=products.search&security_hash=b1dc74d1d655fdc0f5afee14ad856f9a");

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".grid-list .ypi-grid-list__item_body");

            foreach (var node in nodeItems)
            {
                if (!node.QuerySelector(".stock-block").InnerText.Contains("Tạm hết hàng") &&
                    !node.QuerySelector(".block-bottom").InnerText.Contains("Liên hệ để biết giá"))
                {
                    CrawlDataModel model = new CrawlDataModel();

                    model.Name = node.QuerySelector(".ty-grid-list__item-name").InnerText;
                    model.Price = node.QuerySelector(".block-bottom .ty-price span").InnerText;
                    model.ItemUrl = node.QuerySelector(".ty-grid-list__item-name a").Attributes["href"].Value;
                    model.ImageUrl = node.QuerySelector(".ty-grid-list__image .abt-single-image img").Attributes["data-srcset"].Value.Split(" ")[0];
                    crawlDataModels.Add(model);
                }
            }

            return crawlDataModels;
        }
    }
}
