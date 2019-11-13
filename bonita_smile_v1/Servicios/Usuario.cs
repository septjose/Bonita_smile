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


        
        //verifica el tipo de rol. Devuelve el rol del alias
        public string verificarRol(string alias)
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            string rol = "";
            int id = 0;
            //int rol = 0;
            query = "SELECT * FROM usuario where alias='" + alias + "'";

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
              
                    foreach (UsuarioModel um in listaUsuario)
                    {
                        id = um.id_rol;

                    }

                if (id == 1)
                {
                    MessageBox.Show("Administrador");
                     rol = "Administrador";


                }
                else
                if (id == 2)
                {
                    MessageBox.Show("Clinica");
                    rol = "Clinica";

                }
                else
                    if (id == 3)
                {
                    MessageBox.Show("Marketing");
                     rol = "Marketing";

                }


               


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
            conexionBD.Close();
            return rol;


        }


        //Comprobara si existe el usuario en la base de datos y despues cimprobara si esta en la base de datos. Devuelve true si es correcto, de lo contario false
        public void redireccionarLogin(string alias, string password)
        {
            string rol = "";
            bool valido = Validar_login(alias, password);
            if(valido)
            {
                rol = verificarRol(alias);
                if(rol.Equals("Administrador"))
                {
                    //Admin admin = new Admin();
                    //admin.ShowDialog();
                }else
                    if(rol.Equals("Clinica"))
                {
                    //Clin clinica = new Clin();
                    //clinica.ShowDialog();
                }
                else
                    if(rol.Equals("Marketing"))
                {
                    //Mark marketing = new Mark();
                    //marketing.ShowDialog();
                }
            }
        }

       
        public bool Validar_login(string alias, string password)
        {
            bool verdad;
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            string pass = "";
            //int rol = 0;
            query = "SELECT * FROM usuario where alias='" + alias + "'";

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
                bool isEmpty = !listaUsuario.Any();
                if (isEmpty)
                {
                    MessageBox.Show("Usuario incorrecto");
                    verdad = false; ;
                }
                else
                {
                    foreach (UsuarioModel um in listaUsuario)
                    {
                        pass = um.password.ToString();
                       
                    }
                    Seguridad secure = new Seguridad();
                    string contraseña_desenc = secure.Desencriptar(pass);

                    //MessageBox.Show(password + "         pass_desnc=" + contraseña_desenc);
                    bool result = password.Equals(contraseña_desenc);

                    if (result)
                    {
                        conexionBD.Close();
                        verdad= true;
                    }
                    else
                    {
                        MessageBox.Show("contraseña esta incorrecta");
                        conexionBD.Close();
                       verdad= false;
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                verdad = false;
                conexionBD.Close();

            }
            return verdad;
            

        }





    }
}
