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
                Console.WriteLine("Trip");
                Console.WriteLine("Participant Count: {0}", participantsCount); //TODO

                while (participantsCount > 0 && tokenIndex < _tokens.Count)
                {

                    inputNumerics_.Enqueue(participantsCount);

                    for (int i = 0; i < participantsCount; i++)
                    {
                        Console.WriteLine("Participant");
                        ParseParticipant(_tokens, ref tokenIndex);
                    }

                    participantsCount = int.Parse(_tokens[tokenIndex++]);
                    Console.WriteLine("Trip");
                    Console.WriteLine("Participant Count: {0}", participantsCount); //TODO
                }
                inputNumerics_.Enqueue(participantsCount);
            }
            catch (Exception exception)
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
                Console.WriteLine("Expense Count {0}", expensesCount); //TODO

                inputNumerics_.Enqueue(expensesCount);

                for (int i = 0; i < expensesCount; i++)
                {
                    ParseExpense(_tokens, ref _index);
                }

            }
            catch (Exception)
            {

            }
        }

        private void ParseExpense(List<string> _tokens, ref int _index)
        {
            try
            {
                double amount = double.Parse(_tokens[_index++]);
                Console.WriteLine("Expense: {0}", amount); //TODO
                inputNumerics_.Enqueue(amount);
            }
            catch (Exception)
            {

            }
        }
    }
}
