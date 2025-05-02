using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending.Commands
{
    public class InsertMoneyCommand : ICommand
    {
        private SnackDispenser _machine;
        private decimal _amount;

        public InsertMoneyCommand(SnackDispenser machine, decimal amount)
        {
            _machine = machine;
            _amount = amount;
        }

        public void Execute() => _machine.AddBalance(_amount);
    }
}
