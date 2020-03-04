using BillSplitterConsole.Model;
using System.Collections.Generic;

namespace BillSplitterConsole.Infrastructure
{
    public interface IPaymentWriter
    {
        void WritePayments(string _outputPath, List<Trip> _trips);
    }
}
