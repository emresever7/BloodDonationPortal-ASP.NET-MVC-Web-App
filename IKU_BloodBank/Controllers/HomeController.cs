using IKU_BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IKU_BloodBank.Controllers
{
    public class HomeController : Controller
    {
        private BloodTeamDbContext db = new BloodTeamDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult WhoCanGiveBlood()
        {
            return View();
        }

        public ActionResult WhyGiveBlood()
        {
            return View();
        }

        public ActionResult WhereToDonate()
        {
            var bloodTeams = db.BloodTeams.ToList();
            return View(bloodTeams);
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}