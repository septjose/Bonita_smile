using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
   public class abonos_membresiaModel
    {
        public string id_abono_membresia { get; set; }
        public string fecha { get; set; }
        public string monto { get; set; }
        public string comentario { get; set; }
        public MembresiaModel membresia { get; set; }
    }
}
