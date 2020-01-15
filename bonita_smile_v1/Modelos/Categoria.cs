using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace bonita_smile_v1.Modelos
{
    public class Categoria
    {
        public List<string> abonos { get; set; }
        public List<string> carpeta_archivos { get; set; }
        public List<string> clinica { get; set; }
        public List<string> fotos_estudio_carpeta { get; set; }
        public List<string> marketing { get; set; }
        public List<string> motivo_cita {   get; set; }
        public List<string> nota_de_digi_evolucion { get; set; }
        public List<string> paciente { get; set; }
        public List<string> permisos { get; set; }
        public List<string> rol { get; set; }
        public List<string> usuario { get; set; }
    }
}
