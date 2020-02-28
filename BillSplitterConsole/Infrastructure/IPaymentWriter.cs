using System.Collections.Generic;
using BillSplitterConsole.Model;

namespace BillSplitterConsole.Infrastructure
{
    public interface IPaymentWriter
    {
        void WritePayments(string _outputPath, List<Trip> _trips);
    }
}
