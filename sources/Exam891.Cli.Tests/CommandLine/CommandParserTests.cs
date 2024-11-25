using Exam891.Cli.CommandLine;

namespace Exam891.Cli.Tests.CommandLine
{
    public class CommandParserTests
    {
        [Fact]
        public void Parse_WithNoArguments_ReturnsEmptyCommand()
        {
            // Arrange
            var command = "";

            // Act
            void Act() => CommandParser.Parse(command);

            // Assert
            Assert.Throws<Exception>(Act);
        }

        [Fact]
        public void Parse_WithIncorrectCommandFormat_ThrowsException()
        {
            // Arrange
            var command = "Search";

            // Act
            void Act() => CommandParser.Parse(command);

            // Assert
            Assert.Throws<Exception>(Act);
        }

        [Fact]
        public void Parse_WithIncorrectCommandName_ThrowsException()
        {
            // Arrange
            var command = "(param1, param2)";

            // Act
            void Act() => CommandParser.Parse(command);

            // Assert
            Assert.Throws<Exception>(Act);
        }

        [Fact]
        public void Parse_WithCorrectCommand_ReturnsCommand()
        {
            // Arrange
            var command = "Search(param1, param2)";

            // Act
            var result = CommandParser.Parse(command);

            // Assert
            Assert.Equal("Search", result.Name);
            Assert.Equal(["param1", "param2"], result.Parameters);
        }
    }
}
