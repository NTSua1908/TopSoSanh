using System.ComponentModel.DataAnnotations;
using TopSoSanh.Entity;
using TopSoSanh.Helper;

namespace TopSoSanh.DTO
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public Shop Shop { get; set; }
        public double Price { get; set; }
        public bool IsAutoOrder { get; set; }
        public bool IsActive { get; set; }

        public NotificationModel(Notification notification)
        {
            Id = notification.Id;
            Email = notification.Email;
            UserName = notification.UserName;
            ProductName = notification.Product.Name;
            ProductUrl = notification.Product.ItemUrl;
            ImageUrl = notification.Product.ImageUrl;
            Shop = notification.Product.Shop;
            Price = notification.Price;
            IsAutoOrder = notification.IsAutoOrder;
            IsActive = notification.IsActive;
        }
    }

    public class NotificationDetailModel
    {
        //Email information
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

        //Option
        public double Price { get; set; }
        public bool IsAutoOrder { get; set; }
        public bool IsActive { get; set; }

        //Product information
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public Shop Shop { get; set; }

        public NotificationDetailModel() { }

        public NotificationDetailModel(Notification notification)
        {
            Email = notification.Email;
            UserName = notification.UserName;
            Province = notification.Province;
            District = notification.District;
            Commune = notification.Commune;
            Address = notification.Address;
            PhoneNumber = notification.PhoneNumber;
            OrderEmail = notification.OrderEmail;
            OrderName = notification.OrderName;
            Price = notification.Price;
            IsAutoOrder = notification.IsAutoOrder;
            IsActive = notification.IsActive;
            ProductName = notification.Product.Name;
            ProductUrl = notification.Product.ItemUrl;
            ImageUrl = notification.Product.ImageUrl;
            Shop = notification.Product.Shop;
        }
    }

    public class NotificationUpdateModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }

        //Order information
        public string Province { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string Address { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string OrderEmail { get; set; }
        public string OrderName { get; set; }

        //Option
        [Range(0, Double.MaxValue, ErrorMessage = "Invalid price")]
        public double Price { get; set; }
        public bool IsAutoOrder { get; set; }
        public bool IsActive { get; set; }

        public void UpdateEntity(Notification notification)
        {
            notification.Email = Email;
            notification.UserName = UserName;
            notification.Province = Province;
            notification.District = District;
            notification.Commune = Commune;
            notification.Address = Address;
            notification.PhoneNumber = PhoneNumber;
            notification.OrderName = OrderName;
            notification.OrderEmail = OrderEmail;
            notification.Price = Price;
            notification.IsActive = IsActive;
            notification.IsAutoOrder = IsAutoOrder;
        }
    }
}
