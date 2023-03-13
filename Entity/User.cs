using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace TopSoSanh.Entity
{
    public class User : IdentityUser
    {
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<ProductTracking> ProductTrackings { get; set; }
        public virtual ICollection<UserRoleMap> UserRoleMaps { get; set; }
    }
}
