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
    class Rol
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Rol()
        {
            this.conexionBD = obj.conexion();
        }

        public List<RolModel> MostrarRol()
        {
            List<RolModel> listaRol = new List<RolModel>();
            query = "SELECT * FROM rol";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RolModel rolModel = new RolModel();

                    rolModel.id_rol = int.Parse(reader[0].ToString());
                    rolModel.descripcion = reader[1].ToString();

                    listaRol.Add(rolModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaRol;
        }

        public bool eliminarRol(int id_rol)
        {
            MySqlCommand cmd;
            query = "DELETE FROM rol where id_rol=" + id_rol;
            try
            {
                conexionBD.Open();
                if (!ValidarExistencia(id_rol))
                {
                    MessageBox.Show("El registro que esta tratando de eliminar no existe");
                    return false;
                }
                else
                {
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();
                    if (!ti.Test())
                    {
                        Escribir_Archivo ea = new Escribir_Archivo();
                        ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                    }
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

        public bool insertarRol(string descripcion)
        {
            query = "INSERT INTO rol (descripcion) VALUES('" + descripcion + "')";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Seguridad seguridad = new Seguridad();
                    string auxiliar_identificador = seguridad.Encriptar(descripcion);
                    query = "INSERT INTO rol (descripcion,auxiliar_identificador) VALUES('" + descripcion + "','" + auxiliar_identificador + "')";
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

        public bool actualizarRol(int id_rol, string descripcion)
        {
            query = "UPDATE rol set descripcion = '" + descripcion + "' where id_rol = " + id_rol;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Seguridad seguridad = new Seguridad();
                    string auxiliar_identificador = seguridad.Encriptar(descripcion);
                    query = "UPDATE rol set descripcion = '" + descripcion + "',auxiliar_identificador = '" + auxiliar_identificador + "' where id_rol = " + id_rol;
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

        private bool ValidarExistencia(int id_rol)
        {
            MySqlCommand cmd;
            string query = "SELECT * FROM rol where id_rol=" + id_rol;
            try
            {
                cmd = new MySqlCommand(query, conexionBD);
                int existe = Convert.ToInt32(cmd.ExecuteScalar());
                if (existe == 0)
                {
                    return false;
                }
                else
                {
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
    }
}
