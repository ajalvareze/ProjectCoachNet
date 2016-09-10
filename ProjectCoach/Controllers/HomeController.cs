using ProjectCoach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCoach.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                vm.Equipos = user.Equipos;
                vm.Campeonatos = user.Campeonatos;

                return View(vm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}