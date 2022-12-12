using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopSoSanh.Entity
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public virtual Product Product { get; set; }
    }
}
