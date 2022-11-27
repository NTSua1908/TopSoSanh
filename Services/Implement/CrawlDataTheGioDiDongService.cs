using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataTheGioDiDongService : ICrawlDataTheGioDiDongService
    {
        public List<CrawlDataModel> CrawlData(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://www.thegioididong.com/tim-kiem?key=" + keyword.Replace(" ", "+")).Result;
            var redirect = response.RequestMessage.RequestUri.AbsoluteUri;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(redirect);

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".listproduct > .item");

            foreach (var node in nodeItems)
            {
                CrawlDataModel model = new CrawlDataModel();

                model.Name = node.QuerySelector("a h3").InnerText.Replace("\n            ", "").Replace("\n        ", "");
                model.Price = node.QuerySelector(".price").InnerText;
                model.ItemUrl = "https://thegioididong.com" + node.QuerySelector("a").Attributes["href"].Value;
                model.ImageUrl = node.QuerySelector(".item-img img").Attributes["data-src"]?.Value ?? node.QuerySelector(".item-img img").Attributes["src"]?.Value;
                crawlDataModels.Add(model);
            }

            return crawlDataModels;
        }
    }
}
