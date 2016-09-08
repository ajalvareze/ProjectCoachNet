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
    public class CampeonatosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campeonatos
        public ActionResult Index()
        {
            List<Campeonato> campeonatos = new List<Campeonato>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string usuario = HttpContext.User.Identity.Name;
                var user = db.Users.Where(u => u.UserName == usuario).FirstOrDefault();
                campeonatos = user.Campeonatos;
            }
            else
            {
                //equipos = db.Equipos.ToList();
            }
            return View(campeonatos);
        }

        // GET: Campeonatos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonatos.Find(id);
            CampeonatoDetailsVM vm = new CampeonatoDetailsVM();
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            vm.Campeonato = campeonato;
            vm.Jugados = campeonato.Partidos.Count;
            vm.Ganados = 0;
            vm.Empatados = 0;
            vm.Perdidos = 0;
            vm.Puntos = 0;
            foreach (var partido in vm.Campeonato.Partidos)
            {
                if(partido.Resultado1 > partido.Resultado2)
                {
                    vm.Ganados = vm.Ganados + 1;
                    vm.Puntos = vm.Puntos + 3;
                }
                else if (partido.Resultado1 == partido.Resultado2)
                {
                    vm.Empatados = vm.Empatados + 1;
                    vm.Puntos = vm.Puntos + 1;
                }
                else
                {
                    vm.Perdidos = vm.Perdidos + 1;
                }
            }

            vm.Promedio = vm.Jugados != 0 ? (decimal)vm.Puntos / (decimal)vm.Jugados: 0; 

            vm.GolesMarcados = campeonato.Partidos.Select(p => p.Resultado1).Sum();
            vm.GolesSufridos = campeonato.Partidos.Select(p => p.Resultado2).Sum();
            vm.MarcadosMenosSufridos = vm.GolesMarcados - vm.GolesSufridos;
            vm.MarcadosPorJuego = vm.Jugados != 0 ? (decimal)vm.GolesMarcados /(decimal)vm.Jugados : 0;
            vm.SufridosPorJuego = vm.Jugados != 0 ? (decimal)vm.GolesSufridos / (decimal)vm.Jugados : 0;
            
            return View(vm);
        }

        // GET: Campeonatos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campeonatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampeonatoID,Nombre,DFB")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    string usuario = HttpContext.User.Identity.Name;
                    var user = db.Users.Where(u => u.UserName == usuario).FirstOrDefault();
                    user.Campeonatos.Add(campeonato);
                    db.Entry(user).State = EntityState.Modified;
                }
                db.Campeonatos.Add(campeonato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campeonato);
        }

        // GET: Campeonatos/Create
        public ActionResult CreateOwn()
        {
            return View();
        }

        // POST: Campeonatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOwn([Bind(Include = "CampeonatoID,Nombre,DFB")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                db.Campeonatos.Add(campeonato);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = db.Users.Where(u => u.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                    user.Campeonatos.Add(campeonato);
                    db.Entry(user).State = EntityState.Modified;
                }
                db.Campeonatos.Add(campeonato);
                db.SaveChanges();                                
                return RedirectToAction("Index");
            }

            return View(campeonato);
        }

        // GET: Campeonatos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonatos.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            return View(campeonato);
        }

        // POST: Campeonatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampeonatoID,Nombre,DFB")] Campeonato campeonato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campeonato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campeonato);
        }

        // GET: Campeonatos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonatos.Find(id);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            return View(campeonato);
        }

        // POST: Campeonatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campeonato campeonato = db.Campeonatos.Find(id);
            db.Campeonatos.Remove(campeonato);
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
