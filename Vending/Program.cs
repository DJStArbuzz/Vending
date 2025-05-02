using System;
using System.Collections.Generic;
using Vending.Commands;
using Vending;

class Program
{
    static void Main()
    {
        var vm = new SnackDispenser();

        var snackFactory = new SnackFactory();
        var chocolateFactory = new ChocolateBarFactory();
        var drinkFactory = new DrinkFactory();

        vm.Products.AddRange(new Product[] {
            snackFactory.CreateProduct("Чипсы Chays", 120, 8),
            snackFactory.CreateProduct("Сырные орешки", 90, 12),
            chocolateFactory.CreateProduct("Mars", 65, 15),
            chocolateFactory.CreateProduct("Snickers", 70, 10),
            chocolateFactory.CreateProduct("Twix", 75, 8),
            drinkFactory.CreateProduct("Вода BonAqua", 60, 20),
            drinkFactory.CreateProduct("Сок Rich", 110, 8),
            drinkFactory.CreateProduct("Red Bull", 180, 5),
            snackFactory.CreateProduct("Печенье Юбилейное", 85, 10),
            drinkFactory.CreateProduct("Pepsi", 95, 15)
        });

        var ui = new ConsoleDes();
        vm.Attach(ui);
        vm.UpdateSD();

        while (true)
        {
            Console.WriteLine("--------------------------------------");
            Console.Write(" Введите сумму (q для выхода): ");
            var input = Console.ReadLine();

            if (input?.Trim().ToLower() == "q")
            {
                Console.WriteLine("\n Работа завершена. До свидания!");
                return;
            }

            if (!decimal.TryParse(input, out decimal amount) || amount < 0)
            {
                ShowError(" Ошибка: некорректная сумма!");
                continue;
            }

            new InsertMoneyCommand(vm, amount).Execute();

            while (true)
            {
                Console.WriteLine("--------------------------------------");
                Console.Write(" Выберите товар или введите q для выхода: ");
                var productInput = Console.ReadLine();

                if (productInput?.Trim().ToLower() == "q")
                {
                    vm.UpdateSD();
                    break;
                }

                if (!int.TryParse(productInput, out int index) || index < 1 || index > vm.Products.Count)
                {
                    ShowError(" Ошибка: неверный номер товара!");
                    continue;
                }

                new SelectProductCommand(vm, index - 1).Execute();
                break;
            }
        }
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Beep(300, 500);
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine(message);
        Console.WriteLine(new string('=', 40));
        Console.ResetColor();
    }
}