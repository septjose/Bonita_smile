using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class Carpeta_archivosModel
    {
        public int id_carpeta { get; set; }
        public string nombre_carpeta { get; set; }
        public int id_paciente { get; set; }
    }
}
