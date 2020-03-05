using System;
using System.Collections.Generic;
using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Model;

namespace BillSplitterConsole.Workflow
{
    public class BillSplitterWorkflow
    {
        private readonly List<Trip> trips_;

        public BillSplitterWorkflow()
        {
            trips_ = new List<Trip>();
        }

        public void SplitBill(string _tripInputFilePath, string _paymentOutputFilePath)
        {
            var expenseReader = new ExpenseReader();
            var expenseQueue = expenseReader.ReadExpenses(_tripInputFilePath);

            AllocateExpenses(expenseQueue);
            AllocatePayments(trips_);

            var paymentWriter = new PaymentWriter();
            paymentWriter.WritePayments(_paymentOutputFilePath, trips_);
        }

        public List<Trip> AllocateExpenses(Queue<object> _expenseQueue)
        {
            if (_expenseQueue == null || _expenseQueue.Count == 0)
                throw new ArgumentException("expense queue must be instantiated with at least one queue element");

            var participantCount = (int) _expenseQueue.Dequeue();

            while (participantCount > 0)
            {
                var trip = AddTrip();

                for (var elementCount = 0; elementCount < participantCount; elementCount++)
                {
                    var participantID = AddParticipant(trip);
                    AllocateParticipantExpenses(_expenseQueue, trip, participantID);
                }

                participantCount = (int) _expenseQueue.Dequeue();
            }

            return trips_;
        }

        private void AllocateParticipantExpenses(Queue<object> _expenseQueue, Trip _trip, int _participantID)
        {
            var expenseCount = (int) _expenseQueue.Dequeue();
            for (var elementCount = 0; elementCount < expenseCount; elementCount++)
            {
                var amount = (decimal) _expenseQueue.Dequeue();
                AddExpense(_trip, _participantID, amount);
            }
        }

        private Trip AddTrip()
        {
            var trip = new Trip();
            trips_.Add(trip);
            return trip;
        }

        private int AddParticipant(Trip _trip)
        {
            var participantID = _trip.AddParticipant();
            return participantID;
        }

        private void AddExpense(Trip _trip, int _participantID, decimal _amount)
        {
            _trip.AddExpense(_participantID, _amount);
        }

        public List<Trip> AllocatePayments(List<Trip> _trips)
        {
            if (_trips == null) throw new ArgumentException("Trip list must be instantiated");

            foreach (var trip in _trips) trip.SettleBalance();

            return _trips;
        }
    }
}