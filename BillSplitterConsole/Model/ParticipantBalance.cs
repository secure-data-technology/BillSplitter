namespace BillSplitterConsole.Model
{
    internal class ParticipantBalance
    {
        public ParticipantBalance(int _participantID, double _balance)
        {
            ParticipantID = _participantID;
            Balance = _balance;
        }

        public int ParticipantID { get; }

        public double Balance { get; }
    }
}
