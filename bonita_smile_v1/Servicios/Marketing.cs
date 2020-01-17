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

                    marketingModel.id_marketing = reader[0].ToString();
                    marketingModel.descripcion = reader[1].ToString();
                    marketingModel.fecha_de_envio = reader[2].ToString();
                    marketingModel.id_paciente = reader[3].ToString();

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
            query = "DELETE FROM clinica where id_marketing=" + id_marketing;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
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

        public bool insertarMarketing(string descripcion, string fecha_de_envio, int id_paciente)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                Seguridad seguridad = new Seguridad();
                string auxiliar_identificador = seguridad.SHA1(descripcion + fecha_de_envio + id_paciente);
                query = "INSERT INTO marketing (descripcion,fecha_de_envio,id_paciente,auxiliar_identificador) VALUES('" + descripcion + "','" + fecha_de_envio + "'," + id_paciente + ",'" + auxiliar_identificador + "')";
            }
            else
            {
                query = "INSERT INTO marketing (descripcion,fecha_de_envio,id_paciente) VALUES('" + descripcion + "','" + fecha_de_envio + "'," + id_paciente + ")";
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
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
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
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                //seguridad.Encriptar(descripcion + fecha_de_envio + id_paciente);
                string auxiliar_identificador = MostrarMarketing_Update(id_marketing);
                query = "UPDATE marketing set descripcion = '" + descripcion + "',fecha_de_envio = '" + fecha_de_envio + "',id_paciente = " + id_paciente + ",auxiliar_identificador = '<!--" + auxiliar_identificador + "-->' where id_marketing = " + id_marketing;
            }
            else
            {
                query = "UPDATE marketing set descripcion = '" + descripcion + "',fecha_de_envio = '" + fecha_de_envio + "',id_paciente = " + id_paciente + " where id_marketing = " + id_marketing;
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
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
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

        public string MostrarMarketing_Update(int id_marketing)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from marketing where id_marketing=" + id_marketing;

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
    }
}
