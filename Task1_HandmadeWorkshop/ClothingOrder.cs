namespace Task1_HandmadeWorkshop
{
    public class ClothingOrder : Order, IExportable
    {
        public string Size { get; set; }

        public ClothingOrder(string material, string region, int days, decimal basePrice, string size)
            : base("Одяг", material, region, days, basePrice)
        {
            Size = size;
        }

        public override decimal CalculateFinalPrice()
        {
            decimal price = BasePrice;
            if (Material == "Льон" || Material == "Шовк")
                price *= 1.1m;
            else if (Material == "Бавовна" || Material == "Інше")
            if (Days < UrgentDaysThreshold)
                price *= UrgentPriceMultiplier;
            return price;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nРозмір: {Size}";
        }

        public string GetLabel()
        {
            return $"[ОДЯГ] {Material} | {Region} | Розмір: {Size} | {CalculateFinalPrice():C}";
        }
    }
}
