using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Net;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataGearvnService : ICrawlDataGearvnService
    {
        public List<CrawlDataModel> CrawlData(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://gearvn.com/search?type=product&q=filter=((title%3Aproduct%20adjacent%20{keyword.Replace(" ", "%20")}))");

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".product-list .product-row");

            foreach (var node in nodeItems)
            {
                CrawlDataModel model = new CrawlDataModel();

                model.Name = node.QuerySelector(".product-row-name").InnerText;
                model.Price = node.QuerySelector(".product-row-price > .product-row-sale").InnerText;
                model.ItemUrl = "https://gearvn.com" + node.QuerySelector("a").Attributes["href"].Value;
                model.ImageUrl = "https:" + node.QuerySelector(".product-row-img .product-row-thumbnail").Attributes["src"].Value;
                crawlDataModels.Add(model);
            }

            return crawlDataModels;
        }

    }
}
