using TopSoSanh.DTO;
using TopSoSanh.DTO.User;

namespace TopSoSanh.Services.Interface
{
    public interface IUserService
    {
        Task<ErrorModel> Register(RegisterModel model, string hostName);
        Task<ErrorModel> ForgotPassword(string Email);
        void AddFavorite(ProductModel product);
		void RemoveFavorite(string productUrl, ErrorModel errors);
        PaginationDataModel<ProductModel> GetFavoriteProductByToken(PaginationRequestModel req);
	}
}
