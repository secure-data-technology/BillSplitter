using System;
using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Workflow;
using Autofac;

namespace BillSplitterConsole
{
    internal class Program
    {
        private static IContainer _iocContainer;

        public static IContainer IocContainer
        {
            get { return _iocContainer; }
        }

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

            CreateIocContainer(inputFilePath, outputFilePath);

            var workflow = new BillSplitterWorkflow();
            IExpenseReader expenseReader = new FileExpenseReader(inputFilePath);
            IPaymentWriter paymentWriter = new FilePaymentWriter(outputFilePath);

            workflow.SplitBill(expenseReader, paymentWriter);
        }

        private static void CreateIocContainer(string readerFilePath, string writerFilePath)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new FileExpenseReader(readerFilePath))
                .As<IExpenseReader>();
            builder.RegisterType<FileExpenseReader>();

            builder.RegisterInstance(new FilePaymentWriter(writerFilePath))
                .As<IPaymentWriter>();
            builder.RegisterType<FilePaymentWriter>();

            _iocContainer = builder.Build();
        }
    }
}