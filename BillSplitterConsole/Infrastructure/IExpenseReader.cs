using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSplitterConsole.Infrastructure
{
    public interface IExpenseReader
    {
        Queue<object> ReadExpenses(string _filePath);
    }
}
