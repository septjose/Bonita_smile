using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bonita_smile_v1.Servicios
{
    class Sincronizar
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD,conexionBD2;
        Conexion_Offline obj = new Conexion_Offline();
        Test_Internet ti = new Test_Internet();
        Conexion obj2 = new Conexion();
        string ruta = @"c:\backup_bs\script_temporal.txt";

        public Sincronizar()
        {
            this.conexionBD = obj.conexion_offline();
            this.conexionBD2 = obj.conexion_offline();
        }
        public void Backup()
        {
            /*servidor = "162.241.60.126";
            puerto = "3306";
            usuario = "jjdevelo_dentist";
            password = "jjpd1996";
            database = "jjdevelo_dentist";
            */
            string constring = "server=162.241.60.126;user=jjdevelo_dentist;pwd=jjpd1996;database=jjdevelo_dentist;";
            constring += "charset=utf8;convertzerodatetime=true;";
            string file = @"C:\backup_bs\backup.sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        MessageBox.Show("Se hizo el respaldo correctamente");
                        conn.Close();
                    }
                }
            }
        }

        public void Restore()
        {
            string constring = "server=localhost;user=root;pwd=;database=dentista;";
            string file = @"C:\backup_bs\backup.sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        try
                        {
                            cmd.Connection = conn;

                            conn.Open();
                            mb.ImportFromFile(file);
                            MessageBox.Show("Se restauro correctamente");
                            
                        }
                        catch (Exception ex) { }
                        finally
                        {
                            conn.Close();
                        }
                        
                    }
                }
            }
        }

        public bool borrar_bd()
        {
            query = "drop database dentista";
            try
            {
                conexionBD2.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD2);
                cmd.ExecuteReader();
                conexionBD2.Close();

                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD2.Close();
                return false;
            }
        }


        public bool descargar_fotos()
        {
            var lista = lista_de_fotos();
            bool bandera = true;
            foreach (var filename in lista)
            {
                if(filename.Equals(""))
                {

                }
                else
                {
                    if (new Test_Internet().Test())
                    {
                        if (File.Exists(@"C:\bs\" + filename))
                        {
                            MessageBox.Show("SI ESTA"+filename);
                        }
                        else
                        {
                            MessageBox.Show("NO ESTA"+ filename);
                            bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", filename, @"C:\bs\" + filename, 10);

                        }


                    }
                    else
                    {
                        bandera = false;
                    }

                }

            }
            return bandera;


        }

        public bool descargar_fotos_clinica(string id_clinica)
        {
            var lista = lista_de_fotos_clinica(id_clinica);
            bool bandera = true;
            foreach (var filename in lista)
            {
                if (filename.Equals(""))
                {

                }
                else
                {
                    if (new Test_Internet().Test())
                    {
                        if (File.Exists(@"C:\bs\" + filename))
                        {
                            MessageBox.Show("SI ESTA" + filename);
                        }
                        else
                        {
                            MessageBox.Show("NO ESTA" + filename);
                            bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", filename, @"C:\bs\" + filename, 10);

                        }


                    }
                    else
                    {
                        bandera = false;
                    }

                }

            }
            return bandera;
        }

        private List<string> lista_de_fotos_clinica(string id_clinica)
        {
            string var = "";
            conexionBD = obj2.conexion(false);
            List<string> lista = new List<string>();
            query = "select paciente.foto from paciente inner join clinica on paciente.id_clinica=clinica.id_clinica where clinica.id_clinica='"+id_clinica+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var = var + reader[0].ToString() + "\n";
                    lista.Add(reader[0].ToString());
                }
                MessageBox.Show(lista.Count() + "" + var);
            }
            catch (Exception ex) { }
            conexionBD.Close();
            query = "select fotos_estudio_carpeta.foto from paciente inner join clinica on paciente.id_clinica=clinica.id_clinica inner join fotos_estudio_carpeta on fotos_estudio_carpeta.id_paciente=paciente.id_paciente where clinica.id_clinica='"+id_clinica+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var = var + reader[0].ToString() + "\n";
                    lista.Add(reader[0].ToString());
                }
                MessageBox.Show(lista.Count() + "" + var);
            }
            catch (Exception ex) { }
            return lista;
        }

        private List<string> lista_de_fotos()
        {
            string var = "";
            conexionBD = obj2.conexion(false);
            List<string> lista = new List<string>();
            query = "select foto from paciente";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    var = var + reader[0].ToString()+"\n";
                    lista.Add(reader[0].ToString());
                }
                MessageBox.Show( lista.Count()+""+var);
            }
            catch(Exception ex) { }
            conexionBD.Close();
            query = "SELECT foto from fotos_estudio_carpeta";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var = var + reader[0].ToString() + "\n";
                    lista.Add(reader[0].ToString());
                }
                MessageBox.Show(lista.Count() + "" + var);
            }
            catch (Exception ex) { }
            return lista;
        }

        public bool crear_bd()
        {

            query = "create database dentista;";
            try
            {
                conexionBD2.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD2);
                cmd.ExecuteReader();
                conexionBD2.Close();

                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD2.Close();
                return false;
            }
        }

        public bool SincronizarLocalServidor()
        {
            
            conexionBD = obj2.conexion(false);
            Escribir_Archivo ea = new Escribir_Archivo();
            bool internet = ti.Test();
            List<String> lQuery = new List<string>();
            //lQuery = ea.corregirArchivo();
            lQuery = ea.obtenerQueryArchivo();
            if(lQuery!=null)
            {

                if (!internet)
                {
                    MessageBox.Show("entro al if1");
                    MessageBox.Show("Intentelo más tarde, no cuenta con acceso a internet");
                    return false;
                }
                else
                {
                    //CREAR TRANSACCION
                    MySqlTransaction tr = null;
                    try
                    {
                        conexionBD.Open();

                        tr = conexionBD.BeginTransaction();
                        foreach (var query in lQuery)
                        {

                            if (!query.Equals(""))
                            {
                                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                                cmd.ExecuteReader();
                                cmd.Dispose();

                            }
                        }
                        tr.Commit();
                        File.Delete(ruta);
                        return true;

                    }
                    catch (Exception ex) {
                        MessageBox.Show("entro al catch :(");
                        MessageBox.Show("error intente mas tarde");
                        tr.Rollback(); 
                        return false; }
                    finally
                    {
                        conexionBD.Close();
                    }
                    
                }
            }
            else
            {
                return false;
            }

        }

        /*public bool SincronizarLocalServidor()
        {
            conexionBD = obj2.conexion();
            Escribir_Archivo ea = new Escribir_Archivo();
            bool internet = ti.Test();
            List<String> lQuery = new List<string>();
            //lQuery = ea.corregirArchivo();
            lQuery = ea.obtenerQueryArchivo();

            if(lQuery!=null)
            {

                if (!internet)
                {
                    MessageBox.Show("Intentelo más tarde, no cuenta con acceso a internet");
                    return false;
                }
                else
                {
                    //CREAR TRANSACCION
                    foreach (var query in lQuery)
                    {
                        MessageBox.Show(query);
                        if (!query.Equals(""))
                        {
                            MessageBox.Show("entroquery");
                            conexionBD.Open();
                            MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                            cmd.ExecuteReader();
                            conexionBD.Close();
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }*/

        private List<string> Obtener_nombres_archivos()
        {
            List<string> lista = new List<string>();
            string ruta = @"C:\fotos_offline\";

            DirectoryInfo di = new DirectoryInfo(ruta);
            
            foreach (var fi in di.GetFiles())
            {
                //MessageBox.Show(fi.ToString());
                lista.Add(fi.ToString());
            }

            return lista;

        }




        public bool subir_fotos()
        {
            var lista = Obtener_nombres_archivos();
            bool bandera = true;
            foreach (var filename in lista)
            {
               if(new Test_Internet().Test())
                {
                    SubirFicheroStockFTP(filename, @"C:\fotos_offline\");
                    File.Delete(@"C:\fotos_offline\" + filename);
                   
                }
                else
                {
                    bandera =false;
                }
            
            }
            return bandera;


        }

        private bool SubirFicheroStockFTP(string foto, string ruta)
        {
            bool verdad;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                        "ftp://jjdeveloperswdm.com" +
                        "/" +
                       foto);

                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        ruta,
                        foto);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(
                        "bonita_smile@jjdeveloperswdm.com",
                        "bonita_smile");

                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var fileStream = File.OpenRead(filePath))
                {
                    using (var requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyTo(requestStream);
                        requestStream.Close();
                    }
                }

                var response = (FtpWebResponse)request.GetResponse();

                response.Close();
                verdad = true;
            }
            catch (Exception ex)
            {
                //logger.Error("Error " + ex.Message + " " + ex.StackTrace);
                verdad = false;
                System.Windows.MessageBox.Show("Error " + ex.Message + " " + ex.StackTrace);


            }
            return verdad;

        }

        public bool downloadFile(string servidor, string usuario, string password, string archivoOrigen, string carpetaDestino, int bufferdes)
        {
            bool descargar;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(servidor + archivoOrigen);
                reqFTP.Credentials = new NetworkCredential(usuario, password);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = true;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(@carpetaDestino, FileMode.Create);
                int Length = bufferdes;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
                descargar = true;
            }
            catch (WebException wEx)
            {
                descargar = false;
                throw wEx;

            }
            catch (Exception ex)
            {
                descargar = false;
                throw ex;


            }
            return descargar;
        }

        public bool insertarArchivoEnServidor(MySqlConnection conexionBD)
        {
            Escribir_Archivo ea = new Escribir_Archivo();
            List<String> lQuery = new List<string>();
            lQuery = ea.obtenerQueryArchivo();

            if (lQuery != null)
            {
                //CREAR TRANSACCION
                MySqlTransaction tr = null;
                try
                {
                    conexionBD.Open();

                    tr = conexionBD.BeginTransaction();
                    foreach (var query in lQuery)
                    {
                        if (!query.Equals(""))
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                            cmd.ExecuteReader();
                            cmd.Dispose();
                        }
                    }
                    tr.Commit();

                    ea.SetFileReadAccess(ruta,false);
                    File.Delete(ruta);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                    tr.Rollback();
                    return false;
                }
                finally
                {
                    conexionBD.Close();
                }
            }

            return false;
        }





    }
}