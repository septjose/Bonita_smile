using bonita_smile_v1.Modelos;
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
        string alias;
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD,conexionBD2;
        Conexion_Offline obj = new Conexion_Offline();
        Test_Internet ti = new Test_Internet();
        Conexion obj2 = new Conexion();
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");

        string ruta;
        string ruta_borrar ;
        //string ruta = @"\\DESKTOP-ED8E774\backup_bs\script_temporal.txt";
        //string ruta_borrar= @"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt";
        

        public Sincronizar()
        {

            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            this.ruta = @configuracion.carpetas.ruta_respaldo_carpeta+ "\\script_temporal.txt";
            this.ruta_borrar = @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt";
            
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
           
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            // string constring = "server=162.241.60.126;user=jjdevelo_dentist;pwd=jjpd1996;database=jjdevelo_dentist;";
            string constring = "server="+configuracion.servidor_externo.servidor_local+";user="+configuracion.servidor_externo.usuario_local+";pwd="+configuracion.servidor_externo.password_local+";database="+configuracion.servidor_externo.database_local+";";
            constring += "charset=utf8;convertzerodatetime=true;";
            MessageBox.Show(constring);
            string file = @configuracion.carpetas.ruta_respaldo_carpeta+"\\backup.sql";
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
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            string constring = "server="+configuracion.servidor_interno.servidor_local+";user="+configuracion.servidor_interno.usuario_local+";pwd="+configuracion.servidor_interno.password_local+";database="+configuracion.servidor_interno.database_local+";";
            string file = @configuracion.carpetas.ruta_respaldo_carpeta + "\\backup.sql";
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
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

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
                        if (File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta+"\\" + filename))
                        {
                            MessageBox.Show("SI ESTA"+filename);
                        }
                        else
                        {
                            MessageBox.Show("NO ESTA"+ filename);
                            // bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", filename, @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename, 10);
                            bool descargo = downloadFile(configuracion.ftp.ftp_server+configuracion.ftp.ftp_path, configuracion.ftp.ftp_user, configuracion.ftp.ftp_password, filename, @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename, 10);
                            if (descargo)
                            {
                                MessageBox.Show("si descargo");
                            }
                            else
                            {
                                MessageBox.Show("no se descargo");
                            }
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

        public bool descargar_fotos_socio(List<string> list_clinicas)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            var lista = lista_de_fotos_socio(list_clinicas);
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
                        if (File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename))
                        {
                            MessageBox.Show("SI ESTA" + filename);
                        }
                        else
                        {
                            MessageBox.Show("NO ESTA" + filename);
                            //bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", filename, @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename, 10);
                            bool descargo = downloadFile(configuracion.ftp.ftp_server + configuracion.ftp.ftp_path, configuracion.ftp.ftp_user, configuracion.ftp.ftp_password, filename, @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename, 10);

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
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

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
                        if (File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename))
                        {
                            MessageBox.Show("SI ESTA" + filename);
                        }
                        else
                        {
                            MessageBox.Show("NO ESTA" + filename);
                            //bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", filename, @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename, 10);
                            bool descargo = downloadFile(configuracion.ftp.ftp_server + configuracion.ftp.ftp_path, configuracion.ftp.ftp_user, configuracion.ftp.ftp_password, filename, @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + filename, 10);

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

        private List<string> lista_de_fotos_socio(List<string> list_clinicas)
        {
            string var = "";
            conexionBD = obj2.conexion(false);
            List<string> lista = new List<string>();
            foreach(var id_clinica in list_clinicas)
            {
                query = "select paciente.foto from paciente inner join clinica on paciente.id_clinica=clinica.id_clinica where clinica.id_clinica='" + id_clinica + "'";
                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       if(reader[0].ToString().Equals(""))
                        {

                        }
                       else
                        {
                            lista.Add(reader[0].ToString());
                        }
                        
                    }
                    MessageBox.Show(lista.Count() + "" + var);
                }
                catch (Exception ex) { }
                conexionBD.Close();
                query = "select fotos_estudio_carpeta.foto from paciente inner join clinica on paciente.id_clinica=clinica.id_clinica inner join fotos_estudio_carpeta on fotos_estudio_carpeta.id_paciente=paciente.id_paciente where clinica.id_clinica='" + id_clinica + "'";
                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader[0].ToString().Equals(""))
                        {

                        }
                        else
                        {
                            lista.Add(reader[0].ToString());
                        }
                    }
                    MessageBox.Show(lista.Count() + "" + var);
                }
                catch (Exception ex) { }
            }
            
            return lista;
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
                    if (reader[0].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        lista.Add(reader[0].ToString());
                    }
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
                    if (reader[0].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        lista.Add(reader[0].ToString());
                    }
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
                    if (reader[0].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        lista.Add(reader[0].ToString());
                    }
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
                    if (reader[0].ToString().Equals(""))
                    {

                    }
                    else
                    {
                        lista.Add(reader[0].ToString());
                    }
                }
                MessageBox.Show(lista.Count() + "\n" + var);
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
            
            conexionBD = obj2.conexion(true);
            Escribir_Archivo ea = new Escribir_Archivo();
            bool internet = ti.Test();
            List<string> lQuery = new List<string>();
            //lQuery = ea.corregirArchivo();
            lQuery = ea.obtenerQueryArchivo();
            MessageBox.Show(lQuery.Count() + "");
            foreach(var q in lQuery)
            {
                MessageBox.Show(q);
            }
            return true;
            //if(lQuery!=null)
            //{

            //    if (!internet)
            //    {
            //        MessageBox.Show("entro al if1");
            //        MessageBox.Show("Intentelo más tarde, no cuenta con acceso a internet");
            //        return false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("entro al else");
            //        //CREAR TRANSACCION
            //        MySqlTransaction tr = null;
            //        try
            //        {
            //            MessageBox.Show("entro al try");
            //            conexionBD.Open();

            //            tr = conexionBD.BeginTransaction();
            //            foreach (var query in lQuery)
            //            {

            //                if (!query.Equals(""))
            //                {
            //                    MessageBox.Show("entro aqui" + query);
            //                    Console.WriteLine("query : ->" +query);
            //                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);
            //                    cmd.ExecuteReader();
            //                    cmd.Dispose();

            //                }
            //            }
            //            tr.Commit();
            //            ea.SetFileReadAccess(ruta, false);
            //            File.Delete(ruta);
            //            return true;

            //        }
            //        catch (Exception ex) {
            //            MessageBox.Show("entro al catch :("+ex.ToString());
            //            MessageBox.Show("error intente mas tarde");
            //            tr.Rollback(); 
            //            return false; }
            //        finally
            //        {
            //            conexionBD.Close();
            //        }
                    
            //    }
            //}
            //else
            //{
            //    return false;
            //}

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
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            List<string> lista = new List<string>();
            string ruta = @configuracion.carpetas.ruta_subir_servidor_carpeta + "\\";
               

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
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            var lista = Obtener_nombres_archivos();
            bool bandera = true;
            foreach (var filename in lista)
            {
               if(new Test_Internet().Test())
                {
                    SubirFicheroStockFTP(filename, @configuracion.carpetas.ruta_subir_servidor_carpeta + "\\");
                    if(File.Exists(@configuracion.carpetas.ruta_subir_servidor_carpeta+"\\"+filename))
                    {
                        File.Delete(@configuracion.carpetas.ruta_subir_servidor_carpeta +"\\" + filename);

                    }
                    else
                    {

                    }
                   // File.Delete(@"\\DESKTOP-ED8E774\fotos_offline\" + filename);
                   
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
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            bool verdad;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                        configuracion.ftp.ftp_server +
                        configuracion.ftp.ftp_path +
                       foto);

                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        ruta,
                        foto);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(
                        configuracion.ftp.ftp_user,
                        configuracion.ftp.ftp_password);

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
                    if(tr==null)
                    {
                        MessageBox.Show("No se pudo conectar a la conexion , Intentelo mas tarde!! :(");
                    }
                    else
                    {
                        tr.Rollback();
                    }
                   
                    return false;
                }
                finally
                {
                    conexionBD.Close();
                }
            }

            return false;
        }


        public bool eliminar_fotos()
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            Escribir_Archivo ea = new Escribir_Archivo();
            List<String> lista = new List<string>();
            lista = ea.obtener_nombre_foto_eliminar();
            bool eliminarArchivo = true;

            if (lista != null)
            {
                //CREAR TRANSACCION

                try
                {
                    //ELIMINAR FOTOS DE SERVIDOR, OBTENIENDO NOMBRE DEL ARCHIVO
                    var datos = ea.leer(ruta_borrar);

                    foreach (string imagen in datos)
                    {

                        Uri siteUri = new Uri(configuracion.ftp.ftp_server+configuracion.ftp.ftp_path + imagen);
                        bool verdad = DeleteFileOnServer(siteUri, configuracion.ftp.ftp_user, configuracion.ftp.ftp_password);

                        if (!verdad)
                            eliminarArchivo = false;
                    }
                    if (eliminarArchivo)
                    {
                        System.Windows.MessageBox.Show("elimino Archivo");
                        ea.SetFileReadAccess(ruta_borrar, false);

                        File.Delete(@configuracion.carpetas.ruta_respaldo_carpeta+"\\eliminar_imagen_temporal.txt");
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");

                    return false;
                }
            }
            else
                return false;
        }

        public static bool DeleteFileOnServer(Uri serverUri, string ftpUsername, string ftpPassword)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);

                //If you need to use network credentials
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                //additionally, if you want to use the current user's network credentials, just use:
                //System.Net.CredentialCache.DefaultNetworkCredentials


                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
                return true;
            }
            catch (WebException e)
            {
                FtpWebResponse response = (FtpWebResponse)e.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return true;
                }
                return false;
            }
        }

    }
}