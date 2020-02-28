namespace BillSplitterConsole.Model
{
    public class ParticipantBalance
    {
        public ParticipantBalance(int _participantID, decimal _balance)
        {
            ParticipantID = _participantID;
            Balance = _balance;
        }

        public int ParticipantID { get; }

        public decimal Balance { get; }
    }
}
