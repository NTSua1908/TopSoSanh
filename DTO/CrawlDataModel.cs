using TopSoSanh.Helper;

namespace TopSoSanh.DTO
{
    public class CrawlDataModel
    {
        public string Name { get; set; }
        public string ItemUrl { get; set; }
        public string ImageUrl { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public ShopName ShopName { get; set; }
        //public double DiscountPercent { get; set; }
        //public string Location { get; set; } = "Unknown";

        public CrawlDataModel() { }

        public CrawlDataModel(ShopName shopName)
        {
            this.ShopName = shopName;
        }
    }
}
