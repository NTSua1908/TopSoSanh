using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using TopSoSanh.DTO;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataAnkhangService : ICrawlDataAnkhangService
    {
        public List<CrawlDataModel> CrawlData(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.ankhang.vn/tim?q=" + keyword.Replace(" ", "+"));

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".product_list_box > .product-list li");

            foreach (var node in nodeItems)
            {
                try
                {
                    CrawlDataModel model = new CrawlDataModel(ShopName.Ankhang);

                    model.Name = node.QuerySelector(".p-name").InnerText;
                    model.ItemUrl = "https://www.ankhang.vn/" + node.QuerySelector(".p-img").Attributes["href"].Value;
                    model.OldPrice = Double.Parse(
                        node.QuerySelector(".p-oldprice")?
                        .InnerText
                        .GetNumbers() ?? "0"
                    );
                    model.NewPrice = Double.Parse(
                        node.QuerySelector(".p-price")
                        .InnerText
                        .GetNumbers()
                    );
                    model.ImageUrl = node.QuerySelector("a.p-img img").Attributes["data-src"]?.Value;
                    crawlDataModels.Add(model);
                }
                catch (Exception e) { };
            }

            return crawlDataModels;
        }
    }
}
