using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class MarketingModel
    {
        public int id_marketing { get; set; }
        public string descripcion { get; set; }
        public string fecha_de_envio { get; set; }
        public int id_paciente { get; set; }
    }
}
