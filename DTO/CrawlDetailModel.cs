namespace TopSoSanh.DTO
{
    public class CrawlDetailModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public List<KeyValuePair<string, string>> Description { get; set; }
    }
}
