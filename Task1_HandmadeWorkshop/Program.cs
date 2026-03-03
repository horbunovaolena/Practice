// Program.cs
using System.ComponentModel.DataAnnotations;
using Task1_HandmadeWorkshop;

// 🧵інтерактивний консольний додаток для майстрів хендмейду.
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

ShowLogo();

Dictionary<string, decimal> priceList = new Dictionary<string, decimal>
{
    { "Одяг", 2500m },
    { "Посуд", 1500m },
    { "Інше", 1000m  }
};

List<Order> orders = new List<Order>();

while (true)
{
    string itemType = GetValidInput(
    "Будь ласка, введіть тип виробу (Одяг/Посуд/Інше):",
    new[] { "Одяг", "Посуд", "Інше" });

    string material = GetValidInput
            ("Будь ласка, введіть назву матеріалу (Льон/Глина/Пластик/Інше):",
        new[] { "Льон", "Глина", "Пластик", "Інше" });

    string region = GetValidInput(
        "Будь ласка, введіть регіон (Полтава/Гуцульщина/Сучасний/Інше):",
        new[] { "Полтава", "Гуцульщина", "Сучасний", "Інше" }
    );

    int days = GetValidInt("Які строки виконання роботи? Введіть кількість днів: ");

    decimal basePrice = priceList[itemType];

    Order myOrder = new Order(itemType, material, region, days, basePrice);

    Console.WriteLine(myOrder);
    orders.Add(myOrder);
    Console.WriteLine("\n✅ Ваше замовлення прийнято!\n");

    bool shouldExit = false;

    while (true)
    {
        Console.Write("Бажаєте зробити ще одне замовлення? (так / ні): ");
        string? response = Console.ReadLine()?.Trim()?.ToLower();

        if (response is "так" or "да" or "yes")
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
