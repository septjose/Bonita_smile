using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    [Serializable]
    public class RutasCarpetasModelo
    {
       
        public string ruta_imagenes_carpeta { get; set; }
        public string ruta_fotografias_carpeta { get; set; }
        public string ruta_subir_servidor_carpeta { get; set; }
        //rutas que no son imagenes
        public string ruta_respaldo_carpeta { get; set;}
        public string ruta_script_carpeta { get; set; }
        public string ruta_eliminar_carpeta { get; set; }
       
    }
}
