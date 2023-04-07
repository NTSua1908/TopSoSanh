using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Net;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using TopSoSanh.Helper;
using System.Reflection;
using System.Xml.Linq;
using System.Diagnostics;
using static TopSoSanh.Helper.ConstanstHelper;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataGearvnService : ICrawlDataGearvnService
    {
        public List<CrawlDataModel> CrawlData(string keyword, Action<CrawlDataModel> GetPriceAnKhang, Action<CrawlDataModel> GetPriceAnPhat)
        {
            Debug.WriteLine("=====");
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://gearvn.com/search?type=product&q=filter=((title%3Aproduct%20adjacent%20{keyword.Replace(" ", "%20")}))");

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".product-list .product-row");
            List<Task> tasks = new List<Task>();

            foreach (var node in nodeItems)
            {
                try
                {
                    CrawlDataModel model = new CrawlDataModel(Shop.Gearvn);

                    model.Name = node.QuerySelector(".product-row-name").InnerText;
                    model.OldPrice = Double.Parse(
                        node.QuerySelector(".product-row-price > del")?
                        .InnerText
                        .GetNumbers() ?? "0"
                    );
                    model.NewPrice = Double.Parse(
                        node.QuerySelector(".product-row-price > .product-row-sale")
                        .InnerText
                        .GetNumbers()
                    );
                    model.ItemUrl = "https://gearvn.com" + node.QuerySelector("a").Attributes["href"].Value;
                    model.ImageUrl = "https:" + node.QuerySelector(".product-row-img .product-row-thumbnail").Attributes["src"].Value;
                    crawlDataModels.Add(model);
                    if (crawlDataModels.Count >= CrawlConstant.Amount)
                        break;
                    //tasks.Add(Task.Run(() => GetPriceAnKhang(model)));
                    //tasks.Add(Task.Run(() => GetPriceAnPhat(model)));
                }
                catch (Exception e) { };
            }

            //Task.WaitAll(tasks.ToArray());
            Debug.WriteLine("-----");

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
                crawlDetailModel.Name = doc.DocumentNode.QuerySelector("h1.product_name").InnerText.Trim();
                crawlDetailModel.OldPrice = Double.Parse(
                        doc.DocumentNode.QuerySelector(".product_sales_off > .product_price")?
                        .InnerText
                        .GetNumbers() ?? "0"
                );
                crawlDetailModel.NewPrice = Double.Parse(
                    doc.DocumentNode.QuerySelector(".product_sales_off > .product_sale_price")
                    .InnerText
                    .GetNumbers()
                );
                crawlDetailModel.Description = new List<KeyValuePair<string, string>>();

                var descriptionNodes = doc.DocumentNode.QuerySelectorAll("div.tab-content .tab-pane table tr");

                foreach (var descriptionNode in descriptionNodes)
                {
                    List<string> descriptionTag = descriptionNode.QuerySelectorAll("span").Select(x => x.InnerText).ToList();
                    KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(descriptionTag[0].RemoveBreakLineTab(), descriptionTag[descriptionTag.Count - 1].RemoveBreakLineTab());
                    crawlDetailModel.Description.Add(keyValuePair);
                }
            }
            catch (Exception e) { };
            return crawlDetailModel;
        }

        public void GetPriceByName(CrawlDataModel model)
        {
            Debug.WriteLine("+++++++++");
            string link = GetLinkByProductName(model.Name);
            //link = "https://gearvn.com/products/laptop-gaming-acer-nitro-5-eagle-an515-57-53f9";
            model.PriceCompares.Add(new PriceCompare()
            {
                Shop = Shop.Gearvn,
                Url = link,
                Price = string.IsNullOrEmpty(link) ? 0 : CrawlPrice(link)
            });
            Debug.WriteLine("********");
        }

        private string GetLinkByProductName(string productName)
        {
            HtmlWeb web = new HtmlWeb();
            web.PreRequest = (request) =>
            {
                request.AllowAutoRedirect = true;
                return true;
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://www.google.com/search?q=\"{productName}\"+site:https://gearvn.com/");
            var nodeItems = doc.DocumentNode.QuerySelectorAll("#main > div:nth-child(5) a");
            foreach (var item in nodeItems)
            {
                return item.Attributes["href"].Value;
            }

            return "";
        }

        public double CrawlPrice(string url)
        {
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load(url);
            double price;
            try
            {
                price = Double.Parse(
                    doc.DocumentNode.QuerySelector(".product_sales_off > .product_sale_price")
                    .InnerText
                    .GetNumbers() ?? Double.MaxValue.ToString()
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
