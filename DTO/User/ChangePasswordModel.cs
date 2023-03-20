using System.ComponentModel.DataAnnotations;
using TopSoSanh.Helper;

namespace TopSoSanh.DTO.User
{
    public class ChangePasswordModel
    {
        /// <summary>
        /// UserName of Account
        /// </summary>
        [Required(ErrorMessageResourceName = "OldPasswordRequired", ErrorMessageResourceType = typeof(ErrorResource))]
        public string OldPassword { get; set; }
        /// <summary>
        /// NewPassword of Account
        /// </summary>
        [Required(ErrorMessageResourceName = "NewPasswordRequired", ErrorMessageResourceType = typeof(ErrorResource))]
        public string NewPassword { get; set; }
        /// <summary>
        /// ConfirmPassword of Account
        /// </summary>
        [Required(ErrorMessageResourceName = "ConfirmPasswordRequired", ErrorMessageResourceType = typeof(ErrorResource))]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(ErrorResource))]
        public string ConfirmPassword { get; set; }
    }
}
