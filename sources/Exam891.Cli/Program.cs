using Exam891.Cli.CommandLine;
using Exam891.Cli.Commands;
using Exam891.Core.Bookings.Factories;
using Exam891.Core.Hotels.Factories;
using Exam891.Core.Queries.Availability;
using Exam891.Core.Queries.Search;

namespace Exam891.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var mainController = BuildMainController(args);

                while (true)
                {
                    var commandLine = Console.ReadLine();

                    if (string.IsNullOrEmpty(commandLine))
                        return;

                    var command = CommandParser.Parse(commandLine);
                    var result = mainController.Execute(command);

                    Console.WriteLine(result);
                }
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private static MainController BuildMainController(string[] args)
        {
            var parameters = ArgumentsParser.Parse(args);
            var hotelsRepository = FileImportHotelsRepositoryFactory.Create(parameters.HotelsFilePath);
            var bookingsRepository = FileImportBookingsRepositoryFactory.Create(parameters.BookingsFilePath);
            var searchQuery = new SearchQuery(hotelsRepository, bookingsRepository);
            var availabilityQuery = new AvailabilityQuery(searchQuery);
            var mainController = new MainController();
            mainController.Add(new SearchCommand(searchQuery));
            mainController.Add(new AvailabilityCommand(availabilityQuery));
            return mainController;
        }
    }
}
