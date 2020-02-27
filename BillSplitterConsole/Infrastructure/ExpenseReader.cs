using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSplitterConsole.Infrastructure
{
    internal class ExpenseReader
    {
        private Queue<object> inputNumerics_;

        public ExpenseReader()
        {
            //ReadExpenses(_filePath);
            inputNumerics_ = new Queue<object>();
        }

        public Queue<object> ReadExpenses(string _filePath)
        {
            List<string> inputLines = File.ReadLines(_filePath).ToList();
            GetParsedTokens(inputLines);
            return inputNumerics_;
        }

        // public List<string> Lines { get; private set; }

        private void GetParsedTokens(List<string> _tokens)
        {
            try
            {
                int tokenIndex = 0;
                int participantsCount = int.Parse(_tokens[tokenIndex]);

                //Console.WriteLine(participantsCount); //TODO

                while (participantsCount != 0)
                {
                    //inputNumerics_.Add(participantsCount);
                    inputNumerics_.Enqueue(participantsCount);

                    for (int i = 0; i < participantsCount; i++)
                    {
                        ParseParticipant(_tokens, ref tokenIndex);
                    }

                    participantsCount = int.Parse(_tokens[tokenIndex]);
                }

               // return inputNumerics_;
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
            int expensesCount = int.Parse(_tokens[_index++]);

            //Console.WriteLine(expensesCount); //TODO

            //inputNumerics_.Add(expensesCount);
            inputNumerics_.Enqueue(expensesCount);

            for (int i = 0; i < expensesCount; i++)
            {
                ParseExpense(_tokens, ref _index);
            }
        }

        private void ParseExpense(List<string> _tokens, ref int _index)
        {
            double amount = double.Parse(_tokens[_index++]);
            inputNumerics_.Enqueue(amount);
        }

    }
}
