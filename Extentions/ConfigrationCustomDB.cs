using Microsoft.EntityFrameworkCore;
using TopSoSanh.Entity;

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

            builder.Entity<ProductTracking>(productTracking =>
            {
                productTracking.HasOne(ur => ur.Product)
                    .WithMany(r => r.ProductTrackings)
                    .HasForeignKey(ur => ur.ProductId)
                    .IsRequired();

                productTracking.HasOne(ur => ur.User)
                    .WithMany(r => r.ProductTrackings)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                productTracking.HasOne(ur => ur.Location)
                    .WithMany(r => r.ProductTrackings)
                    .HasForeignKey(ur => ur.LocationId)
                    .IsRequired();
            });
        }
    }
}
