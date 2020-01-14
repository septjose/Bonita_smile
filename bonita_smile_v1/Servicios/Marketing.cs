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
    class Marketing
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Marketing()
        {
            this.conexionBD = obj.conexion();
        }

        public List<MarketingModel> MostrarMarketing()
        {
            List<MarketingModel> listaMarketing = new List<MarketingModel>();
            query = "SELECT * FROM marketing";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MarketingModel marketingModel = new MarketingModel();

                    marketingModel.id_marketing = int.Parse(reader[0].ToString());
                    marketingModel.descripcion = reader[1].ToString();
                    marketingModel.fecha_de_envio = reader[2].ToString();
                    marketingModel.id_paciente = int.Parse(reader[3].ToString());

                    listaMarketing.Add(marketingModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaMarketing;
        }

        public bool eliminarMarketing(int id_marketing)
        {
            query = "DELETE FROM clinica where id_marketing="+ id_marketing;
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

        public bool insertarMarketing(string descripcion, string fecha_de_envio, int id_paciente)
        {
            query = "INSERT INTO marketing (descripcion,fecha_de_envio,id_paciente) VALUES('"+ descripcion +"','"+ fecha_de_envio +"',"+ id_paciente +")";
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

        public bool actualizarMarketing(int id_marketing, string descripcion, string fecha_de_envio, int id_paciente)
        {
            query = "UPDATE marketing set descripcion = '"+ descripcion +"',fecha_de_envio = '" + fecha_de_envio +"',id_paciente = "+ id_paciente +" where id_marketing = "+ id_marketing;
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
