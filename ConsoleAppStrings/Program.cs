
//Практична задача: Аналізатор товарних бирок

// 1. Валідація артикула.
string article = "";
const string requiredPrefix = "ETHNO-";
const int requiredLength = 13;

while (true)
{
    Console.WriteLine($"Введіть артикул товару в форматі: {requiredPrefix}XX-YYYY");
    article = Console.ReadLine()?.Replace(" ", "").ToUpper() ?? ""; 
    
    if (string.IsNullOrWhiteSpace(article))
    {
        Console.WriteLine("❌ Помилка: Ви нічого не ввели.");
    }
    else if (article.Length != requiredLength)
    {
        Console.WriteLine($"❌ Помилка: Артикул повинен містити рівно {requiredLength} символів.");
    }
    else if (!article.StartsWith(requiredPrefix))
    {
        Console.WriteLine($"❌ Помилка: Артикул повинен починатися з '{requiredPrefix}'.");
    }
    else
    {
        Console.WriteLine("Артикул прийнятий.");
        break;
    }
    Console.WriteLine("Будь ласка, спробуйте ще раз.");
}

// 2. Розшифровка коду
string[] parts = article.Split('-');
string categoryCode = parts[1];
string yearCode = parts[2];
Console.WriteLine($"Дякуємо! Ви зареєстрували виріб категорії {categoryCode} за {yearCode}  рік.");

//3.Обробка опису товару
Console.WriteLine("Введіть опис товару:");
string description = Console.ReadLine()?.Trim() ?? "";
string replacedDescription = description.Replace("пластик", "еко-матеріал", StringComparison.OrdinalIgnoreCase); //або rawDescription.Trim()
Console.WriteLine("Оновлений опис товару:");
Console.WriteLine(replacedDescription);

//4.Контроль безпеки
string lowerDescription = replacedDescription.ToLower();
if (lowerDescription.Contains("золото") || lowerDescription.Contains("срібло"))
{
    Console.WriteLine("🔔 Увага! Товар містить дорогоцінні метали. Потрібне додаткове страхування.");
}

