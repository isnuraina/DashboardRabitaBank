using DashboardRabitaBank.Controllers;
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
        //public IActionResult RabitaLinkedin()
        //{
        //    try
        //    {
        //        var reviews = _service.GetRabitaLinkedin();


        //        if (reviews == null)
        //        {
        //            reviews = new List<LinkedInPost>();
        //        }

        //        return View(reviews);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(new List<LinkedInPost>());
        //    }
        //}
        public IActionResult RabitaLinkedin()
        {
            try
            {
                var posts = _service.GetRabitaLinkedin();
                if (posts == null)
                {
                    posts = new List<LinkedInPost>();
                }

                // Chart üçün statistika hazırla
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

    }
}

