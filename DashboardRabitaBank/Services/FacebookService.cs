using DashboardRabitaBank.Models;
using DashboardRabitaBank.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DashboardRabitaBank.Services
{
    public class FacebookService
    {
        private readonly IMongoDatabase _database;
        private readonly FacebookMongoSettings _settings;

        public FacebookService(IOptions<FacebookMongoSettings> settings)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));

            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }

        public List<FacebookPost> GetRabitaBankMobile()
        {
            try
            {
                var collection = _database.GetCollection<FacebookPost>(_settings.RabitaBankCollectionFacebookName);
                var posts = collection.Find(_ => true).ToList();

                // Debug üçün
                Console.WriteLine($"MongoDB-dən gələn post sayı: {posts.Count}");

                return posts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB Error: {ex.Message}");
                return new List<FacebookPost>();
            }
        }
    }
}
