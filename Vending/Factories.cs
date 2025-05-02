using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
    public abstract class ProductFactory
    {
        public abstract Product CreateProduct(string name, decimal price, int quantity);
    }

    public class SnackFactory : ProductFactory
    {
        public override Product CreateProduct(string name, decimal price, int quantity) =>
            new Snack { Name = name, Price = price, Quantity = quantity };
    }

    public class ChocolateBarFactory : ProductFactory
    {
        public override Product CreateProduct(string name, decimal price, int quantity) =>
            new ChocolateBar { Name = name, Price = price, Quantity = quantity };
    }

    public class DrinkFactory : ProductFactory
    {
        public override Product CreateProduct(string name, decimal price, int quantity) =>
            new Drink { Name = name, Price = price, Quantity = quantity };
    }
}
