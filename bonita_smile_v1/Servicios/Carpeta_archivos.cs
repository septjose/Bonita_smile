using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bonita_smile_v1.Servicios
{
    class Carpeta_archivos
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Carpeta_archivos()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Carpeta_archivosModel> MostrarCarpeta_archivos()
        {
            List<Carpeta_archivosModel> listaCarpeta_archivos = new List<Carpeta_archivosModel>();
            query = "SELECT * FROM carpeta_archivos";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Carpeta_archivosModel carpeta_ArchivosModel = new Carpeta_archivosModel();

                    carpeta_ArchivosModel.id_carpeta = int.Parse(reader[0].ToString());
                    carpeta_ArchivosModel.nombre_carpeta = reader[1].ToString();
                    carpeta_ArchivosModel.id_paciente = int.Parse(reader[2].ToString());

                    listaCarpeta_archivos.Add(carpeta_ArchivosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaCarpeta_archivos;
        }

        public bool eliminarCarpeta_archivos(int id_carpeta)
        {
            query = "DELETE FROM carpeta_archivos where id_carpeta=" + id_carpeta;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarCarpeta_archivos(string nombre_carpeta, int id_paciente)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                Seguridad seguridad = new Seguridad();
                string auxiliar_identificador = seguridad.Encriptar(nombre_carpeta + id_paciente);
                query = "INSERT INTO carpeta_archivos (nombre_carpeta,id_paciente,auxiliar_identificador) VALUES('" + nombre_carpeta + "'," + id_paciente + ",'" + auxiliar_identificador + "')";
            }
            else
            {
                query = "INSERT INTO carpeta_archivos (nombre_carpeta,id_paciente) VALUES('" + nombre_carpeta + "'," + id_paciente + ")";
            }
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                if (!internet)
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                conexionBD.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarCarpeta_archivos(int id_carpeta, string nombre_carpeta, int id_paciente)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // = seguridad.Encriptar(nombre_carpeta + id_paciente);
                string auxiliar_identificador = MostrarCarpeta_Archivos_Update(id_carpeta);
                query = "UPDATE carpeta_archivos set nombre_carpeta = '" + nombre_carpeta + "',id_paciente = " + id_paciente + ",auxiliar_identificador = '<!--" + auxiliar_identificador + "-->' where id_carpeta = " + id_carpeta;
            }
            else
            {
                query = "UPDATE carpeta_archivos set nombre_carpeta = '" + nombre_carpeta + "',id_paciente = " + id_paciente + " where id_carpeta = " + id_carpeta;
            }
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                if (!internet)
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                conexionBD.Close();
                return false;
            }
        }

        public string MostrarCarpeta_Archivos_Update(int id_carpeta)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from carpeta_archivos where id_carpeta=" + id_carpeta;

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
        public List<Carpeta_archivosModel> MostrarCarpeta_archivos_paciente(int id_paciente)
        {
            List<Carpeta_archivosModel> listaCarpeta_archivos = new List<Carpeta_archivosModel>();
            query = "SELECT * FROM carpeta_archivos where id_paciente=" + id_paciente;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Carpeta_archivosModel carpeta_ArchivosModel = new Carpeta_archivosModel();

                    carpeta_ArchivosModel.id_carpeta = int.Parse(reader[0].ToString());
                    carpeta_ArchivosModel.nombre_carpeta = reader[1].ToString();
                    carpeta_ArchivosModel.id_paciente = int.Parse(reader[2].ToString());

                    listaCarpeta_archivos.Add(carpeta_ArchivosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaCarpeta_archivos;
        }
    }
}
