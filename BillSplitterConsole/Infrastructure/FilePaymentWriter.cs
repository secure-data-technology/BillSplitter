using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BillSplitterConsole.Model;

namespace BillSplitterConsole.Infrastructure
{
    public class FilePaymentWriter : IPaymentWriter
    {
        private readonly List<string> _tokenList;
        private readonly string _instanceFilePath;

        public FilePaymentWriter(string filePath)
        {
            _tokenList = new List<string>();
            _instanceFilePath = filePath;
        }

        public void WritePayments(IEnumerable<Trip> trips)
        {
            TokenizeTrips(trips.ToList());
            var tokenArray = _tokenList.ToArray();
            File.WriteAllLines(_instanceFilePath, tokenArray);
        }

        private void TokenizeTrips(List<Trip> trips)
        {
            foreach (var trip in trips) TokenizeTrip(trip);
        }

        private void TokenizeTrip(Trip trip)
        {
            if (_tokenList.Count > 0) _tokenList.Add(string.Empty);

            var participantBalances = (List<ParticipantBalance>) trip.GetBalances();
            foreach (var participantBalance in participantBalances) TokenizeParticipantBalance(participantBalance);
        }

        private void TokenizeParticipantBalance(ParticipantBalance participantBalance)
        {
            var nfi = new NumberFormatInfo
            {
                NumberNegativePattern = 0,
                CurrencySymbol = "$"
            };

            var token = participantBalance.Balance.ToString("C2", nfi);
            _tokenList.Add(token);
        }
    }
}