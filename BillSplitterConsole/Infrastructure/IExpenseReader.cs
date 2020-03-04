using System.Collections.Generic;

namespace BillSplitterConsole.Infrastructure
{
    public interface IExpenseReader
    {
        Queue<object> ReadExpenses(string _filePath);
    }
}
