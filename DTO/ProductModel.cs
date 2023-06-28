using TopSoSanh.Entity;
using TopSoSanh.Helper;

namespace TopSoSanh.DTO
{
	public class ProductModel
	{
		public string ProductName { get; set; }
		public string ProductUrl { get; set; }
		public string ImageUrl { get; set; }
		public Shop Shop { get; set; }

		public ProductModel() { }	

		public ProductModel(Product product) 
		{
			ProductName = product.Name;
			ProductUrl = product.ItemUrl;
			ImageUrl = product.ImageUrl;
			Shop = product.Shop;
		}
	}
}
