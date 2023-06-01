using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TopSoSanh.Helper;
using System.ComponentModel;

namespace TopSoSanh.Entity
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        //Order information
        public string Province { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderEmail { get; set; }
        public string OrderName { get; set; }

        public double Price { get; set; }
        public bool IsAutoOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public NotificationType NotificationType { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
