using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BillSplitterConsole.Workflow;
using BillSplitterConsole.Infrastructure;

namespace BillSplitterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = args[0];
  

            BillSplitterWorkflow controller = new BillSplitterWorkflow();
          //  Queue<object> parsedTokens = controller.GetParsedTokens(lines);
        }
    }
}
