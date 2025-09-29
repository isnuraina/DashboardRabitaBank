using DashboardRabitaBank.Models;
using DashboardRabitaBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashboardRabitaBank.Controllers
{
    public class LinkedinController : Controller
    {
        private readonly LinkedinService _service;

        public LinkedinController(LinkedinService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RabitaLinkedin()
        {
            try
            {
                var posts = _service.GetRabitaLinkedin();
                if (posts == null)
                {
                    posts = new List<LinkedInPost>();
                }

                ViewBag.TotalPosts = posts.Count;
                ViewBag.TotalReactions = posts.Sum(p => p.Stats?.TotalReactions ?? 0);
                ViewBag.TotalLikes = posts.Sum(p => p.Stats?.Like ?? 0);
                ViewBag.TotalLoves = posts.Sum(p => p.Stats?.Love ?? 0);
                ViewBag.TotalCelebrates = posts.Sum(p => p.Stats?.Celebrate ?? 0);
                ViewBag.TotalReposts = posts.Sum(p => p.Stats?.Reposts ?? 0);

                return View(posts);
            }
            catch (Exception ex)
            {
                return View(new List<LinkedInPost>());
            }
        }

        public IActionResult AlarmPosts()
        {
            try
            {
                // Debug üçün bütün postları yoxla
                var allPosts = _service.GetRabitaLinkedin();

                // İlk kommentli postu tap
                var firstPostWithComments = allPosts.FirstOrDefault(p => p.Comments != null && p.Comments.Any());

                string firstPostDebug = "Kommentli post tapılmadı";
                if (firstPostWithComments != null)
                {
                    var firstComment = firstPostWithComments.Comments.FirstOrDefault();
                    if (firstComment != null)
                    {
                        firstPostDebug = $"İlk post ID: {firstPostWithComments.Id}, " +
                            $"Komment sayı: {firstPostWithComments.Comments.Count}, " +
                            $"İlk komment Analysis null?: {(firstComment.Analysis == null)}, " +
                            $"IsAlarm: {firstComment.Analysis?.IsAlarm}";
                    }
                }

                var alarmPosts = _service.GetAlarmPosts();
                if (alarmPosts == null)
                {
                    alarmPosts = new List<LinkedInPost>();
                }

                var totalPostsWithComments = allPosts.Count(p => p.Comments != null && p.Comments.Any());
                var totalComments = allPosts.SelectMany(p => p.Comments ?? new List<LinkedInComment>()).Count();

                // Analysis olan kommentləri yoxla
                var commentsWithAnalysis = allPosts
                    .SelectMany(p => p.Comments ?? new List<LinkedInComment>())
                    .Where(c => c.Analysis != null)
                    .ToList();

                var totalAlarmTrue = commentsWithAnalysis.Count(c => c.Analysis.IsAlarm == true);
                var totalAlarmFalse = commentsWithAnalysis.Count(c => c.Analysis.IsAlarm == false);

                ViewBag.DebugInfo = $"Ümumi postlar: {allPosts.Count}, " +
                    $"Kommentli postlar: {totalPostsWithComments}, " +
                    $"Ümumi kommentlər: {totalComments}, " +
                    $"Analysis olan: {commentsWithAnalysis.Count}, " +
                    $"is_alarm=true: {totalAlarmTrue}, is_alarm=false: {totalAlarmFalse} | " +
                    $"Tapılan alarm postları: {alarmPosts.Count} | {firstPostDebug}";

                // Alarm statistikası
                ViewBag.TotalAlarms = alarmPosts.Count;
                ViewBag.TotalAlarmComments = alarmPosts
                    .SelectMany(p => p.Comments ?? Enumerable.Empty<LinkedInComment>())
                    .Count(c => c.Analysis?.IsAlarm == false);

                // Sentiment üzrə qruplaşdırma
                var sentimentGroups = alarmPosts
                    .SelectMany(p => p.Comments ?? Enumerable.Empty<LinkedInComment>())
                    .Where(c => c.Analysis?.IsAlarm == false)
                    .GroupBy(c => c.Analysis.Sentiment)
                    .ToDictionary(g => g.Key ?? "unknown", g => g.Count());

                ViewBag.NegativeSentiments = sentimentGroups.GetValueOrDefault("negative", 0);
                ViewBag.NeutralSentiments = sentimentGroups.GetValueOrDefault("neutral", 0);
                ViewBag.PositiveSentiments = sentimentGroups.GetValueOrDefault("positive", 0);

                return View(alarmPosts);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Alarm postları yüklənərkən xəta baş verdi: " + ex.Message;
                return View(new List<LinkedInPost>());
            }
        }
    }
}