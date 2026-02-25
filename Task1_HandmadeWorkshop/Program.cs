// 🧵інтерактивний консольний додаток для майстрів хендмейду.
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

ShowLogo();

string itemType = GetValidInput(
    "Будь ласка, введіть тип виробу (Одяг/Посуд/Інше):",
    new[] { "Одяг", "Посуд", "Інше" }
);

string material = GetValidInput(
    "Будь ласка, введіть назву матеріалу (Льон/Глина/Пластик/Інше):",
    new[] { "Льон", "Глина", "Пластик", "Інше" }
);

string region = GetValidInput(
    "Будь ласка, введіть регіон (Полтава/Гуцульщина/Сучасний/Інше):",
    new[] { "Полтава", "Гуцульщина", "Сучасний", "Інше" }
);

int days = GetValidInt("Які строки виконання роботи? Введіть кількість днів: ", 1, 365);

Order myOrder = new Order(itemType, material, region, days, 2500m);

myOrder.DisplayInfo();

// --- МІСЦЕ ДЛЯ ТВОЇХ МЕТОДІВ (поза Main) ---
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
        string? input = Console.ReadLine()?.Trim().Replace(" ", "");

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

static int GetValidInt(string prompt, int min, int max)
{
    if (min > max)
    {
        throw new ArgumentException("Мінімальне значення не може бути більшим за максимальне!");
    }
    while (true)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine()?.Trim();

        if (int.TryParse(input, out int result))
        {
            if (result >= min && result <= max)
            {
                return result;
            }
            else
            {
                Console.WriteLine($"❌ Помилка: введіть число в межах від {min} до {max}.");
            }
        }
        else
        {
            Console.WriteLine("❌ Це не число. Спробуйте ще раз.");
        }
    }
}
public class Order
{
    public string ItemType { get; set; }
    public string Material { get; set; }
    public string Region { get; set; }
    public int Days { get; set; }
    public decimal Price { get; set; }
    // Створюєм Властивості Technique та Category, які визначаються на основі інших властивостей
    public string Technique
    {
        get
        {
            return Region switch
            {
                "Полтава" => "Вишивка біллю (білим по білому)",
                "Гуцульщина" => "Низинка або кучерявий шов",
                "Сучасний" => "Машинна вишивка або принт",
                _ => "Техніка обирається майстром"
            };
        }
    }
    public string Category
    {
        get
        {   //Кортежі в switch: Твоя конструкція— це дуже сучасний C# (Pattern Matching).
            return (ItemType, Material) switch 
            {
                ("Одяг", "Льон") => "Автентичний одяг (старовинний стиль)",
                ("Одяг", _) => "Сучасний текстильний виріб",
                ("Посуд", "Глина") => "Кераміка ручної роботи",
                _ => "Категорія не визначена"
            };
        }
    }
    public void DisplayInfo()
    {
        Console.WriteLine("\n╔═══════════════════════════════════════╗");
        Console.WriteLine("║      ВАШЕ ЗАМОВЛЕННЯ                 ║");
        Console.WriteLine("╚═══════════════════════════════════════╝");
        Console.WriteLine($"Тип виробу:   {ItemType}");
        Console.WriteLine($"Матеріал:     {Material}");
        Console.WriteLine($"Регіон:       {Region}");
        Console.WriteLine($"Техніка:      {Technique}");  
        Console.WriteLine($"Категорія:    {Category}");   
        Console.WriteLine($"Термін:       {Days} днів");
        Console.WriteLine($"Ціна:         {Price:C}");
        Console.WriteLine("═══════════════════════════════════════\n");
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