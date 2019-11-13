using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class AbonosModel
    {
        public int id_abono { get; set; }
        public int id_paciente { get; set; }
        public int id_motivo { get; set; }
        public string fecha { get; set; }
        public double monto { get; set; }
    }
}
