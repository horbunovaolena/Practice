        //Списки (List) "Динамічний список покупок"

        var shoppingList = new List<string>();

        while (true)
        {
            Console.WriteLine("Введіть назви товарів. В разі завершення дії введіть 'stop': ");
            string? input = Console.ReadLine()?.Trim();
    
            if (input == "stop") break;
    
            if (!string.IsNullOrWhiteSpace(input))
                shoppingList.Add(input);
            else
                Console.WriteLine("Ви не ввели назву товару. Спробуйте ще раз.");
        }

        Console.WriteLine($"Кількість товарів у списку: {shoppingList.Count}");

        if (shoppingList.Contains("Вишиванка"))
            Console.WriteLine("Список містить Вишиванку.");

        if (shoppingList.Count > 0)
        {
            shoppingList.RemoveAt(0);
            Console.WriteLine("Перше замовлення пішло в роботу.");
        }

