using DashboardRabitaBank.Models;
using Microsoft.AspNetCore.Mvc;
using RabitaBank.Dashboard.Services;
using System.Collections.Generic;

namespace DashboardRabitaBank.Controllers
{
    public class GoogleReviewController : Controller
    {
        private readonly GoogleReviewService _service;

        public GoogleReviewController(GoogleReviewService service)
        {
            _service = service;
        }

        public IActionResult RabitaBank()
        {
            var reviews = _service.GetRabitaBank();
            return View(reviews);
        }

        public IActionResult RabitaBankCorporate()
        {
            var reviews = _service.GetRabitaBankCorporate();
            return View(reviews);
        }
    }
}
