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

        public void SplitBill(IExpenseReader expenseReader, IPaymentWriter paymentWriter)
        {
            if (expenseReader == null || paymentWriter == null)
            {
                throw new ArgumentException(Resources.InvalidSplitBillArguments);
            }

            var expenseQueue = expenseReader.ReadExpenses();

            AllocateExpenses(expenseQueue);
            AllocatePayments(_trips);

            paymentWriter.WritePayments(_trips);
        }

        public List<Trip> AllocateExpenses(Queue<object> expenseQueue)
        {
            if (expenseQueue == null || expenseQueue.Count == 0)
                throw new ArgumentException(Resources.InvalidExpenseQueue);

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
            if (trips == null) throw new ArgumentException(Resources.InvalidTripList);

            foreach (var trip in trips) trip.SettleBalance();

            return trips;
        }
    }
}