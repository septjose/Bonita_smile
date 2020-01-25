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
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Servicios
{
    class Fotos_estudio_carpeta
    {
        private string ruta2 = @"C:\bs\";
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Fotos_estudio_carpeta()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Fotos_estudio_carpetaModel> MostrarFoto_estudio_carpeta(string id_carpeta, string id_paciente)
        {
            List<Fotos_estudio_carpetaModel> listaFoto_estudio_carpeta = new List<Fotos_estudio_carpetaModel>();
            query = "SELECT  * FROM fotos_estudio_carpeta where id_carpeta='" + id_carpeta + "' and id_paciente='" + id_paciente+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fotos_estudio_carpetaModel fotos_Estudio_CarpetaModel = new Fotos_estudio_carpetaModel();

                    fotos_Estudio_CarpetaModel.id_foto = reader[0].ToString();
                    fotos_Estudio_CarpetaModel.id_carpeta = reader[1].ToString();
                    fotos_Estudio_CarpetaModel.id_paciente = reader[2].ToString();
                    fotos_Estudio_CarpetaModel.foto = reader[3].ToString();
                    fotos_Estudio_CarpetaModel.imagen = LoadImage(@"C:\bs\" + reader[3].ToString());

                    listaFoto_estudio_carpeta.Add(fotos_Estudio_CarpetaModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaFoto_estudio_carpeta;

        }

        public bool eliminarFoto_estudio_carpeta(string id_foto)
        {
            query = "DELETE FROM fotos_estudio_carpeta where id_foto='" + id_foto+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                   // Escribir_Archivo ea = new Escribir_Archivo();
                   // ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarFoto_estudio_carpeta(string id_carpeta, string id_paciente, string foto)
        {
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            MessageBox.Show((foto.Length/2)+"");
            MessageBox.Show(foto.Length + "");
            foto = foto.Substring((foto.Length/4)*3);


            MessageBox.Show("foto = " + foto);
            //string auxiliar_carpeta = seguridad.Encriptar(id_carpeta);
           //string  auxiliar_paciente = seguridad.Encriptar(id_paciente);
           //string  auxiliar_foto = seguridad.Encriptar(foto);
            auxiliar_identificador=seguridad.SHA1(id_carpeta+id_paciente+foto);
            //auxiliar_identificador = seguridad.Encriptar(id_carpeta + id_paciente + foto);
            bool internet = ti.Test();
            if (!internet)
            {
               
                query = "INSERT INTO fotos_estudio_carpeta (id_foto,id_carpeta,id_paciente,foto,auxiliar_identificador) VALUES('"+auxiliar_identificador +"','"+ id_carpeta + "','" + id_paciente + "','" + foto + "','<!--" + auxiliar_identificador + "-->')";
            }
            else
            {
                query = "INSERT INTO fotos_estudio_carpeta (id_foto,id_carpeta,id_paciente,foto) VALUES('" + auxiliar_identificador + "','" + id_carpeta + "','" + id_paciente + "','" + foto + "')";
            }

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!internet)
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir( query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarFoto_estudio_carpeta(string id_foto, string id_carpeta, string id_paciente, string foto)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // = seguridad.Encriptar(id_carpeta + id_paciente + foto);
                string auxiliar_identificador = MostrarFotos_Update(id_foto);
                query = "UPDATE fotos_estudio_carpeta set id_paciente = '" + id_paciente + "',id_carpeta = '" + id_carpeta + "',foto = '" + foto + ",auxiliar_identificador = '" + auxiliar_identificador + "' where id_foto = '" + id_foto+"'";
            }
            else
            {
                query = "UPDATE fotos_estudio_carpeta set id_paciente = '" + id_paciente + "',id_carpeta = '" + id_carpeta + "',foto = '" + foto + "' where id_foto = '" + id_foto+"'";
            }
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public string MostrarFotos_Update(string id_foto)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from fotos_estudio_carpeta where id_foto='" + id_foto+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    aux_identi = reader[0].ToString();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return aux_identi;
        }
        private BitmapImage LoadImage(string filename)
        {
            BitmapImage bi;

            if (File.Exists(filename))
            {
                MessageBox.Show("si lo encontro la" + filename);
                bi = new BitmapImage(new Uri(filename));
            }
            else
            {
                MessageBox.Show("No la encontro");
                bi = new BitmapImage(new Uri(@"C:\bs\img1.jpg"));
            }
            return bi;
        }
        public void fotos(string id_carpeta, string id_paciente)
        {
            string fotito = "";
            List<string> listaFoto_estudio_carpeta = new List<string>();
            query = "SELECT  foto FROM fotos_estudio_carpeta where id_carpeta='" + id_carpeta + "' and id_paciente='" + id_paciente+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {



                    fotito = reader[0].ToString();

                    listaFoto_estudio_carpeta.Add(fotito);
                }
                foreach (var l in listaFoto_estudio_carpeta)
                {
                    bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", l, ruta2 + l, 10);
                    if (descargo)
                    {
                        System.Windows.MessageBox.Show(":)");

                    }
                    else
                    {
                        System.Windows.MessageBox.Show(":(");
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            //return listaFoto_estudio_carpeta;

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
    }
}
