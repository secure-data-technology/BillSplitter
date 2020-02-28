using System.Collections.Generic;
using System.Linq;

namespace BillSplitterConsole.Model
{
    internal class Participant
    {
        private List<Expense> expenses;
        private decimal balance;
        private static int id = 0;

        public Participant()
        {
            expenses = new List<Expense>();
            balance = decimal.Zero; 
            ID = ++id;
        }

        public int ID { get; }

        public void AddExpense(decimal amount)
        {
            expenses.Add(new Expense(amount));
        }

        public decimal GetTotalExpenses()
        {
            decimal totalExpenses = (from e in expenses select e.Amount).Sum();
            return totalExpenses;
        }

        public void SetTripBalance(decimal tripBalance)
        {
            balance = tripBalance;
        }

        public decimal GetTripBalance()
        {
            return balance;
        }
    }
}
