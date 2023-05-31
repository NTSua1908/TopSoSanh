using TopSoSanh.DTO;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Services.Implement
{
    public class LocationService : ILocationService
    {
        private readonly ApiDbContext _dbContext;
        private readonly UserResolverService _userResolverService;

        public LocationService(ApiDbContext dbContext, UserResolverService userResolverService)
        {
            _dbContext = dbContext;
            _userResolverService = userResolverService;
        }

        public Guid Add(LocationModel model)
        {
            Location location = model.ParseToEntity(_userResolverService.GetUser());
            _dbContext.Locations.Add(location);
            _dbContext.SaveChanges();
            return location.Id;
        }

        public void Update(Guid id, LocationModel model, ErrorModel errors)
        {
            if (ValidateLocation(id, errors, out Location location))
            {
                model.UpdateEntity(location);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id, ErrorModel errors)
        {
            if (ValidateLocation(id, errors, out Location location))
            {
                _dbContext.Locations.Remove(location);
                _dbContext.SaveChanges();
            }
        }

        public LocationModel GetDetail(Guid id, ErrorModel errors)
        {
            if (ValidateLocation(id, errors, out Location location))
            {
                return new LocationModel(location);
            }
            return new LocationModel();
        }

        public PaginationDataModel<LocationGetAllModel> GetAll(PaginationRequestModel req)
        {
            IEnumerable<Location> locations = _dbContext.Locations.Where(x => x.UserId == _userResolverService.GetUser());
            if (!string.IsNullOrEmpty(req.SearchText))
            {
                locations = locations.Where(x => x.Name.ToLower().Contains(req.SearchText.ToLower()) || x.Address.ToLower().Contains(req.SearchText.ToLower()));
            }
            return new PaginationDataModel<LocationGetAllModel>(locations.Select(x => new LocationGetAllModel(x)).ToList(), req);
        }

        private bool ValidateLocation(Guid id, ErrorModel errors, out Location location)
        {
            location = _dbContext.Locations.Find(id);
            if (location == null)
            {
                errors.Add(String.Format(ErrorResource.NotFound, "Location"));
            }
            return errors.IsEmpty;
        }
    }
}
