using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
    public class ConsoleUI : IObserver
    {
        private const int NameWidth = 25;
        private const int PriceWidth = 10;
        private const int QuantityWidth = 10;

        private string CreateSeparator() =>
            new string('-', NameWidth + PriceWidth + QuantityWidth + 7);

        public void Update(VendingMachine machine)
        {
            Console.Clear();
            DrawHeader();
            DrawBalance(machine.Balance);
            DrawProductsTable(machine.Products);
            Console.WriteLine(" Для выхода введите 'q' в любой момент\n");
        }

        private void DrawHeader()
        {
            var sep = CreateSeparator();
            Console.WriteLine(sep);
            Console.WriteLine("|           ВЕНДИНГОВЫЙ АВТОМАТ           |");
            Console.WriteLine(sep);
        }

        private void DrawBalance(decimal balance)
        {
            Console.WriteLine($"\n Текущий баланс: {balance}₽\n");
        }

        private void DrawProductsTable(List<Product> products)
        {
            var sep = CreateSeparator();
            Console.WriteLine(sep);
            Console.WriteLine($"| {"№",2} | {"Название".PadRight(NameWidth)} | {"Цена".PadLeft(PriceWidth)} | {"Остаток".PadLeft(QuantityWidth)} |");
            Console.WriteLine(sep);

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                Console.WriteLine(
                    $"| {i + 1,2} | {p.Name.PadRight(NameWidth)} | " +
                    $"{p.Price.ToString().PadLeft(PriceWidth)}₽ | " +
                    $"{p.Quantity.ToString().PadLeft(QuantityWidth)} |");
            }
            Console.WriteLine(sep + "\n");
        }
    }
}
