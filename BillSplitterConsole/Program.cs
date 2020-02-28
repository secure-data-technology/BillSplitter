using BillSplitterConsole.Workflow;

namespace BillSplitterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = args[0];
 
            BillSplitterWorkflow workflow = new BillSplitterWorkflow();
            workflow.SplitBill(filePath);
        }
    }
}
