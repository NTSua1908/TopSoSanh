using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopSoSanh.Extentions;

namespace TopSoSanh.Entity
{
    public class ApiDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRoleMap, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public static ApiDbContext Create(DbContextOptions<ApiDbContext> options)
        {
            return new ApiDbContext(options);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);

            //Configration relationship
            builder.ConfigrationRelationship();
            //Seed
            builder.Seed();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PriceFluctuation> PriceFluctuations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleMap> UserRoleMaps { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ProductTracking> ProductTrackings { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
