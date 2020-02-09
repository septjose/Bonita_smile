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
    class Nota_de_digi_evolucion
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        private string prueba_remota = "prueba";

        public Nota_de_digi_evolucion(bool online)
        {
            MessageBox.Show(prueba_remota);
            this.conexionBD = obj.conexion(online);
            this.online = online;
        }

        public List<Nota_de_digi_evolucionModel> MostrarNota_de_digi_evolucion(string id_motivo, string id_paciente)
        {
            List<Nota_de_digi_evolucionModel> listaNota_de_digi_evolucion = new List<Nota_de_digi_evolucionModel>();
            query = "SELECT id_nota,id_paciente,id_motivo,descripcion,date_format(fecha, '%d/%m/%Y') as fecha FROM nota_de_digi_evolucion where id_paciente='" + id_paciente + "' and id_motivo='" + id_motivo+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Nota_de_digi_evolucionModel nota_De_Digi_EvolucionModel = new Nota_de_digi_evolucionModel();

                    nota_De_Digi_EvolucionModel.id_nota = reader[0].ToString();
                    nota_De_Digi_EvolucionModel.id_paciente = reader[1].ToString();
                    nota_De_Digi_EvolucionModel.id_motivo = reader[2].ToString();
                    nota_De_Digi_EvolucionModel.descripcion = reader[3].ToString();
                    nota_De_Digi_EvolucionModel.fecha = reader[4].ToString();

                    listaNota_de_digi_evolucion.Add(nota_De_Digi_EvolucionModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaNota_de_digi_evolucion;
        }

        public bool eliminarNotaEvolucion(string id_nota, string id_paciente, string id_motivo)
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

                   query =  "DELETE FROM nota_de_digi_evolucion WHERE id_nota = '"+id_nota+"' AND id_paciente ='"+id_paciente+"' AND id_motivo = '"+id_motivo+"'";
                    //query = "DELETE FROM nota_de_digi_evolucion where id_nota='" + id_nota + "'";

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

        public bool insertarNota_de_digi_evolucion(string id_paciente, string id_motivo, string descripcion_motivo, string descripcion, string fecha)
        {
            Seguridad seguridad = new Seguridad();
            string auxiliar_identificador_nota = seguridad.SHA1(id_paciente + id_motivo + descripcion + fecha + DateTime.Now);

          
           

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
                    int no_carpeta = 0;
                    query = "select count(descripcion) from  nota_de_digi_evolucion where id_paciente='" + id_paciente + "' and id_motivo='" + id_motivo + "'";
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        no_carpeta =Int32.Parse( reader[0].ToString());
                    }

                    conexionBD.Close();
                    string nombre_carpeta = "No_Carpeta_"+no_carpeta + "_" + DateTime.Now.ToString("dd/MM/yyyy");
                    string auxiliar_identificador_carpeta = seguridad.SHA1(nombre_carpeta + DateTime.Now);
                    // -----------HACER TRANSACCION-------- -/
                    query = "INSERT INTO nota_de_digi_evolucion (id_nota,id_paciente,id_motivo,descripcion,fecha,auxiliar_identificador) VALUES('" + auxiliar_identificador_nota + "','" + id_paciente + "','" + id_motivo + "','" + descripcion + "','" + fecha + "','<!--" + auxiliar_identificador_nota + "-->');";
                    query = query + "INSERT INTO carpeta_archivos (id_carpeta,nombre_carpeta,id_paciente,id_motivo,auxiliar_identificador,id_nota) VALUES('" + auxiliar_identificador_carpeta + "','" + nombre_carpeta + "','" + id_paciente + "','" + id_motivo + "','<!--" + auxiliar_identificador_carpeta + "-->','" + auxiliar_identificador_nota + "');";

                    conexionBD.Open();
                    
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();

                    conexionBD.Close();
                    // ---------------------------------------/

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

        public bool actualizarNota_de_digi_evolucion(string id_nota, string id_paciente, string id_motivo, string descripcion, string fecha)
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
                    query = "UPDATE nota_de_digi_evolucion set id_paciente ='" + id_paciente + "',id_motivo = '" + id_motivo + "',descripcion = '" + descripcion + "',fecha = '" + fecha + "',auxiliar_identificador = '" + id_nota + "' where id_nota = '" + id_nota + "'";

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

        public string MostrarNotas_Update(string id_nota)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from nota_de_digi_evolucion where id_nota='" + id_nota+"'";

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
