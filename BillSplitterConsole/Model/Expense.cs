namespace BillSplitterConsole.Model
{
    public class Expense
    {
        public Expense(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }
}