namespace Exam891.Cli.CommandLine
{
    internal static class ArgumentsParser
    {
        internal static Arguments Parse(string[] args)
        {
            var result = new Arguments();

            for (var index = 0; index < args.Length; index++)
            {
                if (args[index] == "--bookings")
                    result.BookingsFilePath = args[++index];

                if (args[index] == "--hotels")
                    result.HotelsFilePath = args[++index];
            }

            return result;
        }
    }
}
