using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSplitterConsole.Model
{
    internal class Participant
    {
        private List<Expense> expenses;
        private double balance;
        private static int id = 0;

        public Participant()
        {
            expenses = new List<Expense>();
            balance = double.NaN;
            ID = ++id;
        }

        public int ID { get; }

        public void AddExpense(double amount)
        {
            expenses.Add(new Expense(amount));
        }

        public double GetTotalExpenses()
        {
            double totalExpenses = (from e in expenses select e.Amount).Sum();
            return totalExpenses;
        }

        public void SetTripBalance(double tripBalance)
        {
            balance = tripBalance;
        }

        public double GetTripBalance()
        {
            return balance;
        }
    }
}
