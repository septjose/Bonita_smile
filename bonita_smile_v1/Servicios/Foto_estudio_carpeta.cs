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
    class Fotos_estudio_carpeta
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Fotos_estudio_carpeta()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Fotos_estudio_carpetaModel> MostrarFoto_estudio_carpeta()
        {
            List<Fotos_estudio_carpetaModel> listaFoto_estudio_carpeta = new List<Fotos_estudio_carpetaModel>();
            query = "SELECT * FROM fotos_estudio_carpeta";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fotos_estudio_carpetaModel fotos_Estudio_CarpetaModel = new Fotos_estudio_carpetaModel();

                    fotos_Estudio_CarpetaModel.id_foto = int.Parse(reader[0].ToString());
                    fotos_Estudio_CarpetaModel.id_carpeta = int.Parse(reader[1].ToString());
                    fotos_Estudio_CarpetaModel.id_paciente = int.Parse(reader[2].ToString());
                    fotos_Estudio_CarpetaModel.foto = reader[3].ToString();

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

        public bool eliminarFoto_estudio_carpeta(int id_foto)
        {
            query = "DELETE FROM fotos_estudio_carpeta where id_foto="+ id_foto;
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

        public bool insertarFoto_estudio_carpeta(int id_carpeta, int id_paciente, string foto)
        {
            query = "INSERT INTO fotos_Estudio_carpeta (id_carpeta,id_paciente) VALUES("+ id_carpeta +","+ id_paciente +",'"+ foto +"')";
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

        public bool actualizarFoto_estudio_carpeta(int id_foto, int id_carpeta, int id_paciente, string foto)
        {
            query = "UPDATE fotos_estudio_carpeta set id_paciente = "+ id_paciente +",id_carpeta = "+ id_carpeta +",foto = '"+ foto +"' where id_foto = "+ id_foto;
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
    }
}
