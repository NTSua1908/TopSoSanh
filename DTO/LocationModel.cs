using System.ComponentModel.DataAnnotations;
using TopSoSanh.Entity;

namespace TopSoSanh.DTO
{
    public class LocationModel
    {
        [Range(-90, 90, ErrorMessage = "Latitude is invalid")]
        public double Latitude { get; set; }
        [Range(-180, 180, ErrorMessage = "Longitude is invalid")]
        public double Longitude { get; set; }
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public LocationModel() { }

        public LocationModel(Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            PhoneNumber = location.PhoneNumber;
            Name = location.Name;
            Address = location.Address;
        }

        public Location ParseToEntity(string userId)
        {
            return new Location()
            {
                Latitude = Latitude,
                Longitude = Longitude,
                PhoneNumber = PhoneNumber,
                Name = Name,
                Address = Address,
                UserId = userId
            };
        }

        public void UpdateEntity(Location location)
        {
            location.Latitude = Latitude;
            location.Longitude = Longitude;
            location.PhoneNumber = PhoneNumber;
            location.Name = Name;
            location.Address = Address;
        }
    }

    public class LocationGetAllModel : LocationModel
    {
        public Guid Id { get; set; }

        public LocationGetAllModel(Location location) : base(location)
        {
            Id = location.Id;
        }
    }
}
