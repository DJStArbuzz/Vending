using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending.Commands
{
    public class SelectProductCommand : ICommand
    {
        private VendingMachine _machine;
        private int _productIndex;

        public SelectProductCommand(VendingMachine machine, int index)
        {
            _machine = machine;
            _productIndex = index;
        }

        public void Execute()
        {
            var product = _machine.Products[_productIndex];
            if (product.Quantity > 0 && _machine.Balance >= product.Price)
            {
                _machine.DispenseProduct(product);
                _machine.DeductBalance(product.Price);
                Console.WriteLine($"Выдан продукт: {product.Name}");
            }
            else
            {
                Program.ShowError(" Ошибка: невозможно выдать продукт! Проверьте баланс и наличие товара.");
            }
        }
    }
}
