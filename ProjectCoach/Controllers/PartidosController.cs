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
    public class PartidosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Partidos
        public ActionResult Index()
        {
            var partidos = db.Partidos.Include(p => p.Campeonato).Include(p => p.Equipo1).Include(p => p.Equipo2);
            return View(partidos.ToList());
        }

        // GET: Partidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        // GET: Partidos/Create
        public ActionResult Create()
        {
            ViewBag.CampeonatoID = new SelectList(db.Campeonatos, "CampeonatoID", "Nombre");
            ViewBag.Equipo1ID = new SelectList(db.Equipos, "EquipoID", "Nombre");
            ViewBag.Equipo2ID = new SelectList(db.Equipos, "EquipoID", "Nombre");
            return View();
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartidoID,Jornada,Fecha,Ubicacion,Equipo1ID,Equipo2ID,CampeonatoID,Resultado1,Resultado2")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Partidos.Add(partido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampeonatoID = new SelectList(db.Campeonatos, "CampeonatoID", "Nombre", partido.CampeonatoID);
            ViewBag.Equipo1ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo1ID);
            ViewBag.Equipo2ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo2ID);
            return View(partido);
        }

        // GET: Partidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampeonatoID = new SelectList(db.Campeonatos, "CampeonatoID", "Nombre", partido.CampeonatoID);
            ViewBag.Equipo1ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo1ID);
            ViewBag.Equipo2ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo2ID);
            return View(partido);
        }

        // POST: Partidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartidoID,Jornada,Fecha,Ubicacion,Equipo1ID,Equipo2ID,CampeonatoID,Resultado1,Resultado2")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampeonatoID = new SelectList(db.Campeonatos, "CampeonatoID", "Nombre", partido.CampeonatoID);
            ViewBag.Equipo1ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo1ID);
            ViewBag.Equipo2ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo2ID);
            return View(partido);
        }

        // GET: Partidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partido partido = db.Partidos.Find(id);
            db.Partidos.Remove(partido);
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
