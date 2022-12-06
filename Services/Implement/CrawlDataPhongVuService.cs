using HtmlAgilityPack;
using System.Net;
using System.Xml;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.Json.Serialization;
using System.Text.Json;
using TopSoSanh.Helper;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataPhongVuService : ICrawlDataPhongVuService
    {
        public async Task<List<CrawlDataModel>> CrawlData(string keyword)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://phongvu.vn/_next/data/e2ZnaGHDQ996kXkMtC67m/default/desktop/search.json?router=productListing&query=man+hinh");
            response.EnsureSuccessStatusCode();
            var responseBody =  response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(responseBody.Result);
            var nodes = doc.RootElement.GetProperty("pageProps")
                .GetProperty("serverProducts")
                .GetProperty("products")
                .EnumerateArray();

            return null;
        }

        public List<CrawlDataModel> CrawlData2(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();

            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load("https://phongvu.vn/search?router=productListing&query=" + keyword.Replace(" ", "+"));
            var nodeItems = doc.DocumentNode.QuerySelectorAll(".product-card > .css-4rhdrh");

            foreach (var node in nodeItems)
            {
                CrawlDataModel model = new CrawlDataModel();

                model.Name = node.QuerySelector("a.css-pxdb0j:nth-child(2) .css-1kbost9  h3").InnerText;

                string price = "*";
                HtmlNode priceNode = node.QuerySelector("div[type='subtitle'][color='primary500']");
                if (priceNode != null)
                {
                    price = priceNode.InnerText;
                }

                double discount = 0;
                HtmlNode discountNode = node.QuerySelector("div[type='caption'][color='primary500']");
                if (discountNode != null)
                {
                    string discountPercent = discountNode.InnerText;
                    discount = double.Parse(discountPercent.Substring(1, discountPercent.Length - 2));
                }

                //model.Price = Double.Parse(price.GetNumbers());
                //model.DiscountPercent = discount;
                model.ItemUrl = "https://phongvu.vn" + node.QuerySelector("a.css-pxdb0j").Attributes["href"].Value;
                model.ImageUrl = node.QuerySelector("a.css-pxdb0j > .css-1v97aik  .css-1uzm8bv img").Attributes["src"].Value;
                crawlDataModels.Add(model);
            }

            return crawlDataModels;
        }
    }
}
