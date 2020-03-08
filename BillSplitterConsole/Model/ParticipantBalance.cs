namespace BillSplitterConsole.Model
{
    public class ParticipantBalance
    {
        public ParticipantBalance(int participantId, decimal balance)
        {
            ParticipantId = participantId;
            Balance = balance;
        }

        public int ParticipantId { get; }

        public decimal Balance { get; }
    }
}