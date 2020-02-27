﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSplitterConsole.Model
{
    internal class Trip
    {
        private Dictionary<int, Participant> participants_;

        public Trip()
        {
            participants_ = new Dictionary<int, Participant>();
        }

        public int AddParticipant()
        {
            Participant participant = new Participant();
            participants_.Add(participant.ID, participant);
            return participant.ID;
        }

        public void AddExpense(int _participantID, double _amount)
        {
            participants_[_participantID].AddExpense(_amount);
        }

        public void SettleBalance()
        {
            // TODO
            double totalExpenses = (from p in participants_.Values select p.GetTotalExpenses()).Sum();
            double averageExpense = totalExpenses / participants_.Values.Count;

            foreach (Participant participant in participants_.Values)
            {
                participant.SetTripBalance(averageExpense - participant.GetTotalExpenses());
            }
        }

        public IEnumerable<ParticipantBalance> GetBalances()
        {
            List<ParticipantBalance> balances = new List<ParticipantBalance>();

            foreach (Participant participant in participants_.Values)
            {
                ParticipantBalance balance = new ParticipantBalance(participant.ID, participant.GetTripBalance());
                balances.Add(balance);
            }

            return balances;
        }
    }
}
