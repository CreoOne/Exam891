using Exam891.Cli.CommandLine;
using Exam891.Core.Queries.Search;
using System.Text;

namespace Exam891.Cli.Commands
{
    internal sealed class SearchCommand : ICommand
    {
        private readonly ISearchQuery _searchQuery;
        private const string DateOnlyFormat = "yyyyMMdd";

        public string Name => "Search";

        public SearchCommand(ISearchQuery searchQuery)
            => _searchQuery = searchQuery;

        public string Execute(Command command)
        {
            var hotelId = command.Parameters[0];
            var length = int.Parse(command.Parameters[1]);
            var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var endDate = startDate.AddDays(length);
            var roomType = command.Parameters[2];
            var results = _searchQuery.Search(hotelId, startDate, endDate, roomType).ToArray();
            return SerializeSearchResults(results);
        }

        private static string SerializeSearchResults(SearchResult[] results)
        {
            if (results.Length == 0)
                return string.Empty;

            var builder = new StringBuilder();

            for (var index = 0; index < results.Length; index++)
            {
                var result = results[index];

                if (result.AvailableRoomsCount <= 0)
                    continue;

                SerializeSearchResult(builder, result);

                if (index < results.Length - 1)
                    builder.Append(",\n");
            }

            return builder.ToString();
        }

        private static void SerializeSearchResult(StringBuilder stringBuilder, SearchResult result)
            => stringBuilder
                .Append('(')
                .Append(result.From.ToString(DateOnlyFormat))
                .Append('-')
                .Append(result.To.ToString(DateOnlyFormat))
                .Append(", ")
                .Append(result.AvailableRoomsCount)
                .Append(')');
    }
}
