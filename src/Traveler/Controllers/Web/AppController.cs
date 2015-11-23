using System;
using Microsoft.AspNet.Mvc;
using Traveler.ViewModels;
using Traveler.Services;
using Traveler;
using Traveler.Models;
using System.Linq;

namespace Traveler.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly TravelerContext _context;

        public AppController(IMailService mailService, TravelerContext context) {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index() {
            var trips = _context.Trips.OrderBy(t => t.Name).ToList();
            return View();
        }

        public IActionResult About() {
            return View();
        }

        public IActionResult Contact() {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model) {

            if (ModelState.IsValid) {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (_mailService.SendMail(email, email, $"Contact Request From {model.Name} ({model.Email})", model.Message)) {
                    ModelState.Clear();
                    ViewBag.Message = Startup.Configuration["AppSettings:ThankYouMessage"];
                }
            }
            return View();
        }
    }
}
