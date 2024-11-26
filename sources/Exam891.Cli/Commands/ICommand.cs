using Exam891.Cli.CommandLine;

namespace Exam891.Cli.Commands
{
    internal interface ICommand
    {
        string Name { get; }
        string Execute(Command command);
    }
}
