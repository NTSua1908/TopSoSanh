using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopSoSanh.Entity
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public double Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
