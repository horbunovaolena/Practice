using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1_HandmadeWorkshop
{
    public abstract class Order
    {
        public const int UrgentDaysThreshold = 5;
        protected const decimal UrgentPriceMultiplier = 1.2m;
        public const int MinDays = 1;
        public const int MaxDays = 365;

        public string ItemType { get; set; }
        public string Material { get; set; }
        public string Region { get; set; }
        public int Days { get; set; }
        public decimal BasePrice { get; set; }

        public string Technique => (ItemType, Region) switch
        {
            ("Одяг", "Полтава")    => "Вишивка біллю (білим по білому)",
            ("Одяг", "Гуцульщина") => "Низинка або кучерявий шов",
            ("Одяг", "Сучасний")   => "Машинна вишивка або принт",
            ("Посуд", "Полтава")   => "Розпис у полтавському стилі",
            ("Посуд", "Гуцульщина")=> "Гуцульська кераміка (орнамент)",
            ("Посуд", "Сучасний")  => "Глазурування та обпалення",
            _                      => "Техніка обирається майстром"
        };

        public string Category => (ItemType, Material) switch
        {
            ("Одяг", "Льон")        => "Автентичний одяг (старовинний стиль)",
            ("Одяг", "Шовк")        => "Святковий одяг",
            ("Одяг", "Бавовна")     => "Повсякденний текстильний виріб",
            ("Одяг", _)             => "Сучасний текстильний виріб",
            ("Посуд", "Глина")      => "Кераміка ручної роботи",
            ("Посуд", "Фаянс")      => "Фаянсовий посуд",
            ("Посуд", "Порцеляна")  => "Порцеляновий посуд",
            _                       => "Категорія не визначена"
        };

        public override string ToString()
        {
            return $@"
╔═══════════════════════════════════════╗
║      ВАШЕ ЗАМОВЛЕННЯ                  ║
╚═══════════════════════════════════════╝
Тип:          {ItemType}   
Матеріал:     {Material}
Регіон:       {Region}
Техніка:      {Technique}
Категорія:    {Category}
Термін:       {Days} днів
Ціна:         {CalculateFinalPrice():C}";
        }

        public abstract decimal CalculateFinalPrice();
      
        protected Order(string itemType, string material, string region, int days, decimal basePrice)
        {
            ItemType = itemType;
            Material = material;
            Region = region;
            Days = days;
            BasePrice = basePrice;
        }
    }
}
