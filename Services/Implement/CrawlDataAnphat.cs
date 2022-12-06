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
    public class CrawlDataAnphat : ICrawlDataAnphat
    {
        public List<CrawlDataModel> CrawlData(string keyword)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.anphatpc.com.vn/tim?scat_id=&q=" + keyword.Replace(" ", "+"));

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".p-list-container > .p-item");

            foreach (var node in nodeItems)
            {
                CrawlDataModel model = new CrawlDataModel();

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

            return crawlDataModels;
        }
    }
}
