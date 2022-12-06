namespace TopSoSanh.DTO
{
    public class PaginationDataModel
    {
        public int PageNumber { get; set; }
        public int Quantity { get; set; }
        public string Keyword { get; set; }
        public bool IsAscending { get; set; }
        public List<CrawlDataModel> Data { get; set; }

        public PaginationDataModel(List<CrawlDataModel> data, PaginationRequestModel req)
        {
            this.Data = data.Skip((req.PageNumber-1) * req.Quantity).Take(req.Quantity).ToList();
            Quantity = req.Quantity;
            Keyword = req.Keyword;
            IsAscending = req.IsAscending;

        }
    }
}
