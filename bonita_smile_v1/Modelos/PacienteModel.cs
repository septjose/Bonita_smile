using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Modelos
{
    public class PacienteModel
    {
        public string id_paciente { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string foto { get; set; }
        public BitmapImage imagen { get; set; }
        
        public string email { get; set; }
        public int marketing { get; set; }
        public ClinicaModel clinica { get; set; }
        public string antecedente { get; set; }

        public string membresia { get; set; }

        public BitmapImage imagen_membresia {get;set;}
        public string fecha { get; set; }
        public bool factura { get; set; }
    }
}