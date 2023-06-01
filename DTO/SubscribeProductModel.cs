namespace TopSoSanh.DTO
{
    public class SubscribeProductModel
    {
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public double Price { get; set; }
        public bool IsAutoOrder { get; set; }
        public Guid? LocationId { get; set; }
    }

    public class SubscribeProductCustomModel
    {
        public string ProductName { get; set; }
        public string ProductPriceSelector { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public double Price { get; set; }
    }
}
