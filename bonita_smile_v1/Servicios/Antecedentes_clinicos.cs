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
    class Antecedentes_clinicos
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();

        public Antecedentes_clinicos()
        {
            this.conexionBD = obj.conexion();
        }

        /*public List<Antecedentes_clinicosModel> MostrarAntecedentes_clinicos()
        {
            List<Antecedentes_clinicosModel> listaAntecedentes_clinicos = new List<Antecedentes_clinicosModel>();
            query = "SELECT * FROM antecedentes_clinicos";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Antecedentes_clinicosModel antecedentes_ClinicosModel = new Antecedentes_clinicosModel();

                    antecedentes_ClinicosModel.id_antecedentes = int.Parse(reader[0].ToString());
                    antecedentes_ClinicosModel.descripcion = reader[1].ToString();

                    listaAntecedentes_clinicos.Add(antecedentes_ClinicosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaAntecedentes_clinicos;
        }*/

        public bool eliminarAntecedentes_clinicos(string id_antecedentes)
        {
            MySqlCommand cmd;
            query = "DELETE FROM antecedentes_clinicos where id_antecedentes=" + id_antecedentes;
            string query_compropar = "SELECT * FROM antecedentes_clinicos where id_antecedentes=" + id_antecedentes;
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query_compropar, conexionBD);
                int existe = Convert.ToInt32(cmd.ExecuteScalar());
                if (existe == 0)
                {
                    MessageBox.Show("El registro que esta tratando de eliminar no existe");
                    return false;
                }
                else
                {
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public int insertarAntecedentes_clinicos(string descripcion)
        {
            int id = 0;
            query = "INSERT INTO antecedentes_clinicos (descripcion) VALUES('" + descripcion + "')";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                query = "select id_antecedentes from antecedentes_clinicos order by id_antecedentes desc limit 1";

                conexionBD.Open();
                var reader2 = new MySqlCommand(query, conexionBD).ExecuteReader();

                while (reader2.Read())
                {
                    id = int.Parse(reader2[0].ToString());
                }

                conexionBD.Close();
                return id;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return 0;
            }
        }

        public bool actualizarAbono(int id_antecedentes, string descripcion)
        {
            query = "UPDATE antecedentes_clinicos set descripcion = '" + descripcion + "' where id_antecedentes = " + id_antecedentes;
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