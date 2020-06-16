using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public enum DeliveryType
    {
        Post,
        Courier,
        Self
    }

    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductAmount { get; set; }
    }

    public class Purchase
    {
        public int Id { get; set; }
        // could be an Anonymous one if User isn't registered
        public string UserId { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DeliveryType DelivType { get; set; }
        // could be empty in case of courier delivery
        public string DelivAddress { get; set; }
    }

    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }

    public class OrderViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DeliveryType Delivery { get; set; }
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
        public DbSet<Cart> Carts { get; set; }
    }
}