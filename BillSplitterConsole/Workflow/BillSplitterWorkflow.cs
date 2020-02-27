using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Model;
using BillSplitterConsole.Workflow;

namespace BillSplitterConsole.Workflow
{
    internal class BillSplitterWorkflow
    {
        private List<Trip> trips_;

        public BillSplitterWorkflow()
        {
            trips_ = new List<Trip>();
        }

        public void SplitBill(string tripInputFilePath)
        {
            ExpenseReader expenseReader = new ExpenseReader();
            Queue<object> expenseQueue = expenseReader.ReadExpenses(tripInputFilePath);

        }

        private void BuildObjectGraph(Queue<object> expenseQueue)
        {
            int participantCount = (int)expenseQueue.Dequeue();

            Trip trip = new Trip();

            for (int elementCount = 0; elementCount < participantCount; elementCount++)
            {
                int expenseCount = (int)expenseQueue.Dequeue();

            }

         }

        //public Queue<object> GetParsedTokens(List<string> _tokens)
        //{
        //    try
        //    {
        //        int tokenIndex = 0;
        //        int participantsCount = int.Parse(_tokens[tokenIndex]);

        //        //Console.WriteLine(participantsCount); //TODO

        //        while (participantsCount != 0)
        //        {
        //            //inputNumerics_.Add(participantsCount);
        //            inputNumerics_.Enqueue(participantsCount);

        //            for (int i = 0; i < participantsCount; i++)
        //            {
        //                ParseParticipant(_tokens, ref tokenIndex);
        //            }

        //            participantsCount = int.Parse(_tokens[tokenIndex]);
        //        }

        //        return inputNumerics_;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally 
        //    { 
        //    }
        //}

        //private void ParseParticipant(List<string> _tokens, ref int _index)
        //{
        //    int expensesCount = int.Parse(_tokens[_index++]);

        //    //Console.WriteLine(expensesCount); //TODO

        //    //inputNumerics_.Add(expensesCount);
        //    inputNumerics_.Enqueue(expensesCount);

        //    for (int i = 0; i < expensesCount; i++)
        //    {
        //        ParseExpense(_tokens, ref _index);
        //    }
        //}

        //private void ParseExpense(List<string> _tokens, ref int _index)
        //{
        //    double amount = double.Parse(_tokens[_index++]);
        //    inputNumerics_.Enqueue(amount);
        //}

        private void AddTrip()
        {

        }
    }
}
