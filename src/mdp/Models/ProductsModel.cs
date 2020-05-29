using System.Collections.Generic;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;

    public enum CategoryType
    {
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

    public class ProductComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
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
        public int Color { get; set; }
        public ICollection<string> Images { get; set; } = new List<string>();
        public ICollection<ProductComment> Comments { get; set; } = new List<ProductComment>();

    }

    public class ProductsModel : DbContext
    {
        public ProductsModel()
            : base("DefaultConnection")
        {
        }

        public static ProductsModel Create()
        {
            return new ProductsModel();
        }

        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}