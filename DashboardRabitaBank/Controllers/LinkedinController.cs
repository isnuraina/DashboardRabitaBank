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
        public IActionResult RabitaLinkedin()
        {
            try
            {
                var reviews = _service.GetRabitaLinkedin();


                if (reviews == null)
                {
                    reviews = new List<LinkedInPost>();
                }

                return View(reviews);
            }
            catch (Exception ex)
            {
                return View(new List<LinkedInPost>());
            }
        }

    }
}

