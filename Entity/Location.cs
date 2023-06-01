using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace TopSoSanh.Entity
{
    public class Location
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductTracking> ProductTrackings { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
