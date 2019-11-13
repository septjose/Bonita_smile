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
    class Usuario
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        private UsuarioModel usuarioModel;
        Conexion obj = new Conexion();

        public Usuario()
        {
            this.conexionBD = obj.conexion();
        }

        public List<UsuarioModel> MostrarUsuario()
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            query = "SELECT * FROM usuario";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UsuarioModel usuarioModel = new UsuarioModel();

                    usuarioModel.id_usuario = int.Parse(reader[0].ToString());
                    usuarioModel.alias = reader[1].ToString();
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    usuarioModel.id_rol = int.Parse(reader[5].ToString());

                    listaUsuario.Add(usuarioModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaUsuario;
        }

        public bool eliminarUsuario(int id_usuario)
        {
            query = "DELETE FROM usuario where id_usuario=" + id_usuario;
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

        public bool insertarUsuario(string alias, string nombre, string apellidos, string password, int id_rol)
        {
            password = new Seguridad().Encriptar(password);
            query = "INSERT INTO usuario (alias,nombre,apellidos,password,id_rol) VALUES('" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
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

        public bool actualizarUsuario(int id_usuario, string alias, string nombre, string apellidos, string password, int id_rol)
        {
            password = new Seguridad().Encriptar(password);
            query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = '" + id_rol + "' where id_usuario = " + id_usuario;
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


        //Comprobara si existe el usuario en la base de datos y despues cimprobara si esta en la base de datos. Devuelve true si es correcto, de lo contario false
        private bool validarUsuario(string alias, string password)
        {
            MySqlCommand cmd;
            string query = "SELECT * FROM usuario where alias=" + alias;
            try
            {
                cmd = new MySqlCommand(query, conexionBD);
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    conexionBD.Close();
                    return false;
                }
                else
                {
                    usuarioModel = new UsuarioModel();

                    usuarioModel.id_usuario = int.Parse(reader[0].ToString());
                    usuarioModel.alias = reader[1].ToString();
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    usuarioModel.id_rol = int.Parse(reader[5].ToString());

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

        //verifica el tipo de rol. Devuelve el rol del alias
        private string verificarRol()
        {
            string rol = "";

            return rol;
        }

        //Hace uso de los dos metodos anteriores, asi como redireccionar a la interfaz cerrespondiente
        public void redireccionarLogin(string alias, string password)
        {

        }

        //mismo metodo que validarUsuario, solo que sin validar pass, usa el que quieras
        public bool Validar_login(string alias, string password)
        {
            query = "SELECT * FROM usuario where alias='" + alias + "'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                if (Convert.ToInt32(cmd.ExecuteScalar())==0)
                {
                    //MessageBox.Show("Usuario incorrecto");
                    conexionBD.Close();
                    return false;
                }
                else
                {
                    usuarioModel = new UsuarioModel();

                    usuarioModel.id_usuario = int.Parse(reader[0].ToString());
                    usuarioModel.alias = reader[1].ToString();
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    usuarioModel.id_rol = int.Parse(reader[5].ToString());

                    Seguridad secure = new Seguridad();

                    if (password.Equals(secure.Desencriptar(usuarioModel.password)))
                    {
                        conexionBD.Close();
                        return true;
                        //MessageBox.Show("Bienvenido usuario");
                    }
                    else
                    {
                        conexionBD.Close();
                        return false;
                        //MessageBox.Show("contraseña esta incorrecta");
                    }
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
