using DashboardRabitaBank.Models;
using DashboardRabitaBank.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DashboardRabitaBank.Services
{
    public class AppReviewService
    {
        private readonly IMongoDatabase _appDb;

        public AppReviewService(IOptions<AppMongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _appDb = client.GetDatabase(settings.Value.DatabaseName);
        }

        public List<AppReview> GetRabitaBankBusiness()
        {
            var collection = _appDb.GetCollection<AppReview>("apple_rabita_business");
            return collection.Find(_ => true).ToList();
        }

        public List<AppReview> GetRabitaBankMobile()
        {
            var collection = _appDb.GetCollection<AppReview>("apple_rabita_mobile");
            return collection.Find(_ => true).ToList();
        }
    }
}
