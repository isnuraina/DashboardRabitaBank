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

        // =====================
        // Instagram / Post Data
        // =====================

        public List<Post> GetRabitaInsights()
        {
            var collection = _database.GetCollection<Post>("rabita.insights");
            return collection.Find(_ => true).ToList();
        }

        public List<Post> GetRabitaJunior()
        {
            var collection = _database.GetCollection<Post>("rabita.junior");
            return collection.Find(_ => true).ToList();
        }

        public List<Post> GetRabitaBank()
        {
            var collection = _database.GetCollection<Post>("rabitabank");
            return collection.Find(_ => true).ToList();
        }

        // =====================
        // Google Reviews
        // =====================

        // RabitaBank Google Reviews (general)
        public List<GoogleReview> GetGoogleRabitaBank()
        {
            var collection = _database.GetCollection<GoogleReview>("google_com.rabitabank");
            return collection.Find(_ => true).ToList();
        }

        // RabitaBank Corporate Google Reviews
        public List<GoogleReview> GetGoogleRabitaBankCorporate()
        {
            var collection = _database.GetCollection<GoogleReview>("google_com.rabitabank.corporate");
            return collection.Find(_ => true).ToList();
        }

        // RabitaJunior Google Reviews
        public List<GoogleReview> GetGoogleRabitaJunior()
        {
            var collection = _database.GetCollection<GoogleReview>("google_com.rabitajunior");
            return collection.Find(_ => true).ToList();
        }
    }
}
