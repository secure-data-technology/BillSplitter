using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BillSplitterConsole.Infrastructure
{
    public class FileExpenseReader : IExpenseReader
    {
        private readonly Queue<object> _inputNumericInstances;
        private readonly string _instanceFilePath;

        public FileExpenseReader(string filePath)
        {
            _inputNumericInstances = new Queue<object>();
            _instanceFilePath = filePath;
        }

        public Queue<object> ReadExpenses()
        {
            var inputLines = File.ReadLines(_instanceFilePath).ToList();
            GetParsedTokens(inputLines);
            return _inputNumericInstances;
        }

        private void GetParsedTokens(List<string> tokens)
        {
            var tokenIndex = 0;
            var participantsCount = int.Parse(tokens[tokenIndex++], NumberStyles.Integer);

            while (participantsCount > 0 && tokenIndex < tokens.Count)
            {
                _inputNumericInstances.Enqueue(participantsCount);

                for (var i = 0; i < participantsCount; i++) ParseParticipant(tokens, ref tokenIndex);

                participantsCount = int.Parse(tokens[tokenIndex++]);
            }

            _inputNumericInstances.Enqueue(participantsCount);
        }

        private void ParseParticipant(List<string> tokens, ref int index)
        {
            var expensesCount = int.Parse(tokens[index++]);

            _inputNumericInstances.Enqueue(expensesCount);

            for (var i = 0; i < expensesCount; i++) ParseExpense(tokens, ref index);
        }

        private void ParseExpense(List<string> tokens, ref int index)
        {
            var amount = decimal.Parse(tokens[index++]);
            _inputNumericInstances.Enqueue(amount);
        }
    }
}