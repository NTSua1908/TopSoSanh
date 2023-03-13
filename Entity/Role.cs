using Microsoft.AspNetCore.Identity;

namespace TopSoSanh.Entity
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRoleMap> UserRoleMaps { get; set; }
    }
}
