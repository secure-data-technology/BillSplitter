using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Workflow;
using System;

namespace BillSplitterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string outputFileExtension = ".out";
            string inputFilePath = args[0];
            string outputFilePath = inputFilePath + outputFileExtension;

            FileSystemValidator fileSystemvalidator = new FileSystemValidator();
            if (fileSystemvalidator.FileExists(inputFilePath))
            {
                fileSystemvalidator.EnsureFileDoesNotExist(outputFilePath);
            }
            else
            {
                throw new ArgumentException("Specified expense file does not exist");
            }

            BillSplitterWorkflow workflow = new BillSplitterWorkflow();
            workflow.SplitBill(inputFilePath, outputFilePath);
        }
    }
}
