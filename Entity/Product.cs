﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopSoSanh.Helper;

namespace TopSoSanh.Entity
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ItemUrl { get; set; }
        public string ImageUrl { get; set; }
        public Shop Shop { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<PriceFluctuation> PriceFluctuations { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
