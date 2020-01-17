using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    public class AbonosModel
    {
        public string id_abono { get; set; }
        public string id_paciente { get; set; }
        public string id_motivo { get; set; }
        public string fecha { get; set; }
        public double monto { get; set; }
        public string comentario { get; set; }
    }
}
