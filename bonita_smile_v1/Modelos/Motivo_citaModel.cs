using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    public class Motivo_citaModel
    {
        public string id_motivo { get; set; }
        public string descripcion { get; set; }
        public double costo { get; set; }

        public string costito { get; set; }
        public PacienteModel paciente { get; set; }

        public string fecha { get; set; }
    }
}
