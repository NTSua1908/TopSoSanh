using System.Collections.Generic;

namespace TopSoSanh.DTO
{
    public class PaginationDataModel<T>
    {
        public int PageNumber { get; set; }
        public int Quantity { get; set; }
        public int TotalPage { get; set; }
        public long TotalCount { get; set; }
        public string Keyword { get; set; }
        public bool IsAscending { get; set; }
        public List<T> Data { get; set; }

        public PaginationDataModel(List<T> data, PaginationRequestModel req)
        {
            this.Data = data.Skip((req.PageNumber-1) * req.Quantity).Take(req.Quantity).ToList();
            Quantity = req.Quantity;
            Keyword = req.Keyword;
            IsAscending = req.IsAscending;
            TotalCount = data.Count();
            TotalPage = (int)Math.Ceiling((decimal)this.TotalCount / Quantity);
            PageNumber = req.PageNumber;
        }
    }
}
