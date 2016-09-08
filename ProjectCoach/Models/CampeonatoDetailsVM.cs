using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoach.Models
{
    public class CampeonatoDetailsVM
    {
        public Campeonato Campeonato { get; set; }

        public int Jugados { get; set; }
        public int Ganados { get; set; }
        public int Empatados { get; set; }
        public int Perdidos { get; set; }

        public int Puntos { get; set; }
        public decimal Promedio { get; set; }
        public int GolesMarcados { get; set; }
        public decimal MarcadosPorJuego { get; set; }
        public int GolesSufridos { get; set; }
        public decimal SufridosPorJuego { get; set; }
        public int MarcadosMenosSufridos { get; set; }


    }
}
