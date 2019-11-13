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
    class Colores
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();

        public Colores()
        {
            this.conexionBD = obj.conexion();
        }

        public List<ColoresModel> MostrarColor()
        {
            List<ColoresModel> listaColor = new List<ColoresModel>();
            query = "SELECT * FROM colores";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ColoresModel coloresModel = new ColoresModel();

                    coloresModel.id_color = int.Parse(reader[0].ToString());
                    coloresModel.descripcion = reader[1].ToString();

                    listaColor.Add(coloresModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaColor;
        }

        public bool eliminarColor(int id_color)
        {
            query = "DELETE FROM colores where id_color=" + id_color;
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

        public bool insertarColor(string descripcion)
        {
            query = "INSERT INTO colores (descripcion) VALUES('" + descripcion + "')";
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

        public bool actualizarColor(int id_color, string descripcion)
        {
            query = "UPDATE colores set descripcion = '" + descripcion + "' where id_color = " + id_color;
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
