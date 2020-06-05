using System.Collections.Generic;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;

    public enum CategoryType
    {
        //������
        sets,
        // �����������
        Controllers,
        // ������, �����
        Modules_Shields,
        // �������, �������
        Sensors,
        // ���������� ��������
        Motor_control,
        // ����������������
        Servo_controllers,
        // ������������
        Communication,
        // �������
        Displays,
        // ����������
        Components,
        // �������������� ����������
        Voltage_probes,
        // ��������� �������
        Power_supplies,
        // �������, ������
        Wires_cables,
        // �������������
        Electric_motors,
        // ������������
        Servos,
        // ������
        Wheels,
        // ��������
        Caterpillars,
        // ������� �����
        Ball_bearings,
        // ���������
        Platforms,
        // �����
        Chassis,
        // ������
        Fasteners
    }

    public enum ProductColor
    {
        White,
        Black,
        Green,
        Yellow,
        Brown,
        Blue,
        Orange,
        Red,
        Purple,
        Pink,
        Grey
    }

    public class ProductComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public CategoryType CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Vendor { get; set; }
        public int Amount { get; set; }
        public DateTime DateAdded { get; set;}
        public float Price { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public ProductColor Color { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductComment> Comments { get; set; } = new List<ProductComment>();
    }

    public class ProductSearch
    {
        public int ProductCategory { get; set; } = -1;
        public string SearchText { get; set; } = "";
    }

    public class ProductsModel : DbContext
    {
        public ProductsModel()
            : base("DefaultConnection")
        { }

        public static ProductsModel Create()
        {
            return new ProductsModel();
        }

        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}