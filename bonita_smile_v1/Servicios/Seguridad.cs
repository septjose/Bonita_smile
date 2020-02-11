using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    class Seguridad
    {
        ////////https://odan.github.io/2017/08/10/aes-256-encryption-and-decryption-in-php-and-csharp.html
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

        public Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public  bool ValidarTelefonos7a10Digitos(string strNumber)
        {
            Regex regex = new Regex("\\A[0-9]{10,12}\\z");
            Match match = regex.Match(strNumber);

            if (match.Success)
                return true;
            else
                return false;
        }

        public bool validar_numero(string strNumber)
        {
            /*
La expresión regular que he usado para que admitiria estos casos,   4 o 4.1 o 4.15 o 0.15 o 44444444.15   es:

"^[0-9]+(\\,[0-9]{1,2})?$"
             */
            Regex regex = new Regex("^[0-9]+([.][0-9]+)?$");
            //Regex regex = new Regex(" ^[0 - 9] + (\\,[0-9]{1,2})?$");
            Match match = regex.Match(strNumber);

            if (match.Success)
                return true;
            else
                return false;
        }


    }
}
