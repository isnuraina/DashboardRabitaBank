using DashboardRabitaBank.Models;
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
                var posts = collection.Find(_ => true)
                    .SortByDescending(p => p.PostedAt.Timestamp)
                    .ToList();
                return posts;
            }
            catch (Exception ex)
            {
                return new List<LinkedInPost>();
            }
        }

        public List<LinkedInPost> GetAlarmPosts()
        {
            try
            {
                var collection = _database.GetCollection<LinkedInPost>(_settings.RabitaBankCollectionLinkedinName);

                // Bütün postları çək
                var allPosts = collection.Find(_ => true).ToList();

                // is_alarm: false olan kommentləri olan postları tap
                var alarmPosts = allPosts
                    .Where(p => p.Comments != null && p.Comments.Count > 0)
                    .Where(p => p.Comments.Any(c =>
                        c != null &&
                        c.Analysis != null &&
                        c.Analysis.IsAlarm == false))
                    .OrderByDescending(p => p.PostedAt?.Timestamp)
                    .ToList();

                Console.WriteLine($"[DEBUG] Total posts: {allPosts.Count}, Posts with is_alarm=false: {alarmPosts.Count}");

                return alarmPosts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAlarmPosts Error: {ex.Message}");
                return new List<LinkedInPost>();
            }
        }
    }
}