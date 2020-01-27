using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Modelos
{
    public class Fotos_estudio_carpetaModel
    {
        public string id_foto { get; set; }
        public string id_carpeta { get; set; }
        public string id_paciente { get; set; }
        public string foto { get; set; }
        public string foto_completa { get; set;}
        public BitmapImage imagen { get; set; }

        public string fecha { get; set; }
    }
}
