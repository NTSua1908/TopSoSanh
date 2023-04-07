using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;
using TopSoSanh.Helper;
using System.Net;
using System;
using Microsoft.AspNetCore.Http;
using static TopSoSanh.Helper.ConstanstHelper;
using System.Diagnostics;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataAnphatService : ICrawlDataAnphatService
    {
        public List<CrawlDataModel> CrawlData(string keyword, Action<CrawlDataModel> GetPriceAnKhang, Action<CrawlDataModel> GetPriceGearvn)
        {
            List<CrawlDataModel> crawlDataModels = new List<CrawlDataModel>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.anphatpc.com.vn/tim?scat_id=&q=" + keyword.Replace(" ", "+"));

            var nodeItems = doc.DocumentNode.QuerySelectorAll(".p-list-container > .p-item");
            List<Task> tasks = new List<Task>();

            foreach (var node in nodeItems)
            {
                try
                {
                    CrawlDataModel model = new CrawlDataModel(Shop.Anphat);

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
                    if (crawlDataModels.Count >= CrawlConstant.Amount)
                        break;
                    //tasks.Add(Task.Run(() => GetPriceAnKhang(model)));
                    //tasks.Add(Task.Run(() => GetPriceGearvn(model)));
                    //GetPriceAnKhang(model);
                    //GetPriceGearvn(model);
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
            //link = "https://www.anphatpc.com.vn//laptop-acer-nitro-5-eagle-an515-57-54mv-nh.qensv.003-core-i5-11400h-8gb-512gb-rtx-3050-4gb-15.6-inch-fhd-win-11-den.html";
            model.PriceCompares.Add( new PriceCompare()
            {
                Shop = Shop.Anphat,
                Url = link,
                Price = string.IsNullOrEmpty(link) ? 0 : CrawlPrice(link)
            });
            Debug.WriteLine("********");
        }

        private string GetLinkByProductName(string productName)
        {
            HtmlWeb web = new HtmlWeb();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlDocument doc = web.Load($"https://www.google.com/search?q=\"{productName}\"+site:https://www.anphatpc.com.vn/");
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
            HtmlDocument doc = web.Load(url);
            double price;
            try
            {
                price = Double.Parse(
                    doc.DocumentNode.QuerySelector("div.pro_info-price-container > table > tr:nth-child(3) > td:nth-child(2) > span > strong")?
                .InnerText
                .GetNumbers() ??
                    doc.DocumentNode.QuerySelector("div.pro_info-price-container > table > tr:nth-child(2) > td:nth-child(2) > b")?
                .InnerText
                .GetNumbers() ??
                doc.DocumentNode.QuerySelector("div.pro_info-price-container > table  > tr:nth-child(1) > td:nth-child(2) > b")?
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
