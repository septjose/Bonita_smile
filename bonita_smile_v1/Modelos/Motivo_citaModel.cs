using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Modelos
{
    public class Motivo_citaModel
    {
        public string id_motivo { get; set; }
        public string descripcion { get; set; }
        public double costo { get; set; }

        public string costito { get; set; }
        public string  id_paciente { get; set; }
        public string id_clinica { get; set; }
        public string restante { get; set; }
        public string abonado { get; set; }

        public BitmapImage imagen_status { get; set; }
    }
}
