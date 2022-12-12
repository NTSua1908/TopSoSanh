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
        }
    }
}
