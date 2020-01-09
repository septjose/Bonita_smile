using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    class Escribir_Archivo
    {
        string ruta = @"c:\offline\script.txt";
        public void escribir(string script)
        {
            try
            {
                // Create the file, or overwrite if the file exists.
                StreamWriter sw = new StreamWriter(ruta, true);
                sw.WriteLine(script);
                sw.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void leer(string ruta)
        {
            using (StreamReader sr = File.OpenText(ruta))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

    }
}
    

