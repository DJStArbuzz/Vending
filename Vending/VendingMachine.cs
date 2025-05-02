using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Vending
{
    public class VendingMachine : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        public decimal Balance { get; private set; }
        public List<Product> Products { get; } = new List<Product>();

        public void AddBalance(decimal amount)
        {
            Balance += amount;
            Notify();
        }

        public void DeductBalance(decimal amount)
        {
            Balance -= amount;
            Notify();
        }

        public void DispenseProduct(Product product)
        {
            product.Quantity--;
            Notify();
        }

        public void Attach(IObserver observer) => _observers.Add(observer);

        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update(this);
        }
    }
}
