using Exam891.Cli.CommandLine;
using Exam891.Cli.Commands;

namespace Exam891.Cli
{
    internal sealed class MainController
    {
        private readonly Dictionary<string, ICommand> _commands = new();

        public void Add(ICommand command)
            => _commands.Add(command.Name, command);

        public string Execute(Command command)
        {
            if (!_commands.TryGetValue(command.Name, out var commandImplementation))
                return "Unknown command";

            return commandImplementation.Execute(command);
        }
    }
}
