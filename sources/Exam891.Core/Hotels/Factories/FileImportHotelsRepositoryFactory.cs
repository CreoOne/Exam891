using Exam891.Core.Hotels.Models;
using Exam891.Core.Hotels.Repositories;
using Exam891.Core.Serialization;

namespace Exam891.Core.Hotels.Factories
{
    public static class FileImportHotelsRepositoryFactory
    {
        public static IHotelsRepository Create(string hotelsFilePath)
        {
            var deserliazer = new DefaultSerializer();
            var fileContent = File.ReadAllText(hotelsFilePath);
            var hotels = deserliazer.Deserialize<Hotel[]>(fileContent);
            var hotelsRepository = new NaiveHotelsRepository();
            hotelsRepository.Add(hotels ?? []);
            return hotelsRepository;
        }
    }
}
