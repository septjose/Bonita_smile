using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class PacienteModel
    {
        public int id_paciente { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string foto { get; set; }
        public int id_antecedentes { get; set; }
        public string email { get; set; }
        public string marketing { get; set; }
    }
}
