using HtmlAgilityPack;
using System.Net;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text;
using TopSoSanh.Helper;
using System.Reflection;
using System.Xml.Linq;

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
                try
                {
                    if (!node.QuerySelector(".stock-block").InnerText.Contains("Tạm hết hàng") &&
                        !node.QuerySelector(".block-bottom").InnerText.Contains("Liên hệ để biết giá"))
                    {
                        CrawlDataModel model = new CrawlDataModel();

                        model.Name = node.QuerySelector(".ty-grid-list__item-name").InnerText;
                        model.OldPrice = Double.Parse(
                            node.QuerySelector(".ty-strike span.ty-list-price")?
                            .InnerText
                            .GetNumbers() ?? "0"
                        );
                        model.NewPrice = Double.Parse(
                            node.QuerySelector(".block-bottom .ty-price span")
                            .InnerText
                            .GetNumbers()
                        );
                        model.ItemUrl = node.QuerySelector(".ty-grid-list__item-name a").Attributes["href"].Value;
                        model.ImageUrl = node.QuerySelector(".ty-grid-list__image .abt-single-image img").Attributes["data-srcset"].Value.Split(" ")[0];
                        crawlDataModels.Add(model);
                    }
                }
                catch (Exception e) { };
            }

            return crawlDataModels;
        }

        public CrawlDetailModel CrawlDetail(string url)
        {
            CrawlDetailModel crawlDetailModel = new CrawlDetailModel();
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load(url);

            try
            {
                crawlDetailModel.Name = doc.DocumentNode.QuerySelector("h1.ty-product-block-title").InnerText.Trim();
                //crawlDetailModel.Price = doc.DocumentNode.QuerySelector("span.ty-price span.ty-price-num").InnerText.Trim();
                crawlDetailModel.OldPrice = Double.Parse(
                    doc.DocumentNode.QuerySelector(".ty-strike span.ty-list-price")?
                    .InnerText
                    .GetNumbers() ?? "0"
                );
                crawlDetailModel.NewPrice = Double.Parse(
                    doc.DocumentNode.QuerySelector("span.ty-price span.ty-price-num")
                    .InnerText
                    .GetNumbers()
                );
                crawlDetailModel.Description = new List<KeyValuePair<string, string>>();

                var descriptionNodes = doc.DocumentNode.QuerySelectorAll("div#content_features .ty-product-feature");

                foreach (var descriptionNode in descriptionNodes)
                {
                    if (descriptionNode.QuerySelector(".ty-compare-checkbox") != null)
                        continue;
                    //KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(
                    //    HtmlTaker.getContent(new StringBuilder(descriptionNode.QuerySelector(".ty-product-feature__label").InnerText)).ToString().RemoveBreakLineTab(),
                    //    HtmlTaker.getContent(new StringBuilder(descriptionNode.QuerySelector(".ty-product-feature__value").InnerText)).ToString().RemoveBreakLineTab()
                    //);
                    KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(
                        descriptionNode.QuerySelector(".ty-product-feature__label").InnerText.RemoveBreakLineTab(),
                        descriptionNode.QuerySelector(".ty-product-feature__value").InnerText.RemoveBreakLineTab()
                    );
                    crawlDetailModel.Description.Add(keyValuePair);
                }
            }
            catch (Exception e) { };

            return crawlDetailModel;
        }
    }
}
