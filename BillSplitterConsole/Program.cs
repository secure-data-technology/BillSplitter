using System;
using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Workflow;

namespace BillSplitterConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string outputFileExtension = ".out";
            var inputFilePath = args[0];
            var outputFilePath = inputFilePath + outputFileExtension;

            var fileSystemValidator = new FileSystemValidator();
            if (fileSystemValidator.FileExists(inputFilePath))
                fileSystemValidator.EnsureFileDoesNotExist(outputFilePath);
            else
                throw new ArgumentException("Specified expense file does not exist");

            var workflow = new BillSplitterWorkflow();
            workflow.SplitBill(inputFilePath, outputFilePath);
        }
    }
}