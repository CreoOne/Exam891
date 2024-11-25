using Exam891.Core.Bookings.Models;
using Exam891.Core.Bookings.Repositories;
using Exam891.Core.Serialization;

namespace Exam891.Core.Bookings.Factories
{
    public static class FileImportBookingsRepositoryFactory
    {
        public static IBookingsRepository Create(string bookingsFilePath)
        {
            var deserliazer = new DefaultSerializer();
            var fileContent = File.ReadAllText(bookingsFilePath);
            var bookings = deserliazer.Deserialize<Booking[]>(fileContent);
            var bookingsRepository = new NaiveBookingsRepository();
            bookingsRepository.Add(bookings ?? []);
            return bookingsRepository;
        }
    }
}
