using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BillSplitterConsole.Model;

namespace BillSplitterConsole.Infrastructure
{
    internal class PaymentWriter
    {
        private readonly List<string> tokenList_;

        public PaymentWriter()
        {
            tokenList_ = new List<string>();
        }

        public void WritePayments(string _outputPath, List<Trip> _trips)
        {
            var tokenList = TokenizeTrips(_trips);
            var tokenArray = tokenList.ToArray();
            File.WriteAllLines(_outputPath, tokenArray);
        }

        private List<string> TokenizeTrips(List<Trip> _trips)
        {
            foreach (var trip in _trips) TokenizeTrip(trip);

            return tokenList_;
        }

        private void TokenizeTrip(Trip trip)
        {
            if (tokenList_.Count > 0) tokenList_.Add(string.Empty);

            var participantBalances = (List<ParticipantBalance>) trip.GetBalances();
            foreach (var participantBalance in participantBalances) TokenizeParticipantBalance(participantBalance);
        }

        private void TokenizeParticipantBalance(ParticipantBalance participantBalance)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberNegativePattern = 0;
            nfi.CurrencySymbol = "$";

            var token = participantBalance.Balance.ToString("C2", nfi);
            tokenList_.Add(token);
        }
    }
}