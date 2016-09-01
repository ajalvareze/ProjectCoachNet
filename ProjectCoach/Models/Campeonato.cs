using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoach.Models
{
    public class Campeonato
    {
        public Campeonato()
        {
            Partidos = new List<Partido>();
            Equipos = new List<Equipo>();
        }

        [Key]        
        public int CampeonatoID { get; set; }
        public string Nombre { get; set; }

        public string DFB { get; set; }

        public virtual List<Partido> Partidos { get; set; }

        public virtual List<Equipo> Equipos { get; set; }
    }
}
