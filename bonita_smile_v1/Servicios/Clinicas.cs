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
    class Clinicas
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();

        public Clinicas()
        {
            this.conexionBD = obj.conexion();
        }

        public List<ClinicaModel> MostrarClinica()
        {
            List<ClinicaModel> listaClinica = new List<ClinicaModel>();
            query = "SELECT * FROM clinica";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClinicaModel clinicaModel = new ClinicaModel();

                    clinicaModel.id_clinica = int.Parse(reader[0].ToString());
                    clinicaModel.id_color = int.Parse(reader[1].ToString());

                    listaClinica.Add(clinicaModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaClinica;

        }

        public bool eliminarClinica(int id_clinica)
        {
            query = "DELETE FROM clinica where id_clinica="+id_clinica;
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

        public bool insertarClinica(string nombre_sucursal, int id_color)
        {
            
            query = "INSERT INTO clinica (nombre_sucursal,id_color) VALUES('"+nombre_sucursal+"',"+id_color+")";
            //query = "INSERT INTO clinica (nombre_sucursal,id_color) VALUES('Clinica Salamanca',1)";
            Console.WriteLine(query);
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
                MessageBox.Show("E");
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarClinica(int id_clinica, string nombre_sucursal, int id_color)
        {
            query = "UPDATE clinica set nombre_sucursal = '"+nombre_sucursal+"',id_color = "+id_color+" where id_clinica = "+ id_clinica;
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
    }
}
