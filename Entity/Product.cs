using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopSoSanh.Entity
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemUrl { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<PriceFluctuation> PriceFluctuations { get; set; }
    }
}
