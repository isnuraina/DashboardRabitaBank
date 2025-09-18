using DashboardRabitaBank.Models;
using Microsoft.AspNetCore.Mvc;
using RabitaBank.Dashboard.Services;

namespace DashboardRabitaBank.Controllers
{
    public class DashboardController : Controller
    {
        private readonly InsightService _insightService;

        public DashboardController(InsightService insightService)
        {
            _insightService = insightService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Instagram()
        {
            return View();
        }
        public IActionResult Google()
        {
            return View();
        }
        //public IActionResult Insights()
        //{
        //    var posts = _insightService.GetRabitaInsights();

        //    return View(posts);
        //}

        //public IActionResult Insights()
        //{
        //    var posts = _insightService.GetRabitaInsights();

        //    // Şərhlərin sentiment statistikası
        //    var sentiments = posts
        //        .SelectMany(p => p.Comments ?? new List<Comment>())   // bütün postların şərhlərini götürür
        //        .GroupBy(c => c.Analysis?.Sentiment ?? "other")       // sentiment-ə görə qruplaşdırır
        //        .Select(g => new { Sentiment = g.Key, Count = g.Count() })
        //        .ToList();

        //    ViewBag.SentimentStats = sentiments;

        //    return View(posts);
        //}

        //public IActionResult Insights()
        //{
        //    var posts = _insightService.GetRabitaInsights() ?? new List<Post>();

        //    // Əgər heç post yoxdursa, boş siyahı qaytarırıq
        //    var allComments = posts
        //        .Where(p => p.Comments != null)
        //        .SelectMany(p => p.Comments)
        //        .ToList();

        //    var sentiments = allComments
        //        .GroupBy(c => c.Analysis?.Sentiment ?? "other")
        //        .Select(g => new { Sentiment = g.Key, Count = g.Count() })
        //        .ToList();

        //    ViewBag.SentimentStats = sentiments;

        //    return View(posts);
        //}

        public IActionResult Insights()
        {
            var posts = _insightService.GetRabitaInsights() ?? new List<Post>();

            var allComments = posts
                .Where(p => p.Comments != null)
                .SelectMany(p => p.Comments)
                .Where(c => c.Analysis != null)
                .ToList();

            var sentiments = allComments
                .GroupBy(c => c.Analysis.Sentiment?.ToLower() ?? "other")
                .Select(g => new { Sentiment = g.Key, Count = g.Count() })
                .ToList();

           

            ViewBag.SentimentStats = sentiments;

            return View(posts);
        }
        public IActionResult Junior()
        {
            // MongoDB-dən bütün postları çəkirik
            var posts = _insightService.GetRabitaJunior();

            // Bütün şərhləri götürürük, Analysis olanları filtrləyirik
            var allComments = posts
                .Where(p => p.Comments != null)   // Comments null deyil
                .SelectMany(p => p.Comments)      // Bütün şərhləri bir listə toplayırıq
                .Where(c => c.Analysis != null)   // Analysis olanları seçirik
                .ToList();

            // Sentiment üzrə qruplaşdırırıq və sayını alırıq
            var sentiments = allComments
                .GroupBy(c => c.Analysis.Sentiment?.ToLower() ?? "other")
                .Select(g => new
                {
                    Sentiment = g.Key,
                    Count = g.Count()
                })
                .ToList();

            // ViewBag vasitəsilə view-a göndəririk
            ViewBag.SentimentStats = sentiments;

            // Postları view-a göndəririk
            return View(posts);
        }
        public IActionResult RabitaBank()
        {
            // MongoDB-dən bütün postları götürürük
            var posts = _insightService.GetRabitaBank() ?? new List<Post>();

            // Bütün şərhləri toplamaq və sentiment statistikasını çıxarmaq
            var allComments = posts
                .Where(p => p.Comments != null)
                .SelectMany(p => p.Comments)
                .Where(c => c.Analysis != null)
                .ToList();

            var sentiments = allComments
                .GroupBy(c => c.Analysis.Sentiment?.ToLower() ?? "other")
                .Select(g => new { Sentiment = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.SentimentStats = sentiments;

            return View(posts);
        }
       
    }
}
