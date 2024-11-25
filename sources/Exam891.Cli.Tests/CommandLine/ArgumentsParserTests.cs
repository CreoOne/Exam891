using Exam891.Cli.CommandLine;

namespace Exam891.Cli.Tests.CommandLine
{
    public class ArgumentsParserTests
    {
        [Fact]
        public void Parse_WithNoArguments_ReturnsEmptyArguments()
        {
            // Arrange
            var args = Array.Empty<string>();
            var result = ArgumentsParser.Parse(args);

            // Act

            // Assert
            Assert.Empty(result.BookingsFilePath);
            Assert.Empty(result.HotelsFilePath);
        }

        [Fact]
        public void Parse_WithBookingsArgument_ReturnsArgumentsWithBookingsFilePath()
        {
            // Arrange
            var args = new[] { "--bookings", "bookings.csv" };
            var result = ArgumentsParser.Parse(args);

            // Act

            // Assert
            Assert.Empty(result.HotelsFilePath);
            Assert.Equal("bookings.csv", result.BookingsFilePath);
        }

        [Fact]
        public void Parse_WithHotelsArgument_ReturnsArgumentsWithHotelsFilePath()
        {
            // Arrange
            var args = new[] { "--hotels", "hotels.csv" };
            var result = ArgumentsParser.Parse(args);

            // Act

            // Assert
            Assert.Empty(result.BookingsFilePath);
            Assert.Equal("hotels.csv", result.HotelsFilePath);
        }

        [Fact]
        public void Parse_WithBothArguments_ReturnsArgumentsWithBothFilePaths()
        {
            // Arrange
            var args = new[] { "--hotels", "hotels.csv", "--bookings", "bookings.csv" };
            var result = ArgumentsParser.Parse(args);

            // Act

            // Assert
            Assert.Equal("hotels.csv", result.HotelsFilePath);
            Assert.Equal("bookings.csv", result.BookingsFilePath);
        }

        [Fact]
        public void Parse_WithMessyArguments_ReturnsArgumentsWithBothFilePaths()
        {
            // Arrange
            var args = new[] { "", " ", "--bookings", "bookings.csv", " ", "--hotels", "hotels.csv" };
            var result = ArgumentsParser.Parse(args);
            // Act
            // Assert
            Assert.Equal("hotels.csv", result.HotelsFilePath);
            Assert.Equal("bookings.csv", result.BookingsFilePath);
        }
    }
}
