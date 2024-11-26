using Exam891.Cli.CommandLine;
using Exam891.Core.Queries.Availability;

namespace Exam891.Cli.Commands
{
    internal sealed class AvailabilityCommand : ICommand
    {
        private readonly IAvailabilityQuery _availabilityQuery;
        private const string DateOnlyFormat = "yyyyMMdd";

        public string Name => "Availability";

        public AvailabilityCommand(IAvailabilityQuery availabilityQuery)
            => _availabilityQuery = availabilityQuery;

        public string Execute(Command command)
        {
            var hotelId = command.Parameters[0];
            var rawDate = command.Parameters[1];
            var roomType = command.Parameters[2];

            if (rawDate.Contains('-'))
                return ExecuteRangeAvailability(hotelId, rawDate, roomType);

            return ExecuteSpecificAvailability(hotelId, rawDate, roomType);
        }

        private string ExecuteRangeAvailability(string hotelId, string rawDate, string roomType)
        {
            var dates = rawDate.Split('-');

            if (dates.Length != 2)
                return "Invalid date range";

            if (!DateOnly.TryParseExact(dates[0].TrimEnd(), DateOnlyFormat, out var from))
                return "Could not parse date range beginning";

            if (!DateOnly.TryParseExact(dates[1].TrimStart(), DateOnlyFormat, out var to))
                return "Could not parse date range end";

            return _availabilityQuery
                .Availability(hotelId, from, to, roomType)
                .ToString();
        }

        private string ExecuteSpecificAvailability(string hotelId, string rawDate, string roomType)
        {
            if (!DateOnly.TryParseExact(rawDate, DateOnlyFormat, out var date))
                return "Could not parse date";

            return _availabilityQuery
                .Availability(hotelId, date, date, roomType)
                .ToString();
        }
    }
}
