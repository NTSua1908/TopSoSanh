using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;
using TopSoSanh.DTO;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;
using static TopSoSanh.Helper.ConstanstHelper;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataAnkhangService : ICrawlDataAnkhangService
    {
        public List<CrawlDataModel> CrawlData(string keyword, Action<CrawlDataModel> GetPriceAnPhat, Action<CrawlDataModel> GetPriceGearvn)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.ankhang.vn/tim?q=" + keyword.Replace(" ", "+"));

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".product_list_box > .product-list li");
            List<Task> tasks = new List<Task>();

            foreach (var node in nodeItems)
            {
                try
                {
                    CrawlDataModel model = new CrawlDataModel(Shop.Ankhang);

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
                    if (crawlDataModels.Count >= CrawlConstant.Amount)
                        break;
                    //tasks.Add(Task.Run(() => GetPriceAnPhat(model)));
                    //tasks.Add(Task.Run(() => GetPriceGearvn(model)));
                    //Console.WriteLine(CrawlPrice(model.ItemUrl));
                }
                catch (Exception e) { };
            }
            //Task.WaitAll(tasks.ToArray());

            return crawlDataModels;
        }

        public void GetPriceByName(CrawlDataModel model)
        {
            Debug.WriteLine("+++++++++");
            string link = GetLinkByProductName(model.Name);
            //link = "https://www.ankhang.vn//laptop-dell-inspiron-3520-n3520-i5u085w11blu.html";
            model.PriceCompares.Add(new PriceCompare()
            {
                Shop = Shop.Ankhang,
                Url = link,
                Price = string.IsNullOrEmpty(link) ? 0 : CrawlPrice(link)
            });
            Debug.WriteLine("********");
        }

        private string GetLinkByProductName(string productName)
        {
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://www.google.com/search?q=\"{productName}\"+site:https://www.ankhang.vn/");
            var nodeItems = doc.DocumentNode.QuerySelectorAll("#main > div:nth-child(5) a");
            foreach (var item in nodeItems)
            {
                return item.Attributes["href"].Value;
            }

            return "";
        }

        public double CrawlPrice(string url)
        {
            //url = "https://www.anphatpc.com.vn/laptop-gaming-acer-nitro-5-an515-57-53f9-nh.qensv.008.html";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            double price;
            try
            {
                price = Double.Parse(
                    doc.DocumentNode.QuerySelector(".name_price_product_detail span.pro-price")?
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
