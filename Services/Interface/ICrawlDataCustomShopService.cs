namespace TopSoSanh.Services.Interface
{
    public interface ICrawlDataCustomShopService
    {
        double CrawlPrice(string url, string priceXPath);
    }
}
