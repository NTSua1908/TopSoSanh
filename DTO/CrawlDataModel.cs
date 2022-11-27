namespace TopSoSanh.DTO
{
    public class CrawlDataModel
    {
        public string Name { get; set; }
        public string ItemUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        //public double DiscountPercent { get; set; }
        //public string Location { get; set; } = "Unknown";

        public override string? ToString()
        {
            //return $"{Name}\n{Price}\n{DiscountPercent}\n{Location}\n{ItemUrl}\n{ImageUrl}\n--------------------";
            return $"{Name}\n{Price}\n{ItemUrl}\n{ImageUrl}\n--------------------";
        }
    }
}
