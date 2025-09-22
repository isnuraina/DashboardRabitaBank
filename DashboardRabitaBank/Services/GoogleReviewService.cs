using DashboardRabitaBank.Models;
using DashboardRabitaBank.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;

namespace RabitaBank.Dashboard.Services
{
    public class GoogleReviewService
    {
        private readonly IMongoDatabase _appDb;

        public GoogleReviewService(IOptions<GoogleMongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _appDb = client.GetDatabase(settings.Value.DatabaseName);
        }

        public List<GoogleReview> GetRabitaBank()
        {
            var collection = _appDb.GetCollection<GoogleReview>("google_com.rabitabank");
            return collection.Find(_ => true).ToList();
        }

        public List<GoogleReview> GetRabitaBankCorporate()
        {
            var collection = _appDb.GetCollection<GoogleReview>("google_com.rabitabank.corporate");
            return collection.Find(_ => true).ToList();
        }
    }
}
