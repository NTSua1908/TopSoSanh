using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace TopSoSanh.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<UserRoleMap> UserRoleMaps { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
