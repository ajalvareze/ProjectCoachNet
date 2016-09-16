using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoach.Models
{
    public class AgregarEquipoCampeonatoVM
    {
        public Equipo Equipo { get; set; }
        public int? CampeonatoID { get; set; }

        public string NombreCampeonato { get; set; }
    }
}
