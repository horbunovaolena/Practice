// 🧵інтерактивний консольний додаток для майстрів хендмейду.

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; //Налаштування кодування консолі для коректного відображення кирилиці 
        Console.InputEncoding = System.Text.Encoding.UTF8;

        // Твій новий метод привітання :

        ShowLogo(); 
        
        // Привітання:

        string ItemType;
        Console.WriteLine("Вітаємо в асистенті майстерні! Давайте оформимо ваше замовлення");

        // Введення категорії:

        Console.WriteLine("Будь ласка, введіть тип виробу(Одяг/Посуд/Інше): ");
        while (true)
        {
            string? input = Console.ReadLine()?.Trim().Replace(" ", "");
            if (!string.IsNullOrEmpty(input))
            {
                input = char.ToUpper(input[0]) + input.Substring(1).ToLower();

                if (input == "Одяг" || input == "Посуд" || input == "Інше")
                {
                    ItemType = input;
                    break;
                }
            }

            Console.WriteLine("Будь ласка, введіть коректний тип виробу(Одяг/Посуд/Інше): ");
        }


        // Введення матеріалу:

        Console.WriteLine("Будь ласка, введіть назву матеріалу(Льон/Глина/Пластик/Інше): ");
        string Material;
        while (true)
        {
            string? input = Console.ReadLine()?.Trim().Replace(" ", "");
            if (!string.IsNullOrEmpty(input))
            {
                input = char.ToUpper(input[0]) + input.Substring(1).ToLower();
                if (input == "Льон" || input == "Глина" || input == "Пластик" || input == "Інше")
                {
                    Material = input;
                    break;
                }
            }
            Console.WriteLine("Будь ласка, введіть коректну назву матеріалу(Льон/Глина/Пластик/Інше): ");
        }

        // Вибір локації: 

        Console.WriteLine("Будь ласка, введіть для якого регіону виконується робота(Полтава/Гуцульщина/Сучасний/Інше): ");
        string Region;
        while (true)
        {
            string? input = Console.ReadLine()?.Trim().Replace(" ", "");
            if (!string.IsNullOrEmpty(input))
            {
                input = char.ToUpper(input[0]) + input.Substring(1).ToLower();
                if (input == "Полтава" || input == "Гуцульщина" || input == "Сучасний" || input == "Інше")
                {
                    Region = input;
                    break;
                }
            }
            Console.WriteLine("Будь ласка, введіть коректно для якого регіону виконується робота(Полтава/Гуцульщина/Сучасний/Інше): ");
        }

        // Вибір техніки декору:

        string technique = Region switch
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

        Console.WriteLine($"Ваше замовлення: Тип виробу: {ItemType}, Матеріал: {Material}, Регіон: {Region} ,Техніка виконання: {technique}, Строки виконання: {Days} днів ");

        // Класифікатор виробу: 

        if (ItemType == "Одяг" && Material == "Льон")
        {
            Console.WriteLine("Це автентичний одяг(старовинний стиль).");
        }
        else if (ItemType == "Одяг" && Material != "Льон")
        {
            Console.WriteLine("Це сучасний текстильний виріб.");
        }
        else if (ItemType == "Посуд" && Material == "Глина")
        {
            Console.WriteLine("Це кераміка ручної роботи");
        }
        else
        {
            Console.WriteLine("Категорія для хендмейду не визначена");
        }
    }
    // --- МІСЦЕ ДЛЯ ТВОЇХ МЕТОДІВ (поза Main) ---

    static void ShowLogo()
    {
        Console.WriteLine("=== ETNO-STYLE WORKSHOP ===");
    }
    static int GetDiscountedPrice(int price)
    {
        if (price > 2000)
            return (int)(price * 0.9);

        return price;
    }
}