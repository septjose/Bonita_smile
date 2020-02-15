using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Servicios
{
    public class Archivo_Binario
    {
        public void Guardar(Configuracion_Model configuracion, string ruta)
        {
            if (File.Exists(ruta))
            {
                BinaryFormatter BF = new BinaryFormatter();
                FileStream Archivo = File.OpenWrite(ruta);
                BF.Serialize(Archivo, configuracion);
                Archivo.Close();
            }

        }

        public Configuracion_Model Cargar(string ruta)
        {
            if (!File.Exists(ruta))
            {
                return null;
            }
            else
            {
                BinaryFormatter BF = new BinaryFormatter();
                FileStream Archivo = File.Open(ruta, FileMode.Open);
                Configuracion_Model DatosCargados = (Configuracion_Model)BF.Deserialize(Archivo);
                Archivo.Close();
                return DatosCargados;
            }
        }
    }
}
