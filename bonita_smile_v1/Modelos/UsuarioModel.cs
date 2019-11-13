using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    class UsuarioModel
    {
        public int id_usuario { get; set; }
        public string alias { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string password { get; set; }
        public int id_rol { get; set; }
    }
}
