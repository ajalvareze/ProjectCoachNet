using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoach.Models
{
    public class Equipo
    {
        public Equipo()
        {
            Partidos = new List<Partido>();
            Campeonatos = new List<Campeonato>();
        }
        [Key]
        public int EquipoID { get; set; }
        public string Nombre { get; set; }

        public virtual List<Partido> Partidos { get; set; }
        public virtual List<Campeonato> Campeonatos { get; set; }
    }
}
