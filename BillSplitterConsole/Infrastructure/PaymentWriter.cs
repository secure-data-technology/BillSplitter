using BillSplitterConsole.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BillSplitterConsole.Infrastructure
{
    internal class PaymentWriter
    {
        private List<string> tokenList_;
        public PaymentWriter()
        {
            tokenList_ = new List<string>();
        }

        public void WritePayments(string _outputPath, List<Trip> _trips)
        {
            List<string> tokenList = TokenizeTrips(_trips);
            string[] tokenArray = tokenList.ToArray();
            File.WriteAllLines(_outputPath, tokenArray);
        }

        private List<string> TokenizeTrips(List<Trip> _trips)
        {
            foreach (Trip trip in _trips)
            {
                TokenizeTrip(trip);
            }

            return tokenList_;
        }

        private void TokenizeTrip(Trip trip)
        {
            if (tokenList_.Count > 0)
            {
                tokenList_.Add(string.Empty);
            }

            List<ParticipantBalance> participantBalances = (List<ParticipantBalance>)trip.GetBalances();
            foreach (ParticipantBalance participantBalance in participantBalances)
            {
                TokenizeParticipantBalance(participantBalance);
            }
        }

        private void TokenizeParticipantBalance(ParticipantBalance participantBalance)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberNegativePattern = 0;
            nfi.CurrencySymbol = "$";

            string token = participantBalance.Balance.ToString("C2", nfi);
            tokenList_.Add(token);
        }
    }
}
