namespace Exam891.Cli.CommandLine
{
    internal static class CommandParser
    {
        private const char OpeningBracket = '(';
        private const char ClosingBracket = ')';
        private const char Separator = ',';

        public static Command Parse(string command)
        {
            var indexOfOpeningBracket = command.IndexOf(OpeningBracket);

            if (indexOfOpeningBracket == -1)
                throw new Exception("Incorrect command format, expected name(param1, param2, ...)");

            var indexOfClosingBracket = command.LastIndexOf(ClosingBracket);

            if (indexOfClosingBracket == -1)
                throw new Exception("Incorrect command format, expected name(param1, param2, ...)");

            var name = command[..indexOfOpeningBracket].Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Incorrect command name, expected non-empty string");

            var parameters = command
                .Substring(indexOfOpeningBracket + 1, indexOfClosingBracket - indexOfOpeningBracket - 1)
                .Split(Separator)
                .Select(p => p.Trim())
                .ToArray();

            return new Command(name, parameters);
        }
    }
}
