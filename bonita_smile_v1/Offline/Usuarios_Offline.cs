using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;

using bonita_smile_v1.Interfaz.Marketing;

using bonita_smile_v1.Interfaz;


using System.Windows.Forms;

using bonita_smile_v1.Servicios;

namespace bonita_smile_v1.Offline
{
    class Usuarios_Offline
    {
        private MySqlDataReader reader = null;
        private MySqlDataReader reader2 = null;
        private string query;
        private MySqlConnection conexionBD;
        private UsuarioModel usuarioModel;
        private Conexion_Offline obj = new Conexion_Offline();


        public Usuarios_Offline()
        {
            this.conexionBD = obj.conexion_offline();
        }

        public List<UsuarioModel> MostrarUsuario()
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            query = "SELECT * FROM usuario inner join rol on usuario.id_rol=rol.id_rol";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarioModel = new UsuarioModel();
                    RolModel rolModel = new RolModel();

                    usuarioModel.id_usuario = int.Parse(reader[0].ToString());
                    usuarioModel.alias = reader[1].ToString();
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    rolModel.id_rol = int.Parse(reader[5].ToString());
                    rolModel.descripcion = reader[7].ToString();
                    usuarioModel.rol = rolModel;

                    listaUsuario.Add(usuarioModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
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
                System.Windows.MessageBox.Show(ex.ToString());
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
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarUsuario(int id_usuario, string alias, string nombre, string apellidos, string password, int id_rol)
        {
            //password = new Seguridad().Encriptar(password);
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
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }



        //verifica el tipo de rol. Devuelve el rol del alias

        public string verificarRol(int id_rol)
        {
            string rol = "";
            MySqlCommand cmd;
            string query = "SELECT *FROM  rol where id_rol=" + id_rol;
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query, conexionBD);
                int existe = Convert.ToInt32(cmd.ExecuteScalar());
                if (existe == 0)
                {
                    conexionBD.Close();
                    return "";
                }
                else
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    rol = reader[1].ToString();
                    conexionBD.Close();
                    return rol;

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return "";
            }
        }



        //Comprobara si existe el usuario en la base de datos y despues cimprobara si esta en la base de datos. Devuelve true si es correcto, de lo contario false
        public void redireccionarLogin(string alias, string password)
        {
            string rol = "";
            bool valido = Validar_login(alias, password);
            if (valido)
            {
                rol = verificarRol(usuarioModel.rol.id_rol);
                if (rol.Equals("Administrador"))
                {
                    //Admin admin = new Admin();

                    //Ingresar_Clinica ic = new Ingresar_Clinica();
                    //Insertar_Color ic = new Insertar_Color();
                    //Insertar_Usuario iu = new Insertar_Usuario();
                    //Ingresar_Antecedentes_Clinicos iac = new Ingresar_Antecedentes_Clinicos();
                    //Ventana_Administrador va = new Ventana_Administrador();
                    //Ventana_Usuario vu = new Ventana_Usuario();

                    System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Windows.Application.Current.Windows[0].Close();
                    new Admin().ShowDialog();


                }
                else
                    if (rol.Equals("Clinica"))
                {
                    int id = Convert.ToInt32(Buscar_Clinica(alias));

                    System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias + "el id de la clinica es " + id, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //System.Windows.Application.Current.Windows[0].Close();
                    //Application.Current.Windows[0].Close();
                    new Clin(id).ShowDialog();
                }
                else
                    if (rol.Equals("Marketing"))
                {

                    System.Windows.MessageBox.Show("Marketing");
                    new Market().ShowDialog();
                }
            }
        }


        public bool Validar_login(string alias, string password)
        {
            bool verdad;
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            string pass = "";
            //int rol = 0;
            query = "SELECT * FROM usuario inner join rol on usuario.id_rol=rol.id_rol where usuario.alias='" + alias + "'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarioModel = new UsuarioModel();
                    RolModel rolModel = new RolModel();

                    usuarioModel.id_usuario = int.Parse(reader[0].ToString());
                    usuarioModel.alias = reader[1].ToString();
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    rolModel.id_rol = int.Parse(reader[5].ToString());
                    rolModel.descripcion = reader[7].ToString();
                    usuarioModel.rol = rolModel;

                    listaUsuario.Add(usuarioModel);
                }
                bool isEmpty = !listaUsuario.Any();
                if (isEmpty)
                {
                    System.Windows.MessageBox.Show("Usuario incorrecto");
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
                        verdad = true;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("contraseña esta incorrecta");
                        conexionBD.Close();
                        verdad = false;
                    }
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                verdad = false;
                conexionBD.Close();

            }
            return verdad;


        }

        public string Buscar_Clinica(String alias)
        {
            string id = "";
            MySqlCommand cmd;
            string query = "select clinica.id_clinica as id from usuario inner join permisos on permisos.id_usuario=usuario.id_usuario inner join clinica on clinica.id_clinica=permisos.id_clinica where usuario.alias='" + alias + "' and usuario.id_rol=2";
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query, conexionBD);
                int existe = Convert.ToInt32(cmd.ExecuteScalar());
                if (existe == 0)
                {
                    conexionBD.Close();
                    return "";
                }
                else
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    id = reader[0].ToString();
                    conexionBD.Close();
                    return id;

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return "";
            }
        }

        public string Buscar_Alias(int id_clinica)
        {
            string id = "";
            MySqlCommand cmd;
            string query = "select usuario.alias from usuario inner join permisos on usuario.id_usuario=permisos.id_usuario inner join clinica on clinica.id_clinica=permisos.id_clinica where clinica.id_clinica=" + id_clinica;
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query, conexionBD);
                System.Windows.MessageBox.Show(cmd.ExecuteScalar().ToString());
                string existe = cmd.ExecuteScalar().ToString();
                if (("".Equals(existe)))
                {
                    conexionBD.Close();
                    return "";
                }
                else
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    id = reader[0].ToString();
                    conexionBD.Close();
                    return id;

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return "";
            }
        }

        public string Buscar_Permiso(int id_clinica)
        {
            string id = "";
            MySqlCommand cmd;
            string query = "select permisos.id_permiso from permisos where permisos.id_clinica=" + id_clinica;
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query, conexionBD);

                int existe = Convert.ToInt32(cmd.ExecuteScalar());
                if (existe == 0)
                {
                    conexionBD.Close();
                    return "";
                }
                else
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    id = reader[0].ToString();
                    conexionBD.Close();
                    return id;

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return "";
            }
        }
        //public bool Validar_usu(string alias,string password)
        //{

        //    MySqlCommand cmd;
        //    string query = "SELECT * FROM usuario where alias='" + alias+"'";
        //    try
        //    {
        //        conexionBD.Open();
        //        cmd = new MySqlCommand(query, conexionBD);
        //        int existe = Convert.ToInt32(cmd.ExecuteScalar());
        //        if (existe == 0)
        //        {
        //            conexionBD.Close();
        //            return false;
        //        }
        //        else
        //        {
        //            reader = cmd.ExecuteReader();
        //            reader.Read();
        //            usuarioModel = new UsuarioModel();
        //            usuarioModel.id_usuario = int.Parse(reader[0].ToString());
        //            usuarioModel.alias = reader[1].ToString();
        //            usuarioModel.nombre = reader[2].ToString();
        //            usuarioModel.apellidos = reader[3].ToString();
        //            usuarioModel.password = reader[4].ToString();
        //            usuarioModel.id_rol = int.Parse(reader[5].ToString());
        //            Seguridad secure = new Seguridad();
        //            if(password.Equals(secure.Desencriptar(usuarioModel.password)))
        //            {
        //                MessageBox.Show("Bienvenido usuario");
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        conexionBD.Close();
        //        return false;
        //    }
        //}
    }

}

