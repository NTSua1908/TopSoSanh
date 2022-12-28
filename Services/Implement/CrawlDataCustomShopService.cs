using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class CrawlDataCustomShopService : ICrawlDataCustomShopService
    {
        public double CrawlPrice(string url, string priceSelector)
        {
            HtmlWeb web = new HtmlWeb();
            double price;
            try
            {
                HtmlDocument doc = web.Load(url);
                string? priceString = doc.DocumentNode
                    .QuerySelector(priceSelector)?
                    .InnerText
                    .GetNumbers();

                if (priceString == null)
                {
                    price = Double.MaxValue;
                }
                else price = Double.Parse(priceString);
            }
            catch (Exception e)
            {
                price = Double.MaxValue;
            }

            return price;
        }
    }
}
