using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using Role = TopSoSanh.Entity.Role;

namespace TopSoSanh.Extentions
{
    public static class ConfigrationCustomDB
    {
        public static void ConfigrationRelationship(this ModelBuilder builder)
        {
            builder.Entity<Notification>(notification =>
            {
                notification.HasOne(n => n.Product)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.ProductId)
                .IsRequired();
                notification.HasOne(n => n.User)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.UserId)
                .IsRequired();
            });
            builder.Entity<PriceFluctuation>(price =>
            {
                price.HasOne(pr => pr.Product)
                .WithMany(p => p.PriceFluctuations)
                .HasForeignKey(pr => pr.ProductId)
                .IsRequired();
            });
            builder.Entity<UserRoleMap>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoleMaps)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoleMaps)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.Entity<Location>(location =>
            {
                location.HasOne(l => l.User)
                    .WithMany(x => x.Locations)
                    .HasForeignKey(x => x.UserId);
            });
            builder.Entity<Favorite>(favorite =>
            {
                favorite.HasKey(ur => new { ur.UserId, ur.ProductId });

                favorite.HasOne(ur => ur.Product)
                    .WithMany(r => r.Favorites)
                    .HasForeignKey(ur => ur.ProductId)
                    .IsRequired();

                favorite.HasOne(ur => ur.User)
                    .WithMany(r => r.Favorites)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

        public static void Seed(this ModelBuilder builder)
        {
            var roleAdminId = new string("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var roleUserId = new string("1B3D7E19-B1A5-4CA2-A491-54593FA16531");
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = roleAdminId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Id = roleUserId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            var adminId = new string("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    FirstName = DefaultAdmin.FirstName,
                    LastName = DefaultAdmin.LastName,
                    UserName = DefaultAdmin.Email,
                    NormalizedUserName = DefaultAdmin.Email.ToUpper(),
                    Email = DefaultAdmin.Email,
                    NormalizedEmail = DefaultAdmin.Email.ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, DefaultAdmin.Password),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = "ba637654-70af-4542-be74-08c7b7329679",
                }
            );

            builder.Entity<UserRoleMap>().HasData(
                new UserRoleMap
                {
                    RoleId = roleAdminId,
                    UserId = adminId
                }
            );
        }
    }
}
