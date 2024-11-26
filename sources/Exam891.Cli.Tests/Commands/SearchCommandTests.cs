using Exam891.Cli.CommandLine;
using Exam891.Cli.Commands;
using Exam891.Core.Queries.Search;
using System.Text.RegularExpressions;

namespace Exam891.Cli.Tests.Commands
{
    public class SearchCommandTests
    {
        [Fact]
        public void Execute_WithCorrectParameters_GetsCalled()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var command = new SearchCommand(searchQuery);

            // Act
            _ = command.Execute(new Command("Search", ["H9", "20", "CDG"]));

            // Assert
            searchQuery.Received(1).Search("H9", Arg.Any<DateOnly>(), Arg.Any<DateOnly>(), "CDG");
        }

        [Fact]
        public void Execute_WithCorrectParameters_ReturnsCorrectlyFormatedResponse()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            searchQuery
                .Search("H9", Arg.Any<DateOnly>(), Arg.Any<DateOnly>(), "CDG")
                .Returns([new SearchResult(new DateOnly(2026, 04, 05), new DateOnly(2026, 04, 25), 2)]);
            var command = new SearchCommand(searchQuery);

            // Act
            var result = command.Execute(new Command("Search", ["H9", "20", "CDG"]));

            // Assert
            Assert.Equal("(20260405-20260425, 2)", result);
        }
    }
}
