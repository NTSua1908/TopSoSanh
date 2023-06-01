using System.ComponentModel.DataAnnotations;
using TopSoSanh.Entity;

namespace TopSoSanh.DTO
{
    public class LocationModel
    {
        [Required(ErrorMessage = "Province is required")]
        public string Province { get; set; }
        [Required(ErrorMessage = "District is required")]
        public string District { get; set; }
        [Required(ErrorMessage = "Commune is required")]
        public string Commune { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public LocationModel() { }

        public LocationModel(Location location)
        {
            Province = location.Province;
            District = location.District;
            Commune = location.Commune;
            Address = location.Address;
            PhoneNumber = location.PhoneNumber;
            Email = location.Email;
            Name = location.Name;
        }

        public Location ParseToEntity(string userId)
        {
            return new Location()
            {
                Province = Province,
                District = District,
                Commune = Commune,
                Address = Address,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Name = Name,
                UserId = userId
            };
        }

        public void UpdateEntity(Location location)
        {
            location.Province = Province;
            location.District = District;
            location.Commune = Commune;
            location.Address = Address;
            location.PhoneNumber = PhoneNumber;
            location.Email = Email;
            location.Name = Name;
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
