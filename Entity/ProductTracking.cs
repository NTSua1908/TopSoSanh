using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopSoSanh.Entity
{
    public class ProductTracking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }
        public double Price { get; set; }
        public bool IsAutoOrder { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        public virtual Location Location { get; set; }
    }
}
