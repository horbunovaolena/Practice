//Створити словник , який містить назви товарів та їх ціни.Заповнити кількома товарами,цінами.
// Додаємо ігнорування регістру прямо при створенні словника
var prices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
{
    {"вишиванка", 2500},
    {"спідниця", 1500},
    {"пояс", 500}
};

//Користувач вводить назву товару, а програма виводить його ціну.
//Якщо товару немає в словнику, вивести повідомлення про це.
var shoppingList = new List<string>();

while (true)
{
    Console.WriteLine("Введіть назви товарів.В разі завершення дії введіть 'stop': ");
    string? input = Console.ReadLine()?.Trim();
    if (input == "stop") break;
    if (!string.IsNullOrWhiteSpace(input))
        shoppingList.Add(input);
    else
        Console.WriteLine("Ви не ввели назву товару. Спробуйте ще раз.");
}

// Виводимо ціну кожного товару зі списку покупок.
// TryGetValue — це безпечний спосіб отримати значення зі словника за ключем.
Console.WriteLine("\n--- ВАШ ЧЕК ---");
int totalOrderPrice = 0;
foreach (var item in shoppingList)
{
    if (prices.TryGetValue(item, out int price)) 
    {
        totalOrderPrice += price;
        Console.WriteLine($"{item} | {price} грн.");
    }
    else
    {
        Console.WriteLine($"Ми поки не виготовляємо '{item}', але можемо додати як індивідуальне замовлення.");
    }
}
Console.WriteLine("-------------------------------");
Console.WriteLine($"ЗАГАЛОМ: {totalOrderPrice} грн.");