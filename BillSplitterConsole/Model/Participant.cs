using System.Collections.Generic;
using System.Linq;

namespace BillSplitterConsole.Model
{
    public class Participant
    {
        private static int _id;
        private readonly List<Expense> _expenses;
        private decimal _balance;

        public Participant()
        {
            _expenses = new List<Expense>();
            _balance = decimal.Zero;
            ID = ++_id;
        }

        public int ID { get; }

        public void AddExpense(decimal amount)
        {
            _expenses.Add(new Expense(amount));
        }

        public decimal GetTotalExpenses()
        {
            var totalExpenses = (from e in _expenses select e.Amount).Sum();
            return totalExpenses;
        }

        public void SetTripBalance(decimal tripBalance)
        {
            _balance = tripBalance;
        }

        public decimal GetTripBalance()
        {
            return _balance;
        }
    }
}