using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class Nota_de_digi_evolucionModel
    {
        public int id_nota { get; set; }
        public int id_paciente { get; set; }
        public int id_motivo { get; set; }
        public string descripcion { get; set; }
        public string fecha { get; set; }
    }
}
