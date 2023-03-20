using System.ComponentModel.DataAnnotations;

namespace TopSoSanh.DTO.User
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
