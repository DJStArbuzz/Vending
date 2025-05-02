using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
    public interface IObserver
    {
        void Update(VendingMachine machine);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }

    public interface ICommand
    {
        void Execute();
    }
}
