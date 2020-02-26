using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    public class MembresiaModel
    {
        public string id_membresia { get; set; }
        public string membresia { get; set; }
        public string costo { get; set; }
        public PacienteModel paciente { get; set; }
    }
}