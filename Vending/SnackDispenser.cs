using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Vending
{
    public class SnackDispenser : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        public decimal Balance { get; private set; }
        public List<Product> Products { get; } = new List<Product>();

        public void AddBalance(decimal amount)
        {
            Balance += amount;
            UpdateSD();
        }

        public void DeductBalance(decimal amount)
        {
            Balance -= amount;
            UpdateSD();
        }

        public void DispenseProduct(Product product)
        {
            product.Quantity--;
            UpdateSD();
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        } 

        public void UpdateSD()
        {
            foreach (var obs in observers)
                obs.Update(this);
        }
    }
}
