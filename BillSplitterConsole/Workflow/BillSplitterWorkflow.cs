using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Model;
using System;
using System.Collections.Generic;

namespace BillSplitterConsole.Workflow
{
    public class BillSplitterWorkflow
    {
        private readonly List<Trip> _trips;

        public BillSplitterWorkflow()
        {
            _trips = new List<Trip>();
        }

       // public void SplitBill(string _tripInputFilePath, string _paymentOutputFilePath)
        public void SplitBill(IExpenseReader expenseReader, IPaymentWriter paymentWriter)
        {
            if (expenseReader == null || paymentWriter == null)
            {
                throw new ArgumentException("ExpenseReader and paymentWriter must be instantiated");
            }

            //var expenseReader = new IExpenseReader();
           // IExpenseReader expenseReader;
            var expenseQueue = expenseReader.ReadExpenses();

            //using (var scope = Container.BeginLifetimeScope())
            //{
            //    var service = scope.Resolve<IService>();
            //}

            AllocateExpenses(expenseQueue);
            AllocatePayments(_trips);

           // var paymentWriter = new FilePaymentWriter();
            paymentWriter.WritePayments(_trips);
        }

        public List<Trip> AllocateExpenses(Queue<object> expenseQueue)
        {
            if (expenseQueue == null || expenseQueue.Count == 0)
                throw new ArgumentException("Expense queue must be instantiated with at least one queue element");

            var participantCount = (int) expenseQueue.Dequeue();

            while (participantCount > 0)
            {
                var trip = AddTrip();

                for (var elementCount = 0; elementCount < participantCount; elementCount++)
                {
                    var participantId = AddParticipant(trip);
                    AllocateParticipantExpenses(expenseQueue, trip, participantId);
                }

                participantCount = (int) expenseQueue.Dequeue();
            }

            return _trips;
        }

        private void AllocateParticipantExpenses(Queue<object> expenseQueue, Trip trip, int participantId)
        {
            var expenseCount = (int) expenseQueue.Dequeue();
            for (var elementCount = 0; elementCount < expenseCount; elementCount++)
            {
                var amount = (decimal) expenseQueue.Dequeue();
                AddExpense(trip, participantId, amount);
            }
        }

        private Trip AddTrip()
        {
            var trip = new Trip();
            _trips.Add(trip);
            return trip;
        }

        private static int AddParticipant(Trip trip)
        {
            var participantId = trip.AddParticipant();
            return participantId;
        }

        private static void AddExpense(Trip trip, int participantId, decimal amount)
        {
            trip.AddExpense(participantId, amount);
        }

        public static List<Trip> AllocatePayments(List<Trip> trips)
        {
            if (trips == null) throw new ArgumentException("Trip list must be instantiated");

            foreach (var trip in trips) trip.SettleBalance();

            return trips;
        }
    }
}