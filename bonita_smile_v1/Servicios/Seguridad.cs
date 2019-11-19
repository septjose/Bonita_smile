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
            string encriptada = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
            encriptada = Convert.ToBase64String(encryted);
            return encriptada;
        }

        public string Desencriptar(string cadena)
        {
            string desencriptada = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadena);
            desencriptada = System.Text.Encoding.Unicode.GetString(decryted);
            return desencriptada;
        }
    }
}
