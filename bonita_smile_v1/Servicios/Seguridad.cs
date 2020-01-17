using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public string SHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
