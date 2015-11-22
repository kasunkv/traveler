using System;
using Microsoft.AspNet.Mvc;
using Traveler.ViewModels;
using Traveler.Services;
using Traveler;

namespace Traveler.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService) {
            _mailService = mailService;
        }

        public IActionResult Index() {
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
                    ViewBag.Message = "Mail Sent. Thank You.";
                }
            }
            return View();
        }
    }
}
