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
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;



using bonita_smile_v1.Interfaz;


using System.Windows.Forms;
using System.IO;

namespace bonita_smile_v1.Servicios
{
    class Usuarios
    {
        private MySqlDataReader reader = null;
        private MySqlDataReader reader2 = null;
        private string query;
        private MySqlConnection conexionBD;
        private UsuarioModel usuarioModel;
        private Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;


        public Usuarios(bool online)
        {
            
            this.conexionBD = obj.conexion(online);
            this.online = online;
        }

        public List<UsuarioModel> MostrarUsuario_Socio(List<string>lista)
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            foreach (var id in lista)
            {
                query = "select * from usuario left join permisos on usuario.id_usuario=permisos.id_usuario left join clinica on clinica.id_clinica=permisos.id_clinica inner join rol on rol.id_rol=usuario.id_rol where clinica.id_clinica='"+id+"'";

                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usuarioModel = new UsuarioModel();
                        RolModel rolModel = new RolModel();

                        usuarioModel.id_usuario = reader[0].ToString();
                        usuarioModel.alias = reader[1].ToString();
                        usuarioModel.nombre = reader[2].ToString();
                        usuarioModel.apellidos = reader[3].ToString();
                        usuarioModel.password = reader[4].ToString();
                        rolModel.id_rol = int.Parse(reader[5].ToString());
                        rolModel.descripcion = reader[16].ToString();
                        usuarioModel.clinica = reader[12].ToString();
                        usuarioModel.rol = rolModel;

                        listaUsuario.Add(usuarioModel);
                    }
                }
                catch (MySqlException ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
                conexionBD.Close();
            }
            
           
            return listaUsuario;
        }

        public List<UsuarioModel> MostrarUsuario()
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            query = "select * from usuario left join permisos on usuario.id_usuario=permisos.id_usuario left join clinica on clinica.id_clinica=permisos.id_clinica inner join rol on rol.id_rol=usuario.id_rol";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarioModel = new UsuarioModel();
                    RolModel rolModel = new RolModel();

                    usuarioModel.id_usuario = reader[0].ToString();
                    usuarioModel.alias = reader[1].ToString();
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    rolModel.id_rol = int.Parse(reader[5].ToString());
                    rolModel.descripcion = reader[16].ToString();
                    usuarioModel.clinica = reader[12].ToString();
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


        public bool eliminarUsuario(string id_usuario)
        {
            bool internet = ti.Test();
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    query = "DELETE FROM usuario where id_usuario='" + id_usuario + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
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
            string auxiliar_identificador = new Seguridad().SHA1(alias + nombre + apellidos + password + id_rol + DateTime.Now);
            bool internet = ti.Test();
            password = new Seguridad().Encriptar(password);
            FileStream fs = null;

            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        bool exito = sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol,auxiliar_identificador) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ",'<!--" + auxiliar_identificador + "-->')";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarUsuario(string id_usuario, string alias, string nombre, string apellidos, string password, int id_rol)
        {
            bool internet = ti.Test();
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + ",auxiliar_identificador = '" + id_usuario + "' where id_usuario = '" + id_usuario + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public string MostrarUsuario_Update(string id_usuario)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from usuario where id_usuario='" + id_usuario+"'";

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
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return aux_identi;
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
                    //System.Windows.Application.Current.Windows[0].Close();
                    new Admin().ShowDialog();


                }
                else
                    if (rol.Equals("Doctor"))
                {
                    string id = Buscar_Clinica(alias,2);
                    if(id.Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("Este usuario no esta habilitado", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //System.Windows.Application.Current.Windows[0].Close();
                        //Application.Current.Windows[0].Close();
                        new Clin(id).ShowDialog();
                    }

                    
                }
                else
                    if (rol.Equals("Recepcionista"))
                {
                    string id = Buscar_Clinica(alias,4);
                    if(id.Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("Este usuario no esta habilitado", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        new Recep(id).ShowDialog();
                    }
                    
                    //new Market().ShowDialog();
                    //new Market(id).ShowDialog();
                }
                else
                    if(rol.Equals("Socio"))
                {
                    List<string> list = Buscar_clinica_socio(alias, 5);
                    
                    if(list.Count!=0)
                    {
                        new Soc(list,alias).ShowDialog();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Este usuario no esta habilitado", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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

                    usuarioModel.id_usuario = reader[0].ToString();
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

        public string Buscar_Clinica(String alias,int id_rol)
        {
            string id = "";
            MySqlCommand cmd;
            string query = "select clinica.id_clinica as id from usuario inner join permisos on permisos.id_usuario=usuario.id_usuario inner join clinica on clinica.id_clinica=permisos.id_clinica where usuario.alias='" + alias + "' and usuario.id_rol="+id_rol;
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

        public List<string> Buscar_clinica_socio(String alias, int id_rol)
        {
            List<string> lista = new List<string>();
            string query = "select clinica.id_clinica as id from usuario inner join permisos on permisos.id_usuario=usuario.id_usuario inner join clinica on clinica.id_clinica=permisos.id_clinica where usuario.alias='" + alias + "' and usuario.id_rol=" + id_rol;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   
                    lista.Add(reader[0].ToString());
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return  null;
                
            }
            conexionBD.Close();
            return lista;
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

