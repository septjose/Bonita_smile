using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    public class Archivo_Binario
    {
        public void CrearArchivo(string ruta)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                fs = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                //bw.Write(cadena);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                if (bw != null)
                {
                    fs.Close();
                    bw.Close();
                }
            }
        }
        public void Agregar_a_Archivo(string ruta, Configuracion_Model configuracion)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                fs = new FileStream(ruta, FileMode.Append, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(7);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                if (bw != null)
                {
                    fs.Close();
                    bw.Close();
                }
            }
        }
        public void Leer(string ruta)
        {
            //Alternativa sin crear el objeto FileStream
            //FileStream fs = null;
            BinaryReader br = null;
            try
            {
                //fs = new FileStream(nomarch, FileMode.Open, FaileAcces.Read);
                if (File.Exists(ruta))
                {
                    //fs = new BinaryReader(fs);
                    br = new BinaryReader(new FileStream(ruta, FileMode.Open, FileAccess.Read));
                    string val;
                    do
                    {
                        val = br.ReadString();
                    } while (true);
                }
                else
                {
                    Console.WriteLine("El archivo no existe");
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("Fin del archivo");
            }
            finally
            {
                if (br != null)
                {
                    //fs.close();
                    br.Close();
                }
            }
        }

        public void Guardar(Configuracion_Model configuracion,string ruta)
        {       
            if(File.Exists(ruta))
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
