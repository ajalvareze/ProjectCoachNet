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
                if (partido.Resultado1 > partido.Resultado2)
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

            vm.Promedio = vm.Jugados != 0 ? (decimal)vm.Puntos / (decimal)vm.Jugados : 0;

            vm.GolesMarcados = campeonato.Partidos.Select(p => p.Resultado1).Sum();
            vm.GolesSufridos = campeonato.Partidos.Select(p => p.Resultado2).Sum();
            vm.MarcadosMenosSufridos = vm.GolesMarcados - vm.GolesSufridos;
            vm.MarcadosPorJuego = vm.Jugados != 0 ? (decimal)vm.GolesMarcados / (decimal)vm.Jugados : 0;
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
        public ActionResult CreateOwn(int? EquipoID)
        {
            if (EquipoID != null)
            {
                var equipo = db.Equipos.Where(e => e.EquipoID == EquipoID).FirstOrDefault();
            }
            return View();
        }

        // POST: Campeonatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOwn([Bind(Include = "CampeonatoID,Nombre,DFB")] Campeonato campeonato, int? EquipoID)
        {
            Equipo equipo;

            if (ModelState.IsValid && EquipoID != null)
            {

                equipo = db.Equipos.Where(e => e.EquipoID == EquipoID).FirstOrDefault();

                campeonato.Equipos.Add(equipo);
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

        // GET: Equipos/Create
        public ActionResult AgregarEquipo(int? CampeonatoID)
        {
            AgregarEquipoCampeonatoVM vm = new AgregarEquipoCampeonatoVM();
            if (CampeonatoID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campeonato campeonato = db.Campeonatos.Find(CampeonatoID);
            if (campeonato == null)
            {
                return HttpNotFound();
            }
            vm.CampeonatoID = CampeonatoID;
            vm.Equipo = new Equipo();
            return View(vm);
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarEquipo(AgregarEquipoCampeonatoVM vm)
        {

            if (ModelState.IsValid)
            {
                if (vm.CampeonatoID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Campeonato campeonato = db.Campeonatos.Find(vm.CampeonatoID);
                vm.Equipo.Campeonatos.Add(campeonato);                
                db.Equipos.Add(vm.Equipo);
                db.SaveChanges();
                return RedirectToAction("Details", "Campeonatos", campeonato.CampeonatoID);
            }

            return View(vm);
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


        // GET: Partidos/Create
        public ActionResult AgregarPartidoACampeonato(int campeonatoID)
        {
            Partido Partido = new Partido();
            var campeonato = db.Campeonatos.Where(c => c.CampeonatoID == campeonatoID).FirstOrDefault();

            List<Equipo> equiposRivales = new List<Equipo>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string usuario = HttpContext.User.Identity.Name;
                var user = db.Users.Where(u => u.UserName == usuario).FirstOrDefault();
                var equiposuser = user.Equipos;
                var equipo1 = campeonato.Equipos.Intersect(equiposuser).FirstOrDefault();
                Partido.Equipo1 = equipo1;
                Partido.Equipo1ID = equipo1.EquipoID;
                equiposRivales = campeonato.Equipos.ToList();
                equiposRivales.Remove(equipo1);
            }

            Partido.CampeonatoID = campeonato.CampeonatoID;
            Partido.Campeonato = campeonato;
            ViewBag.Equipo2ID = new SelectList(equiposRivales, "EquipoID", "Nombre");
            return View(Partido);
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarPartidoACampeonato([Bind(Include = "PartidoID,Jornada,Fecha,Ubicacion,Equipo1ID,Equipo2ID,CampeonatoID,Resultado1,Resultado2")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Partidos.Add(partido);
                db.SaveChanges();
                return RedirectToAction("Details", "Campeonatos", partido.CampeonatoID);
            }

            //ViewBag.CampeonatoID = new SelectList(db.Campeonatos, "CampeonatoID", "Nombre", partido.CampeonatoID);
            ViewBag.Equipo1ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo1ID);
            ViewBag.Equipo2ID = new SelectList(db.Equipos, "EquipoID", "Nombre", partido.Equipo2ID);
            return View(partido);
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
