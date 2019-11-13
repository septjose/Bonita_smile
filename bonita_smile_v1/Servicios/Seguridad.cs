using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    class Seguridad
    {
        public string Encriptar(string cadena)
        {
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
            string encriptada = Convert.ToBase64String(encryted);
            return encriptada;
        }

        public string Desencriptar(string cadena)
        {
            byte[] decryted = Convert.FromBase64String(cadena);
            string desencriptada = Encoding.Unicode.GetString(decryted);
            return desencriptada;
        }
    }
}
