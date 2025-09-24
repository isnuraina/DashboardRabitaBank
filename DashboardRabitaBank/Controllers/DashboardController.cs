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
        public IActionResult App()
        {
            return View();
        }
        public IActionResult Facebook()
        {
            return View();
        }

        public IActionResult Linkedin()
        {
            return View();
        }
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
            var posts = _insightService.GetRabitaJunior() ?? new List<Post>();

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

        public IActionResult RabitaBank()
        {
            var posts = _insightService.GetRabitaBank() ?? new List<Post>();

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

        // =====================
        // GOOGLE REVIEWS
        // =====================

        // RabitaBank Google Reviews (General)
        public IActionResult RabitaBankGoogle()
        {
            var reviews = _insightService.GetGoogleRabitaBank() ?? new List<GoogleReview>();
            return View(reviews);
        }

        // RabitaBank Corporate Google Reviews
        public IActionResult RabitaBankCorporateGoogle()
        {
            var reviews = _insightService.GetGoogleRabitaBankCorporate() ?? new List<GoogleReview>();
            return View(reviews);
        }

        // RabitaJunior Google Reviews
        public IActionResult RabitaJuniorGoogle()
        {
            var reviews = _insightService.GetGoogleRabitaJunior();
            return View(reviews);
        }

        // Combined Google Reviews for Tabs
        public IActionResult GoogleReviews()
        {
            var rabitaBankReviews = _insightService.GetGoogleRabitaBank() ?? new List<GoogleReview>();
            var rabitaJuniorReviews = _insightService.GetGoogleRabitaJunior() ?? new List<GoogleReview>();

            var model = new Tuple<List<GoogleReview>, List<GoogleReview>>(rabitaBankReviews, rabitaJuniorReviews);
            return View(model);
        }
    }
}
