using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopSoSanh.Entity
{
    public class PriceFluctuation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Hour { get; set; }
        public virtual Product Product { get; set; }
    }
}
