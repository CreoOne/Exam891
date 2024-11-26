using Exam891.Cli.CommandLine;
using Exam891.Cli.Commands;
using Exam891.Core.Queries.Availability;

namespace Exam891.Cli.Tests.Commands
{
    public sealed class AvailabilityCommandTests
    {
        [Fact]
        public void Execute_WithCorrectParametersSpecificDate_GetsCalled()
        {
            // Arrange
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var command = new AvailabilityCommand(availabilityQuery);

            // Act
            _ = command.Execute(new Command("Availability", ["H9", "20220405", "CDG"]));

            // Assert
            availabilityQuery.Received(1).Availability("H9", new DateOnly(2022, 04, 05), new DateOnly(2022, 04, 05), "CDG");
        }

        [Fact]
        public void Execute_WithCorrectParametersRangeDate_GetsCalled()
        {
            // Arrange
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            var command = new AvailabilityCommand(availabilityQuery);

            // Act
            _ = command.Execute(new Command("Availability", ["H9", "20220405-20220407", "CDG"]));

            // Assert
            availabilityQuery.Received(1).Availability("H9", new DateOnly(2022, 04, 05), new DateOnly(2022, 04, 07), "CDG");
        }

        [Fact]
        public void Execute_WithCorrectParameters_ReturnsCorrectlyFormatedResponse()
        {
            // Arrange
            var availabilityQuery = Substitute.For<IAvailabilityQuery>();
            availabilityQuery
                .Availability("H9", new DateOnly(2022, 04, 05), new DateOnly(2022, 04, 07), "CDG")
                .Returns(2);
            var command = new AvailabilityCommand(availabilityQuery);

            // Act
            var result = command.Execute(new Command("Availability", ["H9", "20220405-20220407", "CDG"]));

            // Assert
            Assert.Equal("2", result);
        }
    }
}
