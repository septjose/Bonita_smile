using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    public class Nota_de_digi_evolucionModel
    {
        public string id_nota { get; set; }
        public string id_paciente { get; set; }
        public string id_motivo { get; set; }
        public string descripcion { get; set; }
        public string fecha { get; set; }

        public string nombre_doctor { get; set; }

        public Carpeta_archivosModel carpeta { get; set; }
        
    }
}
