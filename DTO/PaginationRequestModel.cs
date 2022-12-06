namespace TopSoSanh.DTO
{
    public class PaginationRequestModel
    {
        public int PageNumber { get; set; }
        public int Quantity { get; set; }
        public string Keyword { get; set; }
        public bool IsAscending { get; set; } = true;
        public void Format()
        {
            if (PageNumber == 0)
                PageNumber = 1;
            if (Quantity == 0)
                Quantity = 20;
        }
    }
}
