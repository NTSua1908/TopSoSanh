﻿using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Net;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using TopSoSanh.Helper;
using System.Reflection;
using System.Xml.Linq;

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
                CrawlDataModel model = new CrawlDataModel(ShopName.Gearvn);

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
            }

            return crawlDataModels;
        }

        public CrawlDetailModel CrawlDetail(string url)
        {
            CrawlDetailModel crawlDetailModel = new CrawlDetailModel();
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load(url);

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

            return crawlDetailModel;
        }

    }
}
