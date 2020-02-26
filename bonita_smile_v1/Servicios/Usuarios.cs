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
        Configuracion_Model configuracion;


        public Usuarios(bool online)
        {
            
            this.conexionBD = obj.conexion(online);
            this.online = online;
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
        }

        public List<UsuarioModel> MostrarUsuario_Socio(string alias)
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();           
                query = "(select DISTINCT usuario.id_usuario,usuario.alias,usuario.nombre,usuario.apellidos,usuario.password,usuario.id_rol,rol.descripcion from usuario LEFT join permisos on usuario.id_usuario=permisos.id_usuario inner join rol on rol.id_rol=usuario.id_rol where permisos.id_usuario is null and usuario.id_rol!=5 and usuario.id_rol!=1) union (select DISTINCT usuario.id_usuario,usuario.alias,usuario.nombre,usuario.apellidos,usuario.password,usuario.id_rol,rol.descripcion from usuario inner join rol on usuario.id_rol=rol.id_rol INNER join permisos on permisos.id_usuario=usuario.id_usuario where permisos.id_clinica in (select id_clinica from usuario inner join permisos on usuario.id_usuario = permisos.id_usuario where usuario.alias='"+alias+"') AND usuario.id_rol!=5 )";
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
                    string aliass = reader[1].ToString();
                    string a = aliass.Replace("_" + reader[0].ToString(), "");
                   // System.Windows.MessageBox.Show(a);
                    usuarioModel.alias = a;
                    usuarioModel.nombre = reader[2].ToString();
                        usuarioModel.apellidos = reader[3].ToString();
                        usuarioModel.password = reader[4].ToString();
                        rolModel.id_rol = int.Parse(reader[5].ToString());
                        rolModel.descripcion = reader[6].ToString();
                        //usuarioModel.clinica = reader[12].ToString();
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

        public List<UsuarioModel> MostrarUsuario()
        {
            List<UsuarioModel> listaUsuario = new List<UsuarioModel>();
            query = "select * from usuario  inner join rol on rol.id_rol=usuario.id_rol";
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
                    string aliass = reader[1].ToString();
                    string a=aliass.Replace("_"+reader[0].ToString(),"");
                   // System.Windows.MessageBox.Show(a);
                    usuarioModel.alias = a;
                    usuarioModel.nombre = reader[2].ToString();
                    usuarioModel.apellidos = reader[3].ToString();
                    usuarioModel.password = reader[4].ToString();
                    rolModel.id_rol = int.Parse(reader[7].ToString());
                    rolModel.descripcion = reader[8].ToString();
                    //usuarioModel.clinica = reader[12].ToString();
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

        public bool actualizarUsuarioSocio(string id_usu, string alias, string nombre, string apellidos, string password, int id_rol   ,string alias_user)
        {
            System.Windows.MessageBox.Show("Alias user : " + alias_user);
            string alias_id = alias + "_" + id_usu;
            bool internet = ti.Test();
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query =  "DELETE FROM permisos where id_usuario='" + id_usu + "';";
                    query = query + "UPDATE usuario set alias = '" + alias_id + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + ",auxiliar_identificador = '" + id_usu + "' where id_usuario = '" + id_usu + "'";
                   
                    Console.WriteLine(query);
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias_user + ".txt");

                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool eliminarUsuario(string id_usuario , string alias)
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
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
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
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");

                    return true;
                }
                
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarUsuario(string alias, string nombre, string apellidos, string password, int id_rol  ,string alias_user)
        {
            string auxiliar_identificador = new Seguridad().SHA1(alias + nombre + apellidos + password + id_rol + DateTime.Now);
            bool internet = ti.Test();
            password = new Seguridad().Encriptar(password);
            string alias_id = alias + "_" + auxiliar_identificador;
            FileStream fs = null;
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        bool exito = sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol,auxiliar_identificador) VALUES('" + auxiliar_identificador + "','" + alias_id + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ",'<!--" + auxiliar_identificador + "-->')";
                    //query = query + "insert into permisos (id_usuario) VALUES('"+auxiliar_identificador+"');";
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias_user + ".txt");

                    return true;
                }
                
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarUsuario(string id_usuario, string alias, string nombre, string apellidos, string password, int id_rol , string alias_user)
        {
            string alias_id = alias + "_" + id_usuario;
            bool internet = ti.Test();
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE usuario set alias = '" + alias_id + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + ",auxiliar_identificador = '" + id_usuario + "' where id_usuario = '" + id_usuario + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias_user + ".txt");

                    return true;
                }
               
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
        public void redireccionarLogin(string aliass, string password)
        {
            string rol = "";
            string alias = regreso_alias(aliass, password);
            if(!alias.Equals(""))
            {
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
                        string nombre_doctor = obtener_nombre_doctor(alias);

                        System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //System.Windows.Application.Current.Windows[0].Close();
                        new Admin(alias,nombre_doctor).ShowDialog();


                    }
                    else
                        if (rol.Equals("Doctor"))
                    {
                        string id = Buscar_Clinica(alias, 2);
                        if (id.Equals(""))
                        {
                            System.Windows.Forms.MessageBox.Show("Este usuario no esta habilitado", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //System.Windows.Application.Current.Windows[0].Close();
                            //Application.Current.Windows[0].Close();
                            string nombre = obtener_nombre_clinica(id);
                            string nombre_doctor = obtener_nombre_doctor(alias);
                            new Clin(id,nombre,nombre_doctor,alias).ShowDialog();
                        }

                    }
                    else
                        if (rol.Equals("Recepcionista"))
                    {
                        string id = Buscar_Clinica(alias, 4);
                        if (id.Equals(""))
                        {
                            System.Windows.Forms.MessageBox.Show("Este usuario no esta habilitado", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string nombre = obtener_nombre_clinica(id);
                            System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            new Recep(id,nombre,alias).ShowDialog();
                        }

                        //new Market().ShowDialog();
                        //new Market(id).ShowDialog();
                    }
                    else
                        if (rol.Equals("Socio"))
                    {
                        List<string> list = Buscar_clinica_socio(alias, 5);

                        if (list.Count != 0)
                            
                        {
                            string nombre_doctor = obtener_nombre_doctor(alias);
                            System.Windows.Forms.MessageBox.Show("Bienvenido usuario: " + alias, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            new Soc(list, nombre_doctor,alias).ShowDialog();
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Este usuario no esta habilitado", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                //System.Windows.MessageBox.Show("imprimo el alias :" + alias);
                System.Windows.Forms.MessageBox.Show("Usuario incorrecto", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        public string regreso_alias(string alias, string password)
        {
            string alias_nuevo = "";
            Seguridad secure = new Seguridad();
            List<UsuarioModel> list_usuarios = MostrarUsuario();
            for(int i=0;i<list_usuarios.Count;i++)
            {
                //System.Windows.MessageBox.Show(list_usuarios[i].alias + "        " + secure.Desencriptar(list_usuarios[i].password));
                if(alias.Equals(list_usuarios[i].alias)&& password.Equals(secure.Desencriptar(list_usuarios[i].password)))
                {
                    alias_nuevo = alias + "_" + list_usuarios[i].id_usuario;
                }
            }
            return alias_nuevo;
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
                    System.Windows.Forms.MessageBox.Show("Usuario incorrecto", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        System.Windows.Forms.MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string existe =cmd.ExecuteScalar().ToString();
                if (existe.Equals(""))
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

        public string obtener_nombre_clinica(string id_clinica)
        {
            string id = "";
            MySqlCommand cmd;
            string query = "select nombre_sucursal from clinica where id_clinica='"+id_clinica+"'";
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query, conexionBD);
                string existe = cmd.ExecuteScalar().ToString();
                if (existe.Equals("") )
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

        public string obtener_nombre_doctor(string alias)
        {
            string id = "";
            MySqlCommand cmd;
            string query = "select concat(nombre,' ',apellidos)as nombre_doctor from usuario where alias='"+alias+"'";
            try
            {
                conexionBD.Open();
                cmd = new MySqlCommand(query, conexionBD);
                string existe = cmd.ExecuteScalar().ToString();
                if (existe.Equals(""))
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
    }

}

