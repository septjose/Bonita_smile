using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

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
            //FileAttributes attributes = File.GetAttributes(ruta);
            FileStream fs = null;
            List<string> datos = new List<string>();
            datos.Add(configuracion.servidor_interno.servidor_local);
            datos.Add(configuracion.servidor_interno.puerto_local);
            datos.Add(configuracion.servidor_interno.usuario_local);
            datos.Add(configuracion.servidor_interno.password_local);
            datos.Add(configuracion.servidor_interno.database_local);
            datos.Add(configuracion.servidor_interno.database_local_aux);

            datos.Add(configuracion.servidor_externo.servidor_local);
            datos.Add(configuracion.servidor_externo.puerto_local);
            datos.Add(configuracion.servidor_externo.usuario_local);
            datos.Add(configuracion.servidor_externo.password_local);
            datos.Add(configuracion.servidor_externo.database_local);

            datos.Add(configuracion.carpetas.ruta_fotografias_carpeta);
            datos.Add(configuracion.carpetas.ruta_imagenes_carpeta);
            datos.Add(configuracion.carpetas.ruta_subir_servidor_carpeta);
            datos.Add(configuracion.carpetas.ruta_respaldo_carpeta);
            datos.Add(configuracion.carpetas.ruta_script_carpeta);
            datos.Add(configuracion.carpetas.ruta_eliminar_carpeta);
            datos.Add(configuracion.carpetas.ruta_acceso_deirecto);

            datos.Add(configuracion.ftp.ftp_server);
            datos.Add(configuracion.ftp.ftp_password);
            datos.Add(configuracion.ftp.ftp_path);
            datos.Add(configuracion.ftp.ftp_user);
            datos.Add(configuracion.ftp.nombre_impresora);
            




            foreach (var script in datos)
            {
                try
                {
                    if (File.Exists(ruta))
                    {
                        SetFileReadAccess(ruta, false);
                        // Create the file, or overwrite if the file exists.
                        StreamWriter sw = new StreamWriter(ruta, true);
                        sw.WriteLine(new Seguridad().Encriptar(script));
                        sw.Close();
                        File.SetAttributes(ruta, File.GetAttributes(ruta) | FileAttributes.Hidden);
                        //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                        SetFileReadAccess(ruta, true);
                    }
                    else
                    {
                        fs = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                        fs.Close();
                        StreamWriter sw = new StreamWriter(ruta, true);
                        sw.WriteLine(new Seguridad().Encriptar(script));
                        sw.Close();
                        File.SetAttributes(ruta, File.GetAttributes(ruta) | FileAttributes.Hidden);
                        //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                        SetFileReadAccess(ruta, true);
                    }
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Console.WriteLine(ex.ToString());
                }
            }
        }

        public Configuracion_Model Cargar(string ruta)
        {
           
            List<string> lista = new List<string>();
            if (File.Exists(ruta))
            {
                using (StreamReader sr = File.OpenText(ruta))
                {

                    string query = "";
                    while ((query = sr.ReadLine()) != null)
                    {
                        lista.Add(query);
                    }

                    Configuracion_Model configuracion;
                    ServidorModelo servidor_intern = new ServidorModelo()
                    {
                        servidor_local = new Seguridad().Desencriptar(lista[0]),
                        puerto_local = new Seguridad().Desencriptar(lista[1]),
                        usuario_local = new Seguridad().Desencriptar(lista[2]),
                        password_local = new Seguridad().Desencriptar(lista[3]),
                        database_local = new Seguridad().Desencriptar(lista[4]),
                        database_local_aux = new Seguridad().Desencriptar(lista[5]),
                    };

                    ServidorModelo servidor_extern = new ServidorModelo()
                    {
                        servidor_local = new Seguridad().Desencriptar(lista[6]),
                        puerto_local = new Seguridad().Desencriptar(lista[7]),
                        usuario_local = new Seguridad().Desencriptar(lista[8]),
                        password_local = new Seguridad().Desencriptar(lista[9]),
                        database_local = new Seguridad().Desencriptar(lista[10]),
                    };
                    RutasCarpetasModelo carpeta = new RutasCarpetasModelo()
                    {
                        ruta_fotografias_carpeta = new Seguridad().Desencriptar(lista[11]),
                        ruta_imagenes_carpeta = new Seguridad().Desencriptar(lista[12]),
                        ruta_subir_servidor_carpeta = new Seguridad().Desencriptar(lista[13]),
                        ruta_respaldo_carpeta = new Seguridad().Desencriptar(lista[14]),
                        ruta_script_carpeta = new Seguridad().Desencriptar(lista[15]),
                        ruta_eliminar_carpeta = new Seguridad().Desencriptar(lista[16]),
                        ruta_acceso_deirecto = new Seguridad().Desencriptar(lista[17]),
                    };

                    ConfiguracionFTPModel ftps = new ConfiguracionFTPModel()
                    {
                        ftp_server = new Seguridad().Desencriptar(lista[18]),
                        ftp_password = new Seguridad().Desencriptar(lista[19]),
                        ftp_path = new Seguridad().Desencriptar(lista[20]),
                        ftp_user = new Seguridad().Desencriptar(lista[21]),
                        nombre_impresora = new Seguridad().Desencriptar(lista[22]),
                        
                    };


                    configuracion = new Configuracion_Model()
                    {
                        carpetas = carpeta,
                        servidor_externo = servidor_extern,
                        servidor_interno = servidor_intern,
                        ftp=ftps,

                    };
                   
                    return configuracion;
                }
            }
            else
            {
                return null;
            }
        }

        public void SetFileReadAccess(string FileName, bool SetReadOnly)
        {
            // Create a new FileInfo object.
            FileInfo fInfo = new FileInfo(FileName);

            // Set the IsReadOnly property.
            fInfo.IsReadOnly = SetReadOnly;
        }
    }
}
