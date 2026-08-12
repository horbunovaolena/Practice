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

        public string Material { get; set; }
        public string Region { get; set; }
        public int Days { get; set; }
        public decimal BasePrice { get; set; }

        public string Technique => (this, Region) switch //властивість поточного об'єкта; C# дивиться на його ТИП (ClothingOrder або PotteryOrder)
        {
            (ClothingOrder, "Полтава")    => "Вишивка біллю (білим по білому)",
            (ClothingOrder, "Гуцульщина") => "Низинка або кучерявий шов",
            (ClothingOrder, "Сучасний")   => "Машинна вишивка або принт",
            (PotteryOrder, "Полтава")     => "Розпис у полтавському стилі",
            (PotteryOrder, "Гуцульщина")  => "Гуцульська кераміка (орнамент)",
            (PotteryOrder, "Сучасний")    => "Глазурування та обпалення",
            _                      => "Техніка обирається майстром"
        };

        public string Category => (this, Material) switch
        {
            (ClothingOrder, "Льон")        => "Автентичний одяг (старовинний стиль)",
            (ClothingOrder, "Шовк")        => "Святковий одяг",
            (ClothingOrder, "Бавовна")     => "Повсякденний текстильний виріб",
            (ClothingOrder, _)             => "Сучасний текстильний виріб",
            (PotteryOrder, "Глина")      => "Кераміка ручної роботи",
            (PotteryOrder, "Фаянс")      => "Фаянсовий посуд",
            (PotteryOrder, "Порцеляна")  => "Порцеляновий посуд",
            _                       => "Категорія не визначена"
        };

        public override string ToString()
        {
        string displayType = this switch
          {
              ClothingOrder => "Одяг",
              PotteryOrder => "Посуд",
            _            => "Невідомий тип"
          };
            return $@"
╔═══════════════════════════════════════╗
║      ВАШЕ ЗАМОВЛЕННЯ                  ║
╚═══════════════════════════════════════╝
Тип:          {displayType}   
Матеріал:     {Material}
Регіон:       {Region}
Техніка:      {Technique}
Категорія:    {Category}
Термін:       {Days} днів
Ціна:         {CalculateFinalPrice():C}"; //Форматизатор "C" (Currency) перетворює число у грошовий формат
        }

        public abstract decimal CalculateFinalPrice();
      
        protected Order(string material, string region, int days, decimal basePrice)
        {
            Material = material;
            Region = region;
            Days = days;
            BasePrice = basePrice;
        }
    }
}
