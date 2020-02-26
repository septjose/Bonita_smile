using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
        Test_Internet ti = new Test_Internet();
        private bool online;
        Configuracion_Model configuracion;

        public Clinicas(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
        }


        public List<PermisosModel> Mostrar_Permisos_socio(int id_rol,string alias)
        {
            List<PermisosModel> listaPermisos = new List<PermisosModel>();
            query = "(select DISTINCT '' as nombre_sucursal,'' as id_clinica,usuario.id_usuario,usuario.alias,usuario.nombre,usuario.apellidos,usuario.password,usuario.id_rol,rol.descripcion from usuario left join permisos on usuario.id_usuario=permisos.id_usuario inner join rol on rol.id_rol=usuario.id_rol where permisos.id_usuario is null and usuario.id_rol="+id_rol+")UNION(select  DISTINCT clinica.nombre_sucursal, clinica.id_clinica, usuario.id_usuario, usuario.alias, usuario.nombre, usuario.apellidos, usuario.password, usuario.id_rol, rol.descripcion from usuario inner join rol on usuario.id_rol = rol.id_rol INNER join permisos on permisos.id_usuario = usuario.id_usuario inner join clinica on clinica.id_clinica = permisos.id_clinica where permisos.id_clinica in (select id_clinica from usuario inner join permisos on usuario.id_usuario = permisos.id_usuario where usuario.alias = '"+alias+"') AND usuario.id_rol = "+id_rol+")";
                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PermisosModel permisosModel = new PermisosModel();
                        permisosModel.nombre_sucursal = reader[0].ToString();
                        permisosModel.id_clinica = reader[1].ToString();
                        permisosModel.id_usuario = reader[2].ToString();
                        string aliass = reader[3].ToString();
                        string a = aliass.Replace("_" + reader[2].ToString(), "");
                        // System.Windows.MessageBox.Show(a);
                        permisosModel.alias = a;
                        permisosModel.nombre = reader[4].ToString();
                        permisosModel.apellidos = reader[5].ToString();
                        listaPermisos.Add(permisosModel);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conexionBD.Close();         
            return listaPermisos;
        }


        public List<PermisosModel> Mostrar_Permisos(int id_rol)
        {
            List<PermisosModel> listaPermisos = new List<PermisosModel>();
            query = "select clinica.nombre_sucursal,clinica.id_clinica,usuario.nombre,usuario.apellidos,usuario.id_usuario,usuario.alias from usuario left join permisos on usuario.id_usuario=permisos.id_usuario left join clinica on clinica.id_clinica=permisos.id_clinica inner join rol on rol.id_rol=usuario.id_rol where rol.id_rol="+id_rol;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PermisosModel permisosModel = new PermisosModel();                
                    permisosModel.nombre_sucursal = reader[0].ToString();
                    permisosModel.id_clinica = reader[1].ToString();
                    permisosModel.nombre = reader[2].ToString();
                    permisosModel.apellidos = reader[3].ToString();
                    permisosModel.id_usuario = reader[4].ToString();
                    string aliass = reader[5].ToString();
                    string a = aliass.Replace("_" + reader[4].ToString(), "");
                    // System.Windows.MessageBox.Show(a);
                    permisosModel.alias = a;

                    listaPermisos.Add(permisosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaPermisos;
        }

        public List<ClinicaModel> MostrarClinica()
        {
            List<ClinicaModel> listaClinica = new List<ClinicaModel>();
            query = "SELECT * FROM clinica ";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClinicaModel clinicaModel = new ClinicaModel();
                    clinicaModel.id_clinica = reader[0].ToString();
                    clinicaModel.nombre_sucursal = reader[1].ToString();
                    clinicaModel.color = reader[2].ToString();
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

        public bool eliminarClinica(string id_clinica, string alias)
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
                    query = "DELETE FROM clinica where id_clinica='" + id_clinica + "'";

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

        public bool eliminar_Permiso(string id_usuario,string id_clinica,    string alias)
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
                   query = "DELETE FROM permisos where id_usuario='" + id_usuario + "' and id_clinica='" + id_clinica + "'";               
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

        public bool eliminar_Permisos(string id_usuario , string alias)
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

                    query = "DELETE FROM permisos where id_usuario='" + id_usuario+"'";

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

        public bool insertarClinica(string nombre_sucursal, string color   ,string alias)
        {
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(nombre_sucursal + color + DateTime.Now);
            bool internet = ti.Test();
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
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "INSERT INTO clinica (id_clinica,nombre_sucursal,color,auxiliar_identificador) VALUES('" + auxiliar_identificador + "','" + nombre_sucursal + "','" + color + "','<!--" + auxiliar_identificador + "-->')";

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

        public bool insertar_Permisos(string id_usuario, string id_clinica  ,string alias)
        {
            MessageBox.Show(alias);
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(id_usuario + id_clinica + DateTime.Now);
            bool internet = ti.Test();
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
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "INSERT INTO permisos (id_usuario,id_clinica,auxiliar_identificador) VALUES('" + id_usuario + "','" + id_clinica + "','<!--" + auxiliar_identificador + "-->')";
                    Console.WriteLine(query);
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
                MessageBox.Show("No se inserto ");
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public  bool Verificar_Tabla_Permisos(string id_usuario)
        {
            int cantidad=0;
            bool verifico ;
            query = "select count(id_usuario) from permisos where id_usuario='"+id_usuario+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     cantidad = Int32.Parse(reader[0].ToString());
                   
                }
                if(cantidad>0)
                {
                    verifico = true;
                }
                else
                {
                    verifico = false;
                }
                conexionBD.Close();
                return verifico;
               
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
                
            }
           
        }

        public bool actualizarClinica(string id_clinica, string nombre_sucursal, string color  ,string alias)
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
                    query = "UPDATE clinica set nombre_sucursal = '" + nombre_sucursal + "',color = '" + color + "',auxiliar_identificador ='" + id_clinica + "' where id_clinica = '" + id_clinica + "'";

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

        public bool actualizar_Permisos(string id_usuario, string id_clinica,string id_clinica_anterior ,string alias)
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
                    query = "UPDATE permisos set id_clinica = '" + id_clinica  + "' where id_usuario ='" + id_usuario + "' and id_clinica='"+ id_clinica_anterior + "'";
                    Console.WriteLine(query);
                    System.Windows.MessageBox.Show(query);
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

       

     

    }
}
