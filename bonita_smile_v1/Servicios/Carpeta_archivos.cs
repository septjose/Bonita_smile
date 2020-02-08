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
    class Carpeta_archivos
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;

        public Carpeta_archivos(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
        }

        public List<Carpeta_archivosModel> MostrarCarpeta_archivos()
        {
            List<Carpeta_archivosModel> listaCarpeta_archivos = new List<Carpeta_archivosModel>();
            query = "SELECT * FROM carpeta_archivos";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Carpeta_archivosModel carpeta_ArchivosModel = new Carpeta_archivosModel();

                    carpeta_ArchivosModel.id_carpeta = reader[0].ToString();
                    carpeta_ArchivosModel.nombre_carpeta = reader[1].ToString();
                    carpeta_ArchivosModel.id_paciente = reader[2].ToString();
                    carpeta_ArchivosModel.id_motivo = reader[3].ToString();

                    listaCarpeta_archivos.Add(carpeta_ArchivosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaCarpeta_archivos;
        }

        public Carpeta_archivosModel carpetaArchivos(string id_nota)
        {
            Carpeta_archivosModel listaCarpeta_archivos = new Carpeta_archivosModel();
            Carpeta_archivosModel carpeta_ArchivosModel = new Carpeta_archivosModel();
            query = "SELECT * FROM carpeta_archivos where id_nota='" + id_nota + "'";
            MessageBox.Show(query);

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   

                    carpeta_ArchivosModel.id_carpeta = reader[0].ToString();
                    carpeta_ArchivosModel.nombre_carpeta = reader[1].ToString();
                    carpeta_ArchivosModel.id_paciente = reader[2].ToString();
                    carpeta_ArchivosModel.id_motivo = reader[3].ToString();
                    carpeta_ArchivosModel.id_nota = reader[4].ToString();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return carpeta_ArchivosModel;
        }
        public bool eliminarCarpeta_archivos(string id_carpeta)
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
                    query = "DELETE FROM carpeta_archivos where id_carpeta='" + id_carpeta + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");

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

        public bool insertarCarpeta_archivos(string nombre_carpeta, string id_paciente, string id_motivo)
        {
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(nombre_carpeta + id_paciente + DateTime.Now);

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
                    query = "INSERT INTO carpeta_archivos (id_carpeta,nombre_carpeta,id_paciente,id_motivo,auxiliar_identificador) VALUES('" + auxiliar_identificador + "','" + nombre_carpeta + "','" + id_paciente + "','" + id_motivo + "','<!--" + auxiliar_identificador + "-->')";
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

        public bool actualizarCarpeta_archivos(string id_carpeta, string nombre_carpeta, string id_paciente, string id_motivo)
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
                    query = "UPDATE carpeta_archivos set nombre_carpeta = '" + nombre_carpeta + "',id_paciente = '" + id_paciente + "',id_motivo='" + id_motivo + "',auxiliar_identificador = '" + id_carpeta + "' where id_carpeta = '" + id_carpeta + "'";
                    Console.WriteLine(query);
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

        public string MostrarCarpeta_Archivos_Update(string id_carpeta)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from carpeta_archivos where id_carpeta='" + id_carpeta + "'";

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
        public List<Carpeta_archivosModel> MostrarCarpeta_archivos_paciente(string id_paciente,string id_motivo)
        {
            List<Carpeta_archivosModel> listaCarpeta_archivos = new List<Carpeta_archivosModel>();
            query = "SELECT * FROM carpeta_archivos where id_paciente='" + id_paciente + "' and id_motivo='"+id_motivo+"'" ;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Carpeta_archivosModel carpeta_ArchivosModel = new Carpeta_archivosModel();

                    carpeta_ArchivosModel.id_carpeta = reader[0].ToString();
                    carpeta_ArchivosModel.nombre_carpeta = reader[1].ToString();
                    carpeta_ArchivosModel.id_paciente = reader[2].ToString();
                    carpeta_ArchivosModel.id_motivo = reader[3].ToString();
                    carpeta_ArchivosModel.id_nota = reader[4].ToString();
                    listaCarpeta_archivos.Add(carpeta_ArchivosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaCarpeta_archivos;
        }
    }
}