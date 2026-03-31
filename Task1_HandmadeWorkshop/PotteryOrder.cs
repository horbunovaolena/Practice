namespace Task1_HandmadeWorkshop
{
    public class PotteryOrder : Order, IExportable
    {
        private const decimal FiringComplexityCost = 300m; // Додаткова вартість складного випалу

        public decimal Volume { get; set; } //Своя властивість — об'єм посуду в літрах

        public PotteryOrder(string material, string region, int days, decimal basePrice, decimal volume)
            : base(material, region, days, basePrice)
        {
            Volume = volume;
        }

        public override decimal CalculateFinalPrice() // Перевизначаємо абстрактний метод батька
        {
            decimal price = BasePrice;           // беремо базову ціну від батька
            if (Volume > 2m)                     // якщо об'єм більше 2 літрів
                price += FiringComplexityCost;   // додаємо 300 грн за випал
            if (Days < UrgentDaysThreshold)      // якщо термін менше 5 днів
                price *= UrgentPriceMultiplier;  // множимо на 1.2 (+ 20%)
            return price;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nОб'єм: {Volume} л"; //Перевизначаємо ToString(). Спочатку виводимо все що батько(base.ToString()),
                                                             //потім додаємо своє — об'єм
        }

        public string GetLabel() //Виконуємо зобов'язання інтерфейсу IExportable — повертаємо рядок для етикетки
        {
            return $"[ПОСУД] {Material} | {Region} | Об'єм: {Volume} л | {CalculateFinalPrice():C}";
        }
    }
}
