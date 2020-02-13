using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    [Serializable]
    public class ServidorModelo
    {
        public string servidor_local { get; set; }
        public string puerto_local { get; set; }
        public string usuario_local { get; set; }
        public string password_local { get; set; }
        public string database_local { get; set; }
        public string database_local_aux { get; set; }
    }
}
