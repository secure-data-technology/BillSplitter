using Autofac;
using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Workflow;
using System;

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
            string outputFileExtension = Resources.OutputFileExtension;
            var inputFilePath = args[0];
            var outputFilePath = inputFilePath + outputFileExtension;

            var fileSystemValidator = new FileSystemValidator();
            if (fileSystemValidator.FileExists(inputFilePath))
                fileSystemValidator.EnsureFileDoesNotExist(outputFilePath);
            else
                throw new ArgumentException(Resources.InvalidExpenseFilePath);

            CreateIocContainer(inputFilePath, outputFilePath);

            var workflow = new BillSplitterWorkflow();

            using (var scope = _iocContainer.BeginLifetimeScope())
            {
                var expenseReader = scope.Resolve<IExpenseReader>();
                var paymentWriter = scope.Resolve<IPaymentWriter>();
                workflow.SplitBill(expenseReader, paymentWriter);
            }
        }

        private static void CreateIocContainer(string readerFilePath, string writerFilePath)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new FileExpenseReader(readerFilePath))
                .As<IExpenseReader>();
            builder.RegisterInstance(new FilePaymentWriter(writerFilePath))
                .As<IPaymentWriter>();

            _iocContainer = builder.Build();
        }
    }
}