using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1_HandmadeWorkshop
{
    public class Order
    {
        public const int UrgentDaysThreshold = 5;
        private const decimal UrgentPriceMultiplier = 1.2m;
        public const int MinDays = 1;
        public const int MaxDays = 365;

        public string ItemType { get; set; }
        public string Material { get; set; }
        public string Region { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }

        public string Technique => Region switch
        {

            "Полтава" => "Вишивка біллю (білим по білому)",
            "Гуцульщина" => "Низинка або кучерявий шов",
            "Сучасний" => "Машинна вишивка або принт",
            _ => "Техніка обирається майстром"
        };

        public string Category => (ItemType, Material) switch
        {
            ("Одяг", "Льон") => "Автентичний одяг (старовинний стиль)",
            ("Одяг", _) => "Сучасний текстильний виріб",
            ("Посуд", "Глина") => "Кераміка ручної роботи",
            _ => "Категорія не визначена"
        };

        public override string ToString()
        {
            return $@"
╔═══════════════════════════════════════╗
║      ВАШЕ ЗАМОВЛЕННЯ                  ║
╚═══════════════════════════════════════╝
Тип виробу:   {ItemType}
Матеріал:     {Material}
Регіон:       {Region}
Техніка:      {Technique}
Категорія:    {Category}
Термін:       {Days} днів
Ціна:         {CalculateFinalPrice():C}";
        }

        public decimal CalculateFinalPrice()
        {
            if (Days < UrgentDaysThreshold)
            {
                return Price * UrgentPriceMultiplier;
            }
            return Price;
        }
        public Order(string itemType, string material, string region, int days, decimal price)
        {
            ItemType = itemType;
            Material = material;
            Region = region;
            Days = days;
            Price = price;
        }
    }
}
