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
    class Clinicas
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;

        public Clinicas(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
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

        public bool eliminarClinica(string id_clinica)
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
                    query = "DELETE FROM clinica where id_clinica='" + id_clinica + "'";

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

        public bool insertarClinica(string nombre_sucursal, string color)
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
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
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

        public bool insertar_Permisos(string id_usuario, string id_clinica)
        {
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
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    query = "INSERT INTO permisos (id_permiso,id_usuario,id_clinica,auxiliar_identificador) VALUES('" + auxiliar_identificador + "','" + id_usuario + "','" + id_clinica + "','<!--" + auxiliar_identificador + "-->')";

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

        public bool actualizarClinica(string id_clinica, string nombre_sucursal, string color)
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
                    query = "UPDATE clinica set nombre_sucursal = '" + nombre_sucursal + "',color = '" + color + "',auxiliar_identificador ='" + id_clinica + "' where id_clinica = '" + id_clinica + "'";

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

        public bool actualizar_Permisos(string id_usuario, string id_clinica, string id_permiso)
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
                    query = "UPDATE permisos set id_usuario = '" + id_usuario + "',id_clinica = '" + id_clinica + "',auxiliar_identificador = '" + id_permiso + "' where id_permiso = '" + id_permiso + "'";

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

        public string MostrarClinica_Update(string id_clinica)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from clinica where id_clinica='" + id_clinica+"'";

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
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return aux_identi;
        }

        public string MostrarPermisos_Update(string id_permiso)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from permisos where id_permiso='" + id_permiso+"'";

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
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return aux_identi;
        }

    }
}
