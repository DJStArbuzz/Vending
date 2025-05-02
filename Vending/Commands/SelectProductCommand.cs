using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending.Commands
{
    public class SelectProductCommand : ICommand
    {
        private SnackDispenser _machine;
        private int _productIndex;

        public SelectProductCommand(SnackDispenser machine, int index)
        {
            _machine = machine;
            _productIndex = index;
        }

        public void Execute()
        {
            var product = _machine.Products[_productIndex];

            if (!CanDispense(product))
            {
                Program.ShowError(" Ошибка: невозможно выдать продукт!");
                return;
            }

            ShowDispensingProcess(product);

            _machine.DispenseProduct(product);
            _machine.DeductBalance(product.Price);
        }

        private bool CanDispense(Product product)
        {
            return product.Quantity > 0 && _machine.Balance >= product.Price;
        }

        private void ShowDispensingProcess(Product product)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n[Процесс выдачи товара]");

            SimulateStep($"1. Проверка наличия '{product.Name}'...", 500);
            SimulateStep($"2. Спишем {product.Price}$...", 700);
            SimulateStep("3. Открываем отсек...", 1000);
            SimulateStep($"4. Выдаём {product.Name}...", 1200);
            SimulateStep("5. Закрываем отсек...", 800);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nУспешно: {product.Name} получен!");
            Console.ResetColor();

            PlaySuccessSound();
        }

        private void SimulateStep(string message, int delayMs)
        {
            Console.WriteLine($"   {message.PadRight(40)}"); // Фиксированная ширина
            Thread.Sleep(delayMs);
            Console.SetCursorPosition(45, Console.CursorTop - 1); // Позиция для галочки
            Console.WriteLine("!");
        }

        private void PlaySuccessSound()
        {
            Console.Beep(1000, 200);
            Console.Beep(1500, 300);
        }
    }
}
