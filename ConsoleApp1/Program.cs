// 📊 Практична задача: Бухгалтерія етно-ярмарку

//1. Створення масиву(Arrays)

int[] sales = new int[5];


// 2. Введення даних(for loop)

    for (int i = 0; i < sales.Length; i++)
{
    Console.WriteLine($"Введіть ціну для дня {i + 1}: ");
    while (true)
    {
        string? input = Console.ReadLine()?.Trim();
        if (int.TryParse(input, out int dailySales) && dailySales >= 0)
        {
            sales[i] = dailySales;
            break;
        }
        Console.WriteLine("Будь ласка, введіть коректну суму продажів (не від'ємне ціле число):");
    }
}

// 3. Обчислення результатів

int totalSum = 0;
int maxPrice = sales[0];

foreach (int dailySales in sales)
{
    totalSum += dailySales;
    if (dailySales > maxPrice)
    {
        maxPrice = dailySales;
    }
}
Console.WriteLine($"Загальна сума продажів за {sales.Length} днів: {totalSum}");
Console.WriteLine($"Максимальна ціна продажів за {sales.Length} днів: {maxPrice}");

// 4. Фінальний звіт

Console.WriteLine(@"
=== ЗВІТ ЗА ДЕНЬ ===
Усі продажі: ["  + string.Join(", ", sales) + @"]
Загальна виручка: " + totalSum + @" грн.
Найдорожчий виріб: " + maxPrice + @" грн.
====================");