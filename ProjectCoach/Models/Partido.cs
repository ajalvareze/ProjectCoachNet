using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoach.Models
{
    public class Partido
    {
        [Key]
        public int PartidoID { get; set; }

        public int Jornada { get; set; }

        public DateTime Fecha { get; set; }

        public string Ubicacion { get; set; }

        [ForeignKey("Equipo1")]
        public int? Equipo1ID { get; set; }

        [ForeignKey("Equipo2")]
        public int? Equipo2ID { get; set; }

        [ForeignKey("Campeonato")]
        public int? CampeonatoID { get; set; }

        public int Resultado1 { get; set; }
        public int Resultado2 { get; set; }

        public virtual Equipo Equipo1 { get; set; }

        public virtual Equipo Equipo2 { get; set; }

        public virtual Campeonato Campeonato { get; set; }
    }
}
