using System.ComponentModel.DataAnnotations;
using TopSoSanh.Helper;

namespace TopSoSanh.DTO.User
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(ErrorResource))]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(ErrorResource))]
        public string Password { get; set; }
    }
}
