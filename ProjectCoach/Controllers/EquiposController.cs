using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCoach.Models;

namespace ProjectCoach.Controllers
{
    public class EquiposController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipos
        public ActionResult Index()
        {
            List<Equipo> equipos = new List<Equipo>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string usuario = HttpContext.User.Identity.Name;
                var user = db.Users.Where(u => u.UserName == usuario).FirstOrDefault();
                equipos = user.Equipos;
            }
            else
            {
                //equipos = db.Equipos.ToList();
            }
            
            return View(equipos);
        }

        // GET: Equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoID,Nombre")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipo);
        }

        // GET: Equipos/Create
        public ActionResult CreateOwn()
        {
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOwn([Bind(Include = "EquipoID,Nombre")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipo);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = db.Users.Where(u => u.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                    user.Equipos.Add(equipo);
                    db.Entry(user).State = EntityState.Modified;
                }                

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoID,Nombre")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipo equipo = db.Equipos.Find(id);
            db.Equipos.Remove(equipo);
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
