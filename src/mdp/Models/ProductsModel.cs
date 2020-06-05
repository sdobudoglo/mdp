using System.Collections.Generic;

namespace mdp.Models
{
    using System;
    using System.Data.Entity;

    public enum CategoryType
    {
        //Наборы
        Sets,
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
            //    ProductName = "Робот DFRobot MiniQ 4wd v2.0",
            //    Vendor = "DFRobot",
            //    Amount = 100,
            //    DateAdded = DateTime.Now,
            //    Price = 6730F,
            //    Discount = 0F,
            //    Description = "Семейство MiniQ это полноценная роботизированная обучающая платформа с хорошими характеристиками и низкой стоимостью. Этот маленький робот обеспечит большую часть того, что вам может понадобится. От начинающих до продвинутых пользователей, MiniQ 4WD позволит быстро начать работу в мире робототехники и в дальнейшем улучшать или адаптировать его в соответствии с вашими требованиями.\n" +
            //    "Робот MiniQ 4WD поставляется со встроенным микроконтроллером на базе Arduino Leonardo,\n" +
            //    "также включает в себя полезные модули,\n" +
            //    "такие как зуммер,\n" +
            //    "светодиод RGB,\n" +
            //    "энкодеры,\n" +
            //    "датчики линии,\n" +
            //    "датчики препятствия,\n" +
            //    "ИК - передатчик и компас.Помимо съемных 4 AA батарей или аккумуляторов,\n" +
            //    "вы можете установить литиевую батарею и использовать порт зарядки MiniQ.Этот удобный комплект позволит создать робота дистанционно управляемого или для следования по линии,\n" +
            //    "нахождение выхода из лабиринта,\n" +
            //    "преодоление препятствий и т.д.Этот робот использует одни из самых распространенных датчиков,\n" +
            //    "применяемых во множестве учебных пособий и руководств в интернете.Он поставляется полностью собранным и готовым к программированию.Библиотека очень полезных образцов кода и набор учебных материалов от DFRobot позволит Вам сразу начать работать.\n\n" +
            //    "Спецификация:\n" +
            //    "Загрузчик Arduino Leonardo\n" +
            //    "Micro USB для загрузки кода\n" +
            //    "Три удобные пользовательские кнопки\n" +
            //    "Пользовательские светодиоды\n" +
            //    "Светодиод RGB WS2812 для управления цветом только одним каналом\n" +
            //    "Зуммер для воспроизведения простых звуков\n" +
            //    "Два энкодера для замкнутого контура управления моторами\n" +
            //    "Встроенный компас может быть использован для грубого измерения ориентации\n" +
            //    "Массив из пяти сенсоров отражения ИК снизу, которые могут быть использованы для следования по линии или обнаружения края\n" +
            //    "Два инфракрасных передатчика и инфракрасный приемник для обнаружения препятствий и предотвращения столкновения\n" +
            //    "ИК - пульт дистанционного управления, позволяющий дистанционно управлять всеми функциями\n" +
            //    "Порт интерфейса IIC(Gadgeteer) для подключения всевозможных устройств\n" +
            //    "Размер(ДхШхВ): 115 x 110 x 45 мм\n" +
            //    "Вес: 400 гр\n\n" +
            //    "Комплектация:\n" +
            //    "Робот MiniQ: 1 шт\n" +
            //    "ИК - пульт: 1 шт\n" +
            //    "Кабель Micro USB: 1 шт\n" +
            //    "Кабель Gadgeteer: 1 шт\n" +
            //    "Пластиковая коробка: 1 шт\n\n" +
            //    "Необходимые принадлежности:\n" +
            //    "MiniQ приводится в действие 4 батареями формата AA, которые не включены в поставку.Мы рекомендуем использовать перезаряжающиеся аккумуляторы NiMH, такие как Аккумуляторная батарея NiMH AA 1,2 в.\n",
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