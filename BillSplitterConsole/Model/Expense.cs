using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSplitterConsole.Model
{
    internal class Expense
    {
        public Expense(double amount)
        {
            Amount = amount;
        }

        public double Amount { get; }
    }
}
