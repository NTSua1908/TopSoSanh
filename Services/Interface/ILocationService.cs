using TopSoSanh.DTO;

namespace TopSoSanh.Services.Interface
{
    public interface ILocationService
    {
        Guid Add(LocationModel model);
        void Update(Guid id, LocationModel model, ErrorModel errors);
        void Delete(Guid id, ErrorModel errors);
        LocationModel GetDetail(Guid id, ErrorModel errors);
        PaginationDataModel<LocationGetAllModel> GetAll(PaginationRequestModel req);
    }
}
