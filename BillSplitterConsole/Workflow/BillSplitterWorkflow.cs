using BillSplitterConsole.Infrastructure;
using BillSplitterConsole.Model;
using System;
using System.Collections.Generic;

namespace BillSplitterConsole.Workflow
{
    public class BillSplitterWorkflow
    {
        private List<Trip> trips_;

        public BillSplitterWorkflow()
        {
            trips_ = new List<Trip>();
        }

        public void SplitBill(string _tripInputFilePath, string _paymentOutputFilePath)
        {
            ExpenseReader expenseReader = new ExpenseReader();
            Queue<object> expenseQueue = expenseReader.ReadExpenses(_tripInputFilePath);

            AllocateExpenses(expenseQueue);
            AllocatePayments(trips_);

            PaymentWriter paymentWriter = new PaymentWriter();
            paymentWriter.WritePayments(_paymentOutputFilePath, trips_);
        }

        public List<Trip> AllocateExpenses(Queue<object> _expenseQueue)
        {
            if (_expenseQueue == null || _expenseQueue.Count == 0)
            {
                throw new ArgumentException("expense queue must be instantiated with at least one queue element");
            }

            int participantCount = (int)_expenseQueue.Dequeue();

            while (participantCount > 0)
            {
                Trip trip = AddTrip();

                for (int elementCount = 0; elementCount < participantCount; elementCount++)
                {
                    int participantID = AddParticipant(trip);
                    AllocateParticipantExpenses(_expenseQueue, trip, participantID);
                }
                participantCount = (int)_expenseQueue.Dequeue();
            }

            return trips_;
        }

        private void AllocateParticipantExpenses(Queue<object> _expenseQueue, Trip _trip, int _participantID)
        {
            int expenseCount = (int)_expenseQueue.Dequeue();
            for (int elementCount = 0; elementCount < expenseCount; elementCount++)
            {
                decimal amount = (decimal)_expenseQueue.Dequeue();
                AddExpense(_trip, _participantID, amount);
            }
        }

        private Trip AddTrip()
        {
            Trip trip = new Trip();
            trips_.Add(trip);
            return trip;
        }

        private int AddParticipant(Trip _trip)
        {
            int participantID = _trip.AddParticipant();
            return participantID;
        }

        private void AddExpense(Trip _trip, int _participantID, decimal _amount)
        {
            _trip.AddExpense(_participantID, _amount);
        }

        public List<Trip> AllocatePayments(List<Trip> _trips)
        {
            if (_trips == null)
            {
                throw new ArgumentException("Trip list must be instantiated");
            }

            foreach (Trip trip in _trips)
            {
                trip.SettleBalance();
            }

            return _trips;
        }
    }
}
