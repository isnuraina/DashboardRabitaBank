using DashboardRabitaBank.Controllers;
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
            var reviews = _service.GetRabitaBankMobile();
            return View(reviews);
        }
    }
}


