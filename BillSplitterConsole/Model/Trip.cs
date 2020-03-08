using System.Collections.Generic;
using System.Linq;

namespace BillSplitterConsole.Model
{
    public class Trip
    {
        private readonly Dictionary<int, Participant> _participants;

        public Trip()
        {
            _participants = new Dictionary<int, Participant>();
        }

        public int AddParticipant()
        {
            var participant = new Participant();
            _participants.Add(participant.Id, participant);
            return participant.Id;
        }

        public void AddExpense(int participantId, decimal amount)
        {
            _participants[participantId].AddExpense(amount);
        }

        public void SettleBalance()
        {
            var totalExpenses = (from p in _participants.Values select p.GetTotalExpenses()).Sum();
            var averageExpense = totalExpenses / _participants.Values.Count;

            foreach (var participant in _participants.Values)
                participant.SetTripBalance(averageExpense - participant.GetTotalExpenses());
        }

        public IEnumerable<ParticipantBalance> GetBalances()
        {
            var balances = new List<ParticipantBalance>();

            foreach (var participant in _participants.Values)
            {
                var balance = new ParticipantBalance(participant.Id, participant.GetTripBalance());
                balances.Add(balance);
            }

            return balances;
        }
    }
}