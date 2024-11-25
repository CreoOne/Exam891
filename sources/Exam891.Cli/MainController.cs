using Exam891.Cli.CommandLine;
using Exam891.Core.Queries.Availability;
using Exam891.Core.Queries.Search;
using System.Text;

namespace Exam891.Cli
{
    internal class MainController
    {
        private readonly ISearchQuery _searchQuery;
        private readonly IAvailabilityQuery _availabilityQuery;
        private const string DateOnlyFormat = "yyyyMMdd";

        public MainController(ISearchQuery searchQuery, IAvailabilityQuery availabilityQuery)
        {
            _searchQuery = searchQuery;
            _availabilityQuery = availabilityQuery;
        }

        public string Execute(Command command)
            => command.Name switch
            {
                "Search" => ExecuteSearch(command),
                "Availability" => ExecuteAvailability(command),
                _ => "Unknown command",
            };

        private string ExecuteSearch(Command command)
        {
            var hotelId = command.Parameters[0];
            var length = int.Parse(command.Parameters[1]);
            var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var endDate = startDate.AddDays(length);
            var roomType = command.Parameters[2];
            var results = _searchQuery.Search(hotelId, startDate, endDate, roomType).ToArray();
            return SerializeSearchResults(results);
        }

        private string ExecuteAvailability(Command command)
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

        private static string SerializeSearchResults(SearchResult[] results)
        {
            if (results.Length == 0)
                return string.Empty;

            var builder = new StringBuilder();

            for (var index = 0; index < results.Length; index++)
            {
                builder.Append('(');
                builder.Append(results[index].From.ToString(DateOnlyFormat));
                builder.Append('-');
                builder.Append(results[index].To.ToString(DateOnlyFormat));
                builder.Append(", ");
                builder.Append(results[index].AvailableRoomsCount);
                builder.Append(')');

                if (index < results.Length - 1)
                    builder.Append(",\n");
            }

            return builder.ToString();
        }
    }
}
