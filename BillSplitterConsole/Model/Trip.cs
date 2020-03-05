using System.Collections.Generic;
using System.Linq;

namespace BillSplitterConsole.Model
{
    public class Trip
    {
        private readonly Dictionary<int, Participant> participants_;

        public Trip()
        {
            participants_ = new Dictionary<int, Participant>();
        }

        public int AddParticipant()
        {
            var participant = new Participant();
            participants_.Add(participant.ID, participant);
            return participant.ID;
        }

        public int AddParticipant(Participant _participant)
        {
            participants_.Add(_participant.ID, _participant);
            return _participant.ID;
        }

        public void AddExpense(int _participantID, decimal _amount)
        {
            participants_[_participantID].AddExpense(_amount);
        }

        public void SettleBalance()
        {
            // TODO
            var totalExpenses = (from p in participants_.Values select p.GetTotalExpenses()).Sum();
            var averageExpense = totalExpenses / participants_.Values.Count;

            foreach (var participant in participants_.Values)
                participant.SetTripBalance(averageExpense - participant.GetTotalExpenses());
        }

        public IEnumerable<ParticipantBalance> GetBalances()
        {
            var balances = new List<ParticipantBalance>();

            foreach (var participant in participants_.Values)
            {
                var balance = new ParticipantBalance(participant.ID, participant.GetTripBalance());
                balances.Add(balance);
            }

            return balances;
        }
    }
}