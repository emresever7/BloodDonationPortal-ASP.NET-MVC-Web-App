using IKU_BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace IKU_BloodBank.Controllers
{
    public class BloodTeamsController : Controller
    {
        private BloodTeamDbContext db = new BloodTeamDbContext();
        private BloodTeamService BloodTeamService = new BloodTeamService();

        //public ActionResult ImportXml()
        //{
        //    // 1. App_Data klasörünün sunucudaki fiziksel yolunu bul
        //    string filePath = Server.MapPath("~/App_Data/BloodTeam.xml");

        //    // 2. Dosya var mı kontrolü
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return Content("HATA: XML dosyası bulunamadı! Yol: " + filePath);
        //    }

        //    // 3. Dosyayı metin olarak oku
        //    string xmlData = System.IO.File.ReadAllText(filePath);

        //    // 4. Daha önceki oluşturduğumuz ayrıştırma metoduna gönder
        //    BloodTeamService.ImportBloodTeams(xmlData);

        //    return Content("İşlem tamamlandı, lütfen veritabanını kontrol edin.");
        //}

        [HttpGet]
        public JsonResult GetTeamsForMap()
        {
            // Tüm listeyi veritabanından çekip sadece harita için gereken alanları seçiyoruz (Performans için)
            var teams = db.BloodTeams.Select(t => new
            {
                t.TeamName,
                t.Neighbourhood,
                t.PhoneNumber,
                t.Latitude,
                t.Longitude
            }).ToList();

            // Veriyi JSON formatına çevirip JavaScript'e gönderiyoruz
            return Json(teams, JsonRequestBehavior.AllowGet);
        }


        // GET: BloodTeams
        public ActionResult Index()
        {

            return View(db.BloodTeams.ToList());
        }

        // GET: BloodTeams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodTeam bloodTeam = db.BloodTeams.Find(id);
            if (bloodTeam == null)
            {
                return HttpNotFound();
            }
            return View(bloodTeam);
        }

        


        // GET: BloodTeams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeamName,Address,CityId,CityIdArea,Town,Latitude,Longitude,Neighbourhood,Platelets,PhoneNumber")] BloodTeam bloodTeam)
        {
            if (ModelState.IsValid)
            {
                db.BloodTeams.Add(bloodTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bloodTeam);
        }

        // GET: BloodTeams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodTeam bloodTeam = db.BloodTeams.Find(id);
            if (bloodTeam == null)
            {
                return HttpNotFound();
            }
            return View(bloodTeam);
        }

        // POST: BloodTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeamName,Address,CityId,CityIdArea,Town,Latitude,Longitude,Neighbourhood,Platelets,PhoneNumber")] BloodTeam bloodTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloodTeam);
        }

        // GET: BloodTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodTeam bloodTeam = db.BloodTeams.Find(id);
            if (bloodTeam == null)
            {
                return HttpNotFound();
            }
            return View(bloodTeam);
        }

        // POST: BloodTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodTeam bloodTeam = db.BloodTeams.Find(id);
            db.BloodTeams.Remove(bloodTeam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
