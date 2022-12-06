namespace TopSoSanh.DTO
{
    public class CrawlDetailModel
    {
        public string Name { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public List<KeyValuePair<string, string>> Description { get; set; }
    }
}
