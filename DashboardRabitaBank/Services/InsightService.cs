using DashboardRabitaBank.Models;
using DashboardRabitaBank.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RabitaBank.Dashboard.Services
{
    public class InsightService
    {
        private readonly IMongoDatabase _database;

        public InsightService(IOptions<MongoSettings> mongoSettings)
        {
            if (mongoSettings?.Value?.ConnectionString == null)
                throw new ArgumentNullException(nameof(mongoSettings.Value.ConnectionString));

            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        }

        public List<Post> GetRabitaInsights()
        {
            var collection = _database.GetCollection<Post>("rabita.insights");
            return collection.Find(_ => true).ToList();
        }


        public List<Post> GetRabitaJunior()
        {
            var collection = _database.GetCollection<Post>("rabita.junior");
            return  collection.Find(_ => true).ToList();
        }

        public List<Post> GetRabitaBank()
        {
            var collection = _database.GetCollection<Post>("rabitabank");
            return  collection.Find(_ => true).ToList();
        }

    }
}
