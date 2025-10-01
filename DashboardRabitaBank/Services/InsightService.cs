//using DashboardRabitaBank.Models;
//using DashboardRabitaBank.Settings;
//using Microsoft.Extensions.Options;
//using MongoDB.Driver;

//namespace RabitaBank.Dashboard.Services
//{
//    public class InsightService
//    {
//        private readonly IMongoDatabase _database;

//        public InsightService(IOptions<MongoSettings> mongoSettings)
//        {
//            if (mongoSettings?.Value?.ConnectionString == null)
//                throw new ArgumentNullException(nameof(mongoSettings.Value.ConnectionString));

//            var client = new MongoClient(mongoSettings.Value.ConnectionString);

//            if (string.IsNullOrEmpty(mongoSettings.Value.DatabaseName))
//                throw new ArgumentNullException(nameof(mongoSettings.Value.DatabaseName));

//            _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
//        }

//        // =====================
//        // Instagram və digər datalar
//        // =====================

//        public List<Post> GetRabitaInsights()
//        {
//            var collection = _database.GetCollection<Post>("rabita.insights");
//            return collection.Find(_ => true).ToList();
//        }

//        public List<Post> GetRabitaJunior()
//        {
//            var collection = _database.GetCollection<Post>("rabita.junior");
//            return collection.Find(_ => true).ToList();
//        }

//        public List<Post> GetRabitaBank()
//        {
//            var collection = _database.GetCollection<Post>("rabitabank");
//            return collection.Find(_ => true).ToList();
//        }

//        // =====================
//        // GOOGLE REVIEW METHODS
//        // =====================

//        /// <summary>
//        /// RabitaBank Google Reviews
//        /// </summary>
//        public List<GoogleReview> GetGoogleRabitaBank()
//        {
//            var collection = _database.GetCollection<GoogleReview>("google_com.rabitabank");
//            return collection.Find(_ => true).ToList();
//        }

//        /// <summary>
//        /// RabitaBank Corporate Google Reviews
//        /// </summary>
//        public List<GoogleReview> GetGoogleRabitaBankCorporate()
//        {
//            var collection = _database.GetCollection<GoogleReview>("google_com.rabitabank.corporate");
//            return collection.Find(_ => true).ToList();
//        }

//        /// <summary>
//        /// RabitaJunior Google Reviews
//        /// </summary>
//        public List<GoogleReview> GetGoogleRabitaJunior()
//        {
//            var collection = _database.GetCollection<GoogleReview>("google_com.rabitajunior");
//            return collection.Find(_ => true).ToList();
//        }
//    }
//}

using DashboardRabitaBank.Models;
using DashboardRabitaBank.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks; // Asinxron metodlar üçün əlavə olundu

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

            if (string.IsNullOrEmpty(mongoSettings.Value.DatabaseName))
                throw new ArgumentNullException(nameof(mongoSettings.Value.DatabaseName));

            _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        }

        // =====================
        // Köməkçi Metod
        // =====================

        /// <summary>
        /// MongoDb-dən "Insight" (Post) obyektlərini çəkən ümumi metod.
        /// </summary>
        private IMongoCollection<Post> GetCollection(string collectionName)
        {
            return _database.GetCollection<Post>(collectionName);
        }

        /// <summary>
        /// Verilmiş kolleksiyadan yalnız daxilində alarm şərhi olan postları çəkir.
        /// </summary>
        private async Task<List<Post>> GetAlarmPostsFromCollection(string collectionName)
        {
            var collection = GetCollection(collectionName);

            // Postun Comments massivində Analysis.IsAlarm == true olan ən azı bir element axtarılır.
            var filter = Builders<Post>.Filter.ElemMatch(
                p => p.Comments,
                c => c.Analysis.IsAlarm == true
            );

            // Yeni postları əvvələ gətirmək üçün tarixinə görə sıralayır (fərz olunur ki, PostInfo.Timestamp mövcuddur)
            var sort = Builders<Post>.Sort.Descending(p => p.PostInfo.Timestamp);

            return await collection.Find(filter).Sort(sort).ToListAsync();
        }


        // =====================
        // INSTAGRAM/POST DATALARININ ÇƏKİLMƏSİ
        // =====================

        // Bu metodlar sinxron qalır (fərz olunur ki, köhnə kod belədir). 
        // Lakin alarm metodları asinxron olacaq.
        public List<Post> GetRabitaInsights() => GetCollection("rabita.insights").Find(_ => true).ToList();
        public List<Post> GetRabitaJunior() => GetCollection("rabita.junior").Find(_ => true).ToList();
        public List<Post> GetRabitaBank() => GetCollection("rabitabank").Find(_ => true).ToList();


        // =====================
        // YENİ ALARM METODLARI
        // =====================

        /// <summary>
        /// Rabita Insights kolleksiyasından alarmı olan postları çəkir.
        /// </summary>
        public Task<List<Post>> GetRabitaInsightsAlarmPosts() =>
            GetAlarmPostsFromCollection("rabita.insights");

        /// <summary>
        /// Rabita Junior kolleksiyasından alarmı olan postları çəkir.
        /// </summary>
        public Task<List<Post>> GetRabitaJuniorAlarmPosts() =>
            GetAlarmPostsFromCollection("rabita.junior");

        /// <summary>
        /// Rabita Bank kolleksiyasından alarmı olan postları çəkir.
        /// </summary>
        public Task<List<Post>> GetRabitaBankAlarmPosts() =>
            GetAlarmPostsFromCollection("rabitabank");


        // =====================
        // GOOGLE REVIEW METHODS (Dəyişməyib)
        // =====================

        // Qeyd: GoogleReview modelində 'Analysis' və 'IsAlarm' sahələri fərqli ola bilər.
        // Onlar üçün ayrıca Alarm metodları yazılmalıdır.

        public List<GoogleReview> GetGoogleRabitaBank()
        {
            var collection = _database.GetCollection<GoogleReview>("google_com.rabitabank");
            return collection.Find(_ => true).ToList();
        }

        public List<GoogleReview> GetGoogleRabitaBankCorporate()
        {
            var collection = _database.GetCollection<GoogleReview>("google_com.rabitabank.corporate");
            return collection.Find(_ => true).ToList();
        }

        public List<GoogleReview> GetGoogleRabitaJunior()
        {
            var collection = _database.GetCollection<GoogleReview>("google_com.rabitajunior");
            return collection.Find(_ => true).ToList();
        }
    }
}