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
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://gearvn.com/search?type=product&q=filter=((title%3Aproduct%20adjacent%20{keyword.Replace(" ", "%20")}))");

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".search-list-results > .proloop");
            List<Task> tasks = new List<Task>();

            foreach (var node in nodeItems)
            {
                try
                {
                    CrawlDataModel model = new CrawlDataModel(Shop.Gearvn);

                    model.Name = node.QuerySelector(".proloop-name a").InnerText;
                    model.OldPrice = Double.Parse(
                        node.QuerySelector(".proloop-price  del")?
                        .InnerText
                        .GetNumbers() ?? "0"
                    );
                    model.NewPrice = node.QuerySelector(".proloop-price .proloop-price--highlight") != null ? Double.Parse(
                        node.QuerySelector(".proloop-price .proloop-price--highlight")
                        .InnerText
                        .GetNumbers()) :
						Double.Parse(
						node.QuerySelector(".proloop-price .proloop-price--normal")
						.InnerText
						.GetNumbers());
                    model.ItemUrl = "https://gearvn.com" + node.QuerySelector("a").Attributes["href"].Value;
                    model.ImageUrl = "https:" + node.QuerySelector("picture > img.img-default").Attributes["data-src"].Value;
                    crawlDataModels.Add(model);
                    if (crawlDataModels.Count >= CrawlConstant.Amount)
                        break;
                    //tasks.Add(Task.Run(() => GetPriceAnKhang(model)));
                    //tasks.Add(Task.Run(() => GetPriceAnPhat(model)));
                }
                catch (Exception e) { };
            }

            //Task.WaitAll(tasks.ToArray());

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
            string link = GetLinkByProductName(model.Name);
            //link = "https://gearvn.com/products/laptop-gaming-acer-nitro-5-eagle-an515-57-53f9";
            model.PriceCompares.Add(new PriceCompare()
            {
                Shop = Shop.Gearvn,
                Url = link,
                Price = string.IsNullOrEmpty(link) ? 0 : CrawlPrice(link)
            });
        }

		public void GetPriceByName(string productName, List<PriceCompare> priceCompares)
		{
			string link = GetLinkByProductName(productName);
			priceCompares.Add(new PriceCompare()
			{
				Shop = Shop.Anphat,
				Url = link,
				Price = string.IsNullOrEmpty(link) ? 0 : CrawlPrice(link)
			});
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
                if (doc.DocumentNode.QuerySelector(".proloop-img > a") != null)
                {
                    string redirectUrl = "https://gearvn.com" + doc.DocumentNode.QuerySelector(".proloop-img > a").Attributes["href"].Value;
                    doc = web.Load(redirectUrl);
                }
				price = Double.Parse(
                        doc.DocumentNode.QuerySelector(".product-price .pro-price")
                        .InnerText
                        .GetNumbers());

            }
            catch (Exception e)
            {
                price = 0;
                Console.WriteLine(e.Message);
            }

            return price;
        }
    }
}
