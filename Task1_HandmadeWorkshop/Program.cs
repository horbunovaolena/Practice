// 🧵інтерактивний консольний додаток для майстрів хендмейду.
Console.OutputEncoding = System.Text.Encoding.UTF8; //Налаштування кодування консолі для коректного відображення кирилиці 
Console.InputEncoding = System.Text.Encoding.UTF8;

// Твій новий метод привітання :
ShowLogo();

// 1. Отримуємо Тип
string itemType = GetValidInput(
    "Будь ласка, введіть тип виробу (Одяг/Посуд/Інше):",
    new[] { "Одяг", "Посуд", "Інше" }
);

// 2. Отримуємо Матеріал
string material = GetValidInput(
    "Будь ласка, введіть назву матеріалу (Льон/Глина/Пластик/Інше):",
    new[] { "Льон", "Глина", "Пластик", "Інше" }
);

// 3. Отримуємо Регіон
string region = GetValidInput(
    "Будь ласка, введіть регіон (Полтава/Гуцульщина/Сучасний/Інше):",
    new[] { "Полтава", "Гуцульщина", "Сучасний", "Інше" }
);

// Вибір техніки декору:
string technique = region switch
{
    "Полтава" => "Вишивка біллю (білим по білому)",
    "Гуцульщина" => "Низинка або кучерявий шов",
    "Сучасний" => "Машинна вишивка або принт",
    _ => "Техніка обирається майстром"
};

// Встановлення дедлайну:
Console.WriteLine("Які строки виконання роботи? Введіть кількість днів: ");
int Days;
while (!int.TryParse(Console.ReadLine(), out Days) || Days <= 0)
{
    Console.WriteLine("Будь ласка, введіть коректну кількість днів(позитивне ціле число): ");
}
string status = Days < 3 ? "Терміново" : "Звичайно";

// Отримання результату:
Console.WriteLine($"Ваше замовлення: Тип виробу: {itemType}, Матеріал: {material}, Регіон: {region} ,Техніка виконання: {technique}, Строки виконання: {Days} днів ");

// Класифікатор виробу: 
if (itemType == "Одяг" && material == "Льон")
{
    Console.WriteLine("Це автентичний одяг(старовинний стиль).");
}
else if (itemType == "Одяг" && material != "Льон")
{
    Console.WriteLine("Це сучасний текстильний виріб.");
}
else if (itemType == "Посуд" && material == "Глина")
{
    Console.WriteLine("Це кераміка ручної роботи");
}
else
{
    Console.WriteLine("Категорія для хендмейду не визначена");
}

// --- МІСЦЕ ДЛЯ ТВОЇХ МЕТОДІВ (поза Main) ---
static void ShowLogo()
{
    Console.WriteLine("=== ETNO-STYLE WORKSHOP ===");
}

static string GetValidInput(string prompt, string[] validOptions)
{
    while (true)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine()?.Trim().Replace(" ", "");

        // Додаємо перевірку: якщо рядок порожній, не йдемо далі
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("❌ Ви нічого не ввели. Спробуйте ще раз.");
            continue; // Повертаємося на початок циклу
        }

        // Тепер це безпечно, бо ми точно знаємо, що там є хоча б один символ
        input = char.ToUpper(input[0]) + input.Substring(1).ToLower();

        foreach (string option in validOptions)
        {
            if (input == option) return input;
        }

        Console.WriteLine("❌ Помилка. Оберіть варіант зі списку.");
    }
}