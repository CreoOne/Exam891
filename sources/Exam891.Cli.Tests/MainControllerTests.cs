using Exam891.Cli.CommandLine;
using Exam891.Cli.Commands;

namespace Exam891.Cli.Tests
{
    public class MainControllerTests
    {
        [Fact]
        public void Execute_WithNoCommands_ReturnsUnknownCommand()
        {
            // Arrange
            var controller = new MainController();
            var command = new Command("SomeCommand", ["H9", "20", "XDG"]);

            // Act
            var result = controller.Execute(command);

            // Assert
            Assert.Equal("Unknown command", result);
        }

        [Fact]
        public void Execute_WithOneCommand_GetsCalled()
        {
            // Arrange
            var oneCommand = Substitute.For<ICommand>();
            oneCommand.Name.Returns("OneCommand");
            var controller = new MainController();
            controller.Add(oneCommand);
            var command = new Command("OneCommand", ["H9", "20", "XDG"]);

            // Act
            _ = controller.Execute(command);

            // Assert
            oneCommand.Received(1).Execute(command);
        }
    }
}
