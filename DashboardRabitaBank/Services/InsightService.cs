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


        public async Task<List<Post>> GetRabitaJuniorAsync()
        {
            var collection = _database.GetCollection<Post>("rabita.junior");
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<Post>> GetRabitaBankAsync()
        {
            var collection = _database.GetCollection<Post>("rabitabank");
            return await collection.Find(_ => true).ToListAsync();
        }

    }
}
