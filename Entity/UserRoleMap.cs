using Microsoft.AspNetCore.Identity;

namespace TopSoSanh.Entity
{
    public class UserRoleMap : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
