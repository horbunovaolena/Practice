// Program.cs
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Task1_HandmadeWorkshop;

// 🧵інтерактивний консольний додаток для майстрів хендмейду.
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

ShowLogo();

Dictionary<string, decimal> priceList = new Dictionary<string, decimal>
{
    { "Одяг", 2500m },
    { "Посуд", 1500m }
};

List<Order> orders = new List<Order>();

while (true)
{
    string itemType = GetValidInput(
    "Будь ласка, введіть тип виробу (Одяг/Посуд):",
    new[] { "Одяг", "Посуд" });

    string material;
    if (itemType == "Одяг")
    {
        material = GetValidInput(
            "Будь ласка, введіть матеріал (Льон/Бавовна/Шовк/Інше):",
            new[] { "Льон", "Бавовна", "Шовк", "Інше" });
    }
    else
    {
        material = GetValidInput(
            "Будь ласка, введіть матеріал (Глина/Фаянс/Порцеляна/Інше):",
            new[] { "Глина", "Фаянс", "Порцеляна", "Інше" });
    }

    string region = GetValidInput(
        "Будь ласка, введіть регіон (Полтава/Гуцульщина/Сучасний/Інше):",
        new[] { "Полтава", "Гуцульщина", "Сучасний", "Інше" });

    int days = GetValidInt("Які строки виконання роботи? Введіть кількість днів: ");

    decimal basePrice = priceList[itemType];

    Order myOrder;
    if (itemType == "Одяг")
    {
        string size = GetValidInput(
            "Вкажіть розмір (S/M/L):",
            new[] { "S", "M", "L" });
        myOrder = new ClothingOrder(material, region, days, basePrice, size);
    }
    else
    {
        decimal volume = GetValidDecimal("Вкажіть об'єм виробу в літрах (наприклад, 1.5): ");
        myOrder = new PotteryOrder(material, region, days, basePrice, volume);
    }

    Console.WriteLine(myOrder);
    orders.Add(myOrder);
    Console.WriteLine("\n✅ Ваше замовлення прийнято!\n");

    bool shouldExit = false;  //flag для виходу із зовнішнього циклу while.

    while (true)
    {
        Console.Write("Бажаєте зробити ще одне замовлення? (так / ні): ");
        string? response = Console.ReadLine()?.Trim()?.ToLower();

        if (response is "так" or "да" or "yes")  // Pattern Matching з логічними шаблонами
        {
            break;
        }
        else if (response is "ні" or "нет" or "no")
        {
            shouldExit = true;
            break;
        }
        else
        {
            Console.WriteLine("❌ Будь ласка, введіть 'так' або 'ні'.");
        }
    }

    if (shouldExit) break;
}

Console.WriteLine("\nАналітика майстерні за день");
if (orders.Any())
{  // Агрегація даних по всіх замовленнях за допомогою LINQ
    decimal totalRevenue = orders.Sum(o => o.CalculateFinalPrice());
    decimal maxPrice = orders.Max(o => o.CalculateFinalPrice());
    decimal averagePrice = orders.Average(o => o.CalculateFinalPrice());
    int urgentCount = orders.Count(o => o.Days < Order.UrgentDaysThreshold);
    //аналітика за день
    Console.WriteLine($"✅ Всього замовлень: {orders.Count}");
    Console.WriteLine($"💰 Загальна виручка: {totalRevenue:C}");
    Console.WriteLine($"📈 Найвищий чек: {maxPrice:C}");
    Console.WriteLine($"⚖️ Середня вартість: {averagePrice:C}");
    Console.WriteLine($"🔥 Термінових замовлень: {urgentCount}");

    Console.WriteLine("\n📦 Етикетки для всіх замовлень:");
    foreach (Order o in orders)
    {
        if (o is IExportable exportable)
            Console.WriteLine(exportable.GetLabel());
    }
}
else
{
    Console.WriteLine("Замовлень сьогодні не було.");
}


static void ShowLogo()
{
    Console.WriteLine("╔═══════════════════════════════════════╗");
    Console.WriteLine("║    ETNO-STYLE WORKSHOP 🧵             ║");
    Console.WriteLine("╚═══════════════════════════════════════╝\n");
}

static string GetValidInput(string prompt, string[] validOptions)
{
    while (true)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine()?.Trim()?.Replace(" ", "");

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("❌ Ви нічого не ввели. Спробуйте ще раз.");
            continue;
        }

        input = char.ToUpper(input[0]) + input.Substring(1).ToLower();

        foreach (string option in validOptions)
        {
            if (input == option) return input;
        }

        Console.WriteLine("❌ Помилка. Оберіть варіант зі списку.");
    }
}

static decimal GetValidDecimal(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine()?.Trim()?.Replace(',', '.');

        if (decimal.TryParse(input, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out decimal result) && result > 0)
        {
            return result;
        }
        Console.WriteLine("❌ Введіть коректне число більше нуля.");
    }
}

static int GetValidInt(string prompt)
{
    if (Order.MinDays > Order.MaxDays)
    {
        throw new ArgumentException("Мінімальне значення не може бути більшим за максимальне!");
    }
    while (true)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine()?.Trim();

        if (int.TryParse(input, out int result))
        {
            if (result >= Order.MinDays && result <= Order.MaxDays)
            {
                return result;
            }
            else
            {
                Console.WriteLine($"❌ Помилка: введіть число в межах від {Order.MinDays} до {Order.MaxDays}.");
            }
        }
        else
        {
            Console.WriteLine("❌ Це не число. Спробуйте ще раз.");
        }
    }
}
