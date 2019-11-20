using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Administrador.Paciente;
using bonita_smile_v1.Interfaz.Administrador.Clinica;
using bonita_smile_v1.Interfaz;
using bonita_smile_v1.Interfaz.Administrador.Color;
using bonita_smile_v1.Interfaz.Administrador.Usuario;
using bonita_smile_v1.Interfaz.Administrador.Antecedentes;
namespace bonita_smile_v1.Servicios
{
    class Usuarios
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        private UsuarioModel usuarioModel;
        private Conexion obj = new Conexion();
        

        public Usuarios()
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

        public List<UsuarioModel> Mostrar_unico_usuario(int id_usuario)
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            query = "SELECT * FROM usuario where id_usuario="+id_usuario+"";

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
            query = "DELETE FROM usuario where id_usuario="+ id_usuario;
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
            query = "INSERT INTO usuario (alias,nombre,apellidos,password,id_rol) VALUES('"+ alias +"','" + nombre +"','" + apellidos +"','"+ password +"',"+ id_rol +")";
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
            query = "UPDATE usuario set alias = '"+ alias +"',nombre = '"+ nombre +"',apellidos = '"+ apellidos +"',password = '"+ password +"',id_rol = '"+ id_rol +"' where id_usuario = "+ id_usuario;
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

        public string verificarRol(int id_rol)
        {
            string rol = "";
            MySqlCommand cmd;
            string query = "SELECT *FROM  rol where id_rol="+ id_rol;
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
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return "";
            }
        }



        //Comprobara si existe el usuario en la base de datos y despues cimprobara si esta en la base de datos. Devuelve true si es correcto, de lo contario false
        public void redireccionarLogin(string alias, string password)
        {
            string rol = "";
            bool valido = Validar_login(alias, password);
            if(valido)
            {
                rol = verificarRol(usuarioModel.id_rol);
                if(rol.Equals("Administrador"))
                {
                    //Admin admin = new Admin();
                    //Insertar_Paciente ip = new Insertar_Paciente();
                    //Ingresar_Clinica ic = new Ingresar_Clinica();
                    //Insertar_Color ic = new Insertar_Color();
                    //Insertar_Usuario iu = new Insertar_Usuario();
                    //Ingresar_Antecedentes_Clinicos iac = new Ingresar_Antecedentes_Clinicos();
                    Ventana_Administrador va = new Ventana_Administrador();
                    //Ventana_Usuario vu = new Ventana_Usuario();
                    MessageBox.Show("Administrador");
                    Application.Current.Windows[0].Close();
                    va.ShowDialog();
                   
                    
                }
                else
                    if(rol.Equals("Clinica"))
                {
                    //Clin clinica = new Clin();
                    //clinica.ShowDialog();
                    MessageBox.Show("Clinica");
                }
                else
                    if(rol.Equals("Marketing"))
                {
                    //Mark marketing = new Mark();
                    //marketing.ShowDialog();
                    MessageBox.Show("Marketing");
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
                    usuarioModel = new UsuarioModel();

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

