using System.Collections.Generic;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;

    public enum CategoryType
    {
        //������
        Sets,
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
        {
            // code below just for Product table creation

            //var comment1 = new ProductComment { Text = "First comment" };
            //var comment2 = new ProductComment { Text = "Second comment" };

            //var image1 = new ProductImage { ImagePath = "~/App_Data/product_images/1/image1.jpeg" };
            //var image2 = new ProductImage { ImagePath = "~/App_Data/product_images/1/image2.jpeg" };

            //var product = new Product
            //{
            //    CategoryId = CategoryType.Sets,
            //    ProductName = "����� DFRobot MiniQ 4wd v2.0",
            //    Vendor = "DFRobot",
            //    Amount = 100,
            //    DateAdded = DateTime.Now,
            //    Price = 6730F,
            //    Discount = 0F,
            //    Description = "��������� MiniQ ��� ����������� ���������������� ��������� ��������� � �������� ���������������� � ������ ����������. ���� ��������� ����� ��������� ������� ����� ����, ��� ��� ����� �����������. �� ���������� �� ����������� �������������, MiniQ 4WD �������� ������ ������ ������ � ���� ������������� � � ���������� �������� ��� ������������ ��� � ������������ � ������ ������������.\n" +
            //    "����� MiniQ 4WD ������������ �� ���������� ����������������� �� ���� Arduino Leonardo,\n" +
            //    "����� �������� � ���� �������� ������,\n" +
            //    "����� ��� ������,\n" +
            //    "��������� RGB,\n" +
            //    "��������,\n" +
            //    "������� �����,\n" +
            //    "������� �����������,\n" +
            //    "�� - ���������� � ������.������ ������� 4 AA ������� ��� �������������,\n" +
            //    "�� ������ ���������� �������� ������� � ������������ ���� ������� MiniQ.���� ������� �������� �������� ������� ������ ������������ ������������ ��� ��� ���������� �� �����,\n" +
            //    "���������� ������ �� ���������,\n" +
            //    "����������� ����������� � �.�.���� ����� ���������� ���� �� ����� ���������������� ��������,\n" +
            //    "����������� �� ��������� ������� ������� � ���������� � ���������.�� ������������ ��������� ��������� � ������� � ����������������.���������� ����� �������� �������� ���� � ����� ������� ���������� �� DFRobot �������� ��� ����� ������ ��������.\n\n" +
            //    "������������:\n" +
            //    "��������� Arduino Leonardo\n" +
            //    "Micro USB ��� �������� ����\n" +
            //    "��� ������� ���������������� ������\n" +
            //    "���������������� ����������\n" +
            //    "��������� RGB WS2812 ��� ���������� ������ ������ ����� �������\n" +
            //    "������ ��� ��������������� ������� ������\n" +
            //    "��� �������� ��� ���������� ������� ���������� ��������\n" +
            //    "���������� ������ ����� ���� ����������� ��� ������� ��������� ����������\n" +
            //    "������ �� ���� �������� ��������� �� �����, ������� ����� ���� ������������ ��� ���������� �� ����� ��� ����������� ����\n" +
            //    "��� ������������ ����������� � ������������ �������� ��� ����������� ����������� � �������������� ������������\n" +
            //    "�� - ����� �������������� ����������, ����������� ������������ ��������� ����� ���������\n" +
            //    "���� ���������� IIC(Gadgeteer) ��� ����������� ������������ ���������\n" +
            //    "������(�����): 115 x 110 x 45 ��\n" +
            //    "���: 400 ��\n\n" +
            //    "������������:\n" +
            //    "����� MiniQ: 1 ��\n" +
            //    "�� - �����: 1 ��\n" +
            //    "������ Micro USB: 1 ��\n" +
            //    "������ Gadgeteer: 1 ��\n" +
            //    "����������� �������: 1 ��\n\n" +
            //    "����������� ��������������:\n" +
            //    "MiniQ ���������� � �������� 4 ��������� ������� AA, ������� �� �������� � ��������.�� ����������� ������������ ���������������� ������������ NiMH, ����� ��� �������������� ������� NiMH AA 1,2 �.\n",
            //    Color = ProductColor.Black,
            //    Images = { image1, image2 },
            //    Comments = { comment1, comment2 }
            //};

            //ProductComments.Add(comment1);
            //ProductComments.Add(comment2);

            //ProductImages.Add(image1);
            //ProductImages.Add(image2);

            //Products.Add(product);
            //SaveChanges();
        }

        public static ProductsModel Create()
        {
            return new ProductsModel();
        }

        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}