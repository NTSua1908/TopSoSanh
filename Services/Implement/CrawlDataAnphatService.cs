using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using TopSoSanh.Helper;
using System.Net;
using System;
using Microsoft.AspNetCore.Http;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataAnphatService : ICrawlDataAnphatService
    {
        public List<CrawlDataModel> CrawlData(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.anphatpc.com.vn/tim?scat_id=&q=" + keyword.Replace(" ", "+"));

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".p-list-container > .p-item");

            foreach (var node in nodeItems)
            {
                try
                {
                    CrawlDataModel model = new CrawlDataModel(ShopName.Anphat);

                    model.Name = node.QuerySelector(".p-text .p-name h3").InnerText;
                    model.ItemUrl = "https://www.anphatpc.com.vn/" + node.QuerySelector(".p-text .p-name").Attributes["href"].Value;
                    model.OldPrice = Double.Parse(
                        node.QuerySelector(".price-container .p-old-price")?
                        .InnerText
                        .GetNumbers() ?? "0"
                    );
                    model.NewPrice = Double.Parse(
                        node.QuerySelector(".price-container .p-price")
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
