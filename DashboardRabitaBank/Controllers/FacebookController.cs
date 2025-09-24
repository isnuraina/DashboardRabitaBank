using DashboardRabitaBank.Controllers;
using DashboardRabitaBank.Models;
using DashboardRabitaBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashboardRabitaBank.Controllers
{
    public class FacebookController : Controller
    {
        private readonly FacebookService _service;
        public FacebookController(FacebookService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public IActionResult RabitaMobile()
        {
            try
            {
                var reviews = _service.GetRabitaBankMobile();

                Console.WriteLine($"Posts count: {reviews?.Count ?? 0}");

                if (reviews == null)
                {
                    reviews = new List<FacebookPost>();
                }

                return View(reviews);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View(new List<FacebookPost>());
            }
        }
    }
}


