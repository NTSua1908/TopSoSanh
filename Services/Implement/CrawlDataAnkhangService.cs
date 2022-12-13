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
                    //Console.WriteLine(CrawlPrice(model.ItemUrl));
                }
                catch (Exception e) { };
            }

            return crawlDataModels;
        }

        public double CrawlPrice(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            double price;
            try
            {
                price = Double.Parse(
                    doc.DocumentNode.QuerySelector("#price_deal_detail_2 > div.img_price_full")?
                    .InnerText
                    .GetNumbers() ??
                    doc.DocumentNode.QuerySelector("#overview-left > table > tbody > tr:nth-child(2) > td:nth-child(2) > span.pro-price")?
                    .InnerText
                    .GetNumbers() ??
                    doc.DocumentNode.QuerySelector("#overview-left > table > tbody > tr > td:nth-child(2) > span.pro-price")?
                    .InnerText
                    .GetNumbers() ??
                    Double.MaxValue.ToString()
                );
            }
            catch (Exception e)
            {
                price = Double.MaxValue;
                Console.WriteLine(e.Message);
            }

            return price;
        }
    }
}
