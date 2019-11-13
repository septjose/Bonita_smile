using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class Motivo_citaModel
    {
        public int id_motivo { get; set; }
        public string descripcion { get; set; }
        public double costo { get; set; }
        public int id_paciente { get; set; }
    }
}
