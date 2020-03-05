using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BillSplitterConsole.Infrastructure
{
    public class ExpenseReader : IExpenseReader
    {
        private readonly Queue<object> inputNumerics_;

        public ExpenseReader()
        {
            inputNumerics_ = new Queue<object>();
        }

        public Queue<object> ReadExpenses(string _filePath)
        {
            var inputLines = File.ReadLines(_filePath).ToList();
            GetParsedTokens(inputLines);
            return inputNumerics_;
        }

        private void GetParsedTokens(List<string> _tokens)
        {
            var tokenIndex = 0;
            var participantsCount = int.Parse(_tokens[tokenIndex++]);

            while (participantsCount > 0 && tokenIndex < _tokens.Count)
            {
                inputNumerics_.Enqueue(participantsCount);

                for (var i = 0; i < participantsCount; i++) ParseParticipant(_tokens, ref tokenIndex);

                participantsCount = int.Parse(_tokens[tokenIndex++]);
            }

            inputNumerics_.Enqueue(participantsCount);
        }

        private void ParseParticipant(List<string> _tokens, ref int _index)
        {
            var expensesCount = int.Parse(_tokens[_index++]);

            inputNumerics_.Enqueue(expensesCount);

            for (var i = 0; i < expensesCount; i++) ParseExpense(_tokens, ref _index);
        }

        private void ParseExpense(List<string> _tokens, ref int _index)
        {
            var amount = decimal.Parse(_tokens[_index++]);
            inputNumerics_.Enqueue(amount);
        }
    }
}