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

        public void SplitBill(string _tripInputFilePath)
        {
            ExpenseReader expenseReader = new ExpenseReader();
            Queue<object> expenseQueue = expenseReader.ReadExpenses(_tripInputFilePath);
            AllocateExpenses(expenseQueue);
            AllocatePayments(trips_);
        }

        private void AllocateExpenses(Queue<object> _expenseQueue)
        {
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
 
         }

        private void AllocateParticipantExpenses(Queue<object> _expenseQueue, Trip _trip, int _participantID)
        {
            int expenseCount = (int)_expenseQueue.Dequeue();
            for (int elementCount = 0; elementCount < expenseCount; elementCount++)
            {
                double amount = (double)_expenseQueue.Dequeue();
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

        private void AddExpense(Trip _trip, int _participantID, double _amount)
        {
            _trip.AddExpense(_participantID, _amount);
        }

        private void AllocatePayments(List<Trip> _trips)
        {
            foreach (Trip trip in _trips)
            {
                trip.SettleBalance();
                trip.GetBalances();

            }
        }
    }
}
