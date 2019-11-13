using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class Fotos_estudio_carpetaModel
    {
        public int id_foto { get; set; }
        public int id_carpeta { get; set; }
        public int id_paciente { get; set; }
        public string foto { get; set; }
    }
}
