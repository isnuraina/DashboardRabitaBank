using DashboardRabitaBank.Models;
using DashboardRabitaBank.Services;
using DashboardRabitaBank.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DashboardRabitaBank.Services
{
    public class LinkedinService
    {
        private readonly IMongoDatabase _database;
        private readonly LinkedinMongoSettings _settings;
        public LinkedinService(IOptions<LinkedinMongoSettings> settings)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }
        public List<LinkedInPost> GetRabitaLinkedin()
        {
            try
            {
                var collection = _database.GetCollection<LinkedInPost>(_settings.RabitaBankCollectionLinkedinName);
                var posts = collection.Find(_ => true).ToList();

                return posts;
            }
            catch (Exception ex)
            {
                return new List<LinkedInPost>();
            }
        }

    }
}