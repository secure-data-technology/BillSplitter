using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BillSplitterConsole.Infrastructure
{
    internal class ExpenseReader
    {
        private Queue<object> inputNumerics_;

        public ExpenseReader()
        {
            inputNumerics_ = new Queue<object>();
        }

        public Queue<object> ReadExpenses(string _filePath)
        {
            List<string> inputLines = File.ReadLines(_filePath).ToList();
            GetParsedTokens(inputLines);
            return inputNumerics_;
        }

        private void GetParsedTokens(List<string> _tokens)
        {
            try
            {
                int tokenIndex = 0;
                int participantsCount = int.Parse(_tokens[tokenIndex++]);

                while (participantsCount > 0 && tokenIndex < _tokens.Count)
                {

                    inputNumerics_.Enqueue(participantsCount);

                    for (int i = 0; i < participantsCount; i++)
                    {
                        ParseParticipant(_tokens, ref tokenIndex);
                    }

                    participantsCount = int.Parse(_tokens[tokenIndex++]);
                }
                inputNumerics_.Enqueue(participantsCount);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        private void ParseParticipant(List<string> _tokens, ref int _index)
        {
            try
            {
                int expensesCount = int.Parse(_tokens[_index++]);

                inputNumerics_.Enqueue(expensesCount);

                for (int i = 0; i < expensesCount; i++)
                {
                    ParseExpense(_tokens, ref _index);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ParseExpense(List<string> _tokens, ref int _index)
        {
            try
            {
                decimal amount = decimal.Parse(_tokens[_index++]);
                inputNumerics_.Enqueue(amount);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
