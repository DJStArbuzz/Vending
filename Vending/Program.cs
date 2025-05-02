using System;
using System.Collections.Generic;
using Vending.Commands;
using Vending;

class Program
{
    static void Main()
    {
        var vm = new VendingMachine();

        // Инициализация фабрик
        var snackFactory = new SnackFactory();
        var chocolateFactory = new ChocolateBarFactory();
        var drinkFactory = new DrinkFactory();

        // Добавление товаров
        vm.Products.AddRange(new Product[] {
            snackFactory.CreateProduct("Чипсы Lays", 120m, 8),
            snackFactory.CreateProduct("Соленые орешки", 90m, 12),
            chocolateFactory.CreateProduct("Mars", 65m, 15),
            chocolateFactory.CreateProduct("Snickers", 70m, 10),
            chocolateFactory.CreateProduct("Twix", 75m, 8),
            drinkFactory.CreateProduct("Вода BonAqua", 60m, 20),
            drinkFactory.CreateProduct("Сок Rich", 110m, 8),
            drinkFactory.CreateProduct("Red Bull", 180m, 5),
            snackFactory.CreateProduct("Печенье Юбилейное", 85m, 10),
            drinkFactory.CreateProduct("Pepsi", 95m, 15)
        });

        var ui = new ConsoleUI();
        vm.Attach(ui);
        vm.Notify();

        while (true)
        {
            Console.WriteLine("--------------------------------------");
            Console.Write(" Введите сумму (q для выхода): ");
            var input = Console.ReadLine();

            // Проверка на выход
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

            // Цикл выбора товара с проверкой на выход
            while (true)
            {
                Console.WriteLine("--------------------------------------");
                Console.Write(" Выберите товар или введите q для выхода: ");
                var productInput = Console.ReadLine();

                if (productInput?.Trim().ToLower() == "q")
                {
                    vm.Notify();
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
        Console.WriteLine(message);
        Console.ResetColor();
        Console.Beep(300, 200); // Добавляем звуковой сигнал
    }
}