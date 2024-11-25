using Exam891.Cli.CommandLine;
using Exam891.Core.Queries.Availability;
using Exam891.Core.Queries.Search;

namespace Exam891.Cli.Tests
{
    public class MainControllerTests
    {
        [Fact]
        public void Execute_WithSearchCommand_ReturnsSearchResults()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Search", ["H9", "20", "XDG"]);

            // Act
            _ = controller.Execute(command);

            // Assert
            searchQuery.Received(1).Search("H9", Arg.Any<DateOnly>(), Arg.Any<DateOnly>(), "XDG");
        }

        [Fact]
        public void Execute_WithAvailabilityCommandSpecificDate_ReturnsAvailabilityResults()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Availability", ["H9", "20220701", "XDG"]);

            // Act
            _ = controller.Execute(command);

            // Assert
            availabilityQuery.Received(1).Availability("H9", new DateOnly(2022, 07, 01), new DateOnly(2022, 07, 01), "XDG");
        }

        [Fact]
        public void Execute_WithAvailabilityCommandRangeDate_ReturnsAvailabilityResults()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Availability", ["H9", "20220902-20220907", "XDG"]);

            // Act
            _ = controller.Execute(command);

            // Assert
            availabilityQuery.Received(1).Availability("H9", new DateOnly(2022, 09, 02), new DateOnly(2022, 09, 07), "XDG");
        }

        [Fact]
        public void Execute_WithUnknownCommand_ReturnsUnknownCommandMessage()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Unknown", ["param1", "param2"]);

            // Act
            var result = controller.Execute(command);

            // Assert
            Assert.Equal("Unknown command", result);
        }

        [Fact]
        public void Execute_WithIncorrectDate_ReturnsErrorMessage()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Availability", ["H9", "20220z02", "XDG"]);

            // Act
            var result = controller.Execute(command);

            // Assert
            Assert.Equal("Could not parse date", result);
        }

        [Fact]
        public void Execute_WithIncorrectDateRangeBeginning_ReturnsErrorMessage()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Availability", ["H9", "202202-20220907", "XDG"]);

            // Act
            var result = controller.Execute(command);

            // Assert
            Assert.Equal("Could not parse date range beginning", result);
        }

        [Fact]
        public void Execute_WithIncorrectDateRangeEnd_ReturnsErrorMessage()
        {
            // Arrange
            var searchQuery = Substitute.For<ISearchQuery>();
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var controller = new MainController(searchQuery, availabilityQuery);
            var command = new Command("Availability", ["H9", "20220902-20220z07", "XDG"]);

            // Act
            var result = controller.Execute(command);

            // Assert
            Assert.Equal("Could not parse date range end", result);
        }
    }
}
