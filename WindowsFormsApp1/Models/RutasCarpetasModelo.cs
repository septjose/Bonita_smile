using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.Models
{
    [Serializable]
    public class RutasCarpetasModelo
    {

        public string ruta_imagenes_carpeta { get; set; }
        public string ruta_temporal_carpeta { get; set; }
        public string ruta_fotografias_carpeta { get; set; }
        public string ruta_subir_servidor_carpeta { get; set; }
    }
}
