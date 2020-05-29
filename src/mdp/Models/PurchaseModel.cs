using System.Collections.Generic;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public enum DeliveryType
    {
        Post,
        Courier
    }

    public class Order
    {
        public Product ProductId { get; set; }
        public int ProductAmount { get; set; }
    }

    public class Purchase
    {
        // could be an Anonymous one if User isn't registered
        public ApplicationUser UserId { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DeliveryType DelivType { get; set; }
        // could be empty in case of courier delivery
        public string DelivAddress { get; set; }
    }

    public class PurchaseModel : DbContext
    {
        public PurchaseModel()
            : base("DefaultConnection")
        {
        }

        public static PurchaseModel Create()
        {
            return new PurchaseModel();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}