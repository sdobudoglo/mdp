using System.Collections.Generic;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;

    public enum CategoryType
    {
        // Контроллеры
        Controllers,
        // Модули, шилды
        Modules_Shields,
        // Датчики, сенсоры
        Sensors,
        // Управление моторами
        Motor_control,
        // Сервоконтроллеры
        Servo_controllers,
        // Коммуникация
        Communication,
        // Дисплеи
        Displays,
        // Компоненты
        Components,
        // Пробразователи напряжения
        Voltage_probes,
        // Источники питания
        Power_supplies,
        // Провода, кабели
        Wires_cables,
        // Электромоторы
        Electric_motors,
        // Сервоприводы
        Servos,
        // Колеса
        Wheels,
        // Гусеницы
        Caterpillars,
        // Шаровые опоры
        Ball_bearings,
        // Платформы
        Platforms,
        // Шасси
        Chassis,
        // Крепеж
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