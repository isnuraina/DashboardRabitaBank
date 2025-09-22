using DashboardRabitaBank.Services;
using Microsoft.AspNetCore.Mvc;
using RabitaBank.Dashboard.Services;

namespace DashboardRabitaBank.Controllers
{
    public class AppReviewController : Controller
    {
        private readonly AppReviewService _service;

        public AppReviewController(AppReviewService service)
        {
            _service = service;
        }

        public IActionResult RabitaBankBusiness()
        {
            var reviews = _service.GetRabitaBankBusiness();
            return View(reviews);
        }

        public IActionResult RabitaBankMobile()
        {
            var reviews = _service.GetRabitaBankMobile();
            return View(reviews);
        }
    }
}
