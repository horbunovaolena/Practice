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
            if (Material == "Льон")
                price *= 1.1m;
            if (Days < UrgentDaysThreshold)
                price *= UrgentPriceMultiplier;
            return price;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nРозмір:       {Size}";
        }

        public string GetExportString()
        {
            return $"[ОДЯГ] {Material} | {Region} | Розмір: {Size} | {CalculateFinalPrice():C}";
        }
    }
}
